using System;
using System.Collections.Generic;
using System.Linq;
using LBHFSSPublicAPI.V1.Boundary.HelperWrappers;
using LBHFSSPublicAPI.V1.Boundary.Request;
using LBHFSSPublicAPI.V1.Domain;
using LBHFSSPublicAPI.V1.Factories;
using LBHFSSPublicAPI.V1.Gateways.Interfaces;
using LBHFSSPublicAPI.V1.Helpers;
using LBHFSSPublicAPI.V1.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace LBHFSSPublicAPI.V1.Gateways
{
    public class ServicesGateway : IServicesGateway
    {
        private readonly DatabaseContext _context;

        public ServicesGateway(DatabaseContext context)
        {
            _context = context;
        }
        public ServiceEntity GetService(int id)
        {
            var service = _context.Services
            .Include(s => s.Image)
            .Include(s => s.Organization)
            .Include(s => s.ServiceLocations)
            .Include(s => s.ServiceAnalytics)
            .Include(s => s.ServiceTaxonomies)
            .ThenInclude(st => st.Taxonomy)
            .FirstOrDefault(x => x.Id == id && x.Status.ToLower() == "active");
            service?.ServiceAnalytics.Add(new AnalyticsEvent
            {
                TimeStamp = DateTime.Now
            });
            _context.SaveChanges();
            return service.ToDomain();
        }

        public SearchServiceGatewayResult SearchServices(SearchServicesRequest requestParams)
        {
            var baseQuery = _context.Services
            .Include(s => s.Image)
            .Include(s => s.Organization)
            .Include(s => s.ServiceLocations)
            .Include(s => s.ServiceTaxonomies)
            .ThenInclude(st => st.Taxonomy)
            .Where(s => s.Organization.Status.ToLower() == "published")
            .Where(s => s.Status.ToLower() == "active")
            .AsEnumerable();

            IEnumerable<Service> fullMatchServicesQuery = baseQuery,
            splitMatchServicesQuery = baseQuery;

            List<ServiceEntity> fullMatchServices = new List<ServiceEntity>(),
            splitMatchServices = new List<ServiceEntity>();

            var synonyms = new HashSet<string>();

            var demographicTaxonomies = _context.Taxonomies
            .Where(t => t.Vocabulary == "demographic" && requestParams.TaxonomyIds.Any(ti => ti == t.Id))
            .Select(t => t.Id).ToList();

            var categoryTaxonomies = _context.Taxonomies
            .Where(t => t.Vocabulary == "category" && requestParams.TaxonomyIds.Any(ti => ti == t.Id))
            .Select(t => t.Id).ToList();

            if (demographicTaxonomies != null && demographicTaxonomies.Count != 0)
                fullMatchServicesQuery = fullMatchServicesQuery
                .Where(s => s.ServiceTaxonomies
                .Any(st => demographicTaxonomies.Contains(st.TaxonomyId)));

            if (categoryTaxonomies != null && categoryTaxonomies.Count != 0)
                fullMatchServicesQuery = fullMatchServicesQuery
                .Where(s => s.ServiceTaxonomies
                .Any(st => categoryTaxonomies.Contains(st.TaxonomyId)));

            if (!string.IsNullOrWhiteSpace(requestParams.Search))
            {
                var searchInputText = requestParams.Search.ToLower();
                var splitWords = searchInputText
                .Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

                var moreThan1SearchInputWord = splitWords.Count > 1; // fuzzy search won't execute if only 1 word entered

                splitWords = splitWords.Where(k => k.Length > 3).ToList(); // filter short words

                var matchedSynonyms = _context.SynonymWords // This variable is not being used! Does it need to be here?
                .Include(sw => sw.Group)
                .ThenInclude(sg => sg.SynonymWords)
                .Where(x => x.Word.ToLower().Contains(searchInputText))
                .Select(sw => sw.Group.SynonymWords.Select(sw => synonyms.Add(sw.Word.ToLower())) // hard to track side effects >_<
                .ToList())
                .ToList();

                // Filter on service organisation name
                Predicate<Service> containsUserInput = service => service.Organization.Name.ToLower().Contains(searchInputText);
                Predicate<Service> containsAnySynonym = service => synonyms.Any(sn => service.Organization.Name.ToLower().Contains(sn));
                ApplyFullTextFilter(containsUserInput,
                containsAnySynonym,
                synonyms,
                fullMatchServicesQuery,
                fullMatchServices);

                // Filter on service name
                containsUserInput = service => service.Name.ToLower().Contains(searchInputText);
                containsAnySynonym = service => synonyms.Any(sn => service.Name.ToLower().Contains(sn));
                ApplyFullTextFilter(containsUserInput,
                containsAnySynonym,
                synonyms,
                fullMatchServicesQuery,
                fullMatchServices);

                // Filter on service description
                containsUserInput = service => service.Description.ContainsWord(searchInputText);
                containsAnySynonym = service => synonyms.AnyWord(service.Description);
                ApplyFullTextFilter(containsUserInput,
                containsAnySynonym,
                synonyms,
                fullMatchServicesQuery,
                fullMatchServices);

                // More than one word in search input
                if (moreThan1SearchInputWord)
                {
                    var splitWordSynonyms = _context.SynonymWords
                    .Include(sw => sw.Group)
                    .ThenInclude(sg => sg.SynonymWords)
                    .AsEnumerable()
                    .Where(sw => splitWords.Any(spw => sw.Word.ToLower().Contains(spw)))
                    .SelectMany(sw => sw.Group.SynonymWords)
                    .Select(sw => sw.Word.ToLower())
                    .ToHashSet();

                    // Filter words on service organisation name
                    Predicate<Service> containsSplitUserInput = service => splitWords.Any(spw => service.Organization.Name.ToLower().Contains(spw));

                    ApplySplitMatchFilter(containsSplitUserInput,
                    splitMatchServicesQuery,
                    demographicTaxonomies,
                    categoryTaxonomies,
                    splitMatchServices,
                    fullMatchServices);

                    // Filter words on service name
                    containsSplitUserInput = service => splitWords.Any(spw => service.Name.ToLower().Contains(spw));

                    ApplySplitMatchFilter(containsSplitUserInput,
                    splitMatchServicesQuery,
                    demographicTaxonomies,
                    categoryTaxonomies,
                    splitMatchServices,
                    fullMatchServices);

                    // Filter words on service description
                    containsSplitUserInput = service => splitWords.AnyWord(service.Description);
                    ApplySplitMatchFilter(containsSplitUserInput,
                    splitMatchServicesQuery,
                    demographicTaxonomies,
                    categoryTaxonomies,
                    splitMatchServices,
                    fullMatchServices);

                    // Filter synonyms on organisation name
                    Predicate<Service> containsAnySplitInputSynonym = service => splitWordSynonyms.Any(spwsn => service.Organization.Name.ToLower().Contains(spwsn));
                    ApplySplitSynonymsMatchFilter(containsAnySplitInputSynonym,
                    splitMatchServicesQuery,
                    splitWordSynonyms,
                    demographicTaxonomies,
                    categoryTaxonomies,
                    splitMatchServices,
                    fullMatchServices);

                    // Filter synonyms on service name
                    containsAnySplitInputSynonym = service => splitWordSynonyms.Any(spwsn => service.Name.ToLower().Contains(spwsn));
                    ApplySplitSynonymsMatchFilter(containsAnySplitInputSynonym,
                    splitMatchServicesQuery,
                    splitWordSynonyms,
                    demographicTaxonomies,
                    categoryTaxonomies,
                    splitMatchServices,
                    fullMatchServices);

                    // Filter synonyms on service description
                    containsAnySplitInputSynonym = service => splitWordSynonyms.AnyWord(service.Description);
                    ApplySplitSynonymsMatchFilter(containsAnySplitInputSynonym,
                    splitMatchServicesQuery,
                    splitWordSynonyms,
                    demographicTaxonomies,
                    categoryTaxonomies,
                    splitMatchServices,
                    fullMatchServices);
                }
            }
            else
            {
                AddToCollection(fullMatchServices, fullMatchServicesQuery.Select(s => s.ToDomain()).ToList());
            }

            return new SearchServiceGatewayResult(fullMatchServices, splitMatchServices);
        }

        private void ApplyFullTextFilter(Predicate<Service> containsUserInput,
        Predicate<Service> containsAnySynonym,
        HashSet<string> synonyms,
        IEnumerable<Service> fullMatchServicesQuery,
        List<ServiceEntity> fullMatchServices)
        {
            var filters = new List<Predicate<Service>>() { containsUserInput };
            if (synonyms.Count > 0)
                filters.Add(containsAnySynonym);
            fullMatchServicesQuery = fullMatchServicesQuery.Where(s => filters.Any(p => p(s)));
            AddToCollection(fullMatchServices, fullMatchServicesQuery.Select(s => s.ToDomain()).ToList());
        }

        //NS test method...
        private void ApplyFullTextWordFilter(Predicate<Service> containsUserInput,
        Predicate<Service> containsAnySynonym,
        HashSet<string> synonyms,
        IEnumerable<Service> fullMatchServicesQuery,
        List<ServiceEntity> fullMatchServices)
        {
            var filters = new List<Predicate<Service>>() { containsUserInput };
            if (synonyms.Count > 0)
                filters.Add(containsAnySynonym);
            fullMatchServicesQuery = fullMatchServicesQuery.Where(s => filters.Any(p => p(s)));
            AddToCollection(fullMatchServices, fullMatchServicesQuery.Select(s => s.ToDomain()).ToList());
        }

        private void ApplySplitMatchFilter(Predicate<Service> containsSplitUserInput,
        IEnumerable<Service> splitMatchServicesQuery,
        List<int> demographicTaxonomies,
        List<int> categoryTaxonomies,
        List<ServiceEntity> splitMatchServices,
        List<ServiceEntity> fullMatchServices)
        {
            var splitFilters = new List<Predicate<Service>>() { containsSplitUserInput };
            var query = splitMatchServicesQuery.Where(s => splitFilters.Any(p => p(s))); //.Distinct(; is Id unique, or not?

            if (demographicTaxonomies != null && demographicTaxonomies.Count != 0)
                query = query?
                .Where(s => s.ServiceTaxonomies
                .Any(st => demographicTaxonomies.Contains(st.TaxonomyId)));

            if (categoryTaxonomies != null && categoryTaxonomies.Count != 0)
                query = query?
                .Where(s => s.ServiceTaxonomies
                .Any(st => categoryTaxonomies.Contains(st.TaxonomyId)));

            AddToCollection(splitMatchServices, query?.Select(s => s.ToDomain()).Except(
            fullMatchServices,
            new AnonEqualityComparer<ServiceEntity>(
            (f, s) => f.Name == s.Name,
            o => o.Id ^ o.Name.GetHashCode() ^ o.Description.GetHashCode()
            ))
            .ToList());
        }

        private void ApplySplitSynonymsMatchFilter(Predicate<Service> containsAnySplitInputSynonym,
        IEnumerable<Service> splitMatchServicesQuery,
        HashSet<string> splitWordSynonyms,
        List<int> demographicTaxonomies,
        List<int> categoryTaxonomies,
        List<ServiceEntity> splitMatchServices,
        List<ServiceEntity> fullMatchServices)
        {
            var splitFilters = new List<Predicate<Service>>();

            if (splitWordSynonyms.Count > 0)
                splitFilters.Add(containsAnySplitInputSynonym);

            var query = splitMatchServicesQuery?.Where(s => splitFilters.Any(p => p(s))); //.Distinct(; is Id unique, or not?

            if (demographicTaxonomies != null && demographicTaxonomies.Count != 0)
                query = query?
                .Where(s => s.ServiceTaxonomies
                .Any(st => demographicTaxonomies.Contains(st.TaxonomyId)));

            if (categoryTaxonomies != null && categoryTaxonomies.Count != 0)
                query = query?
                .Where(s => s.ServiceTaxonomies
                .Any(st => categoryTaxonomies.Contains(st.TaxonomyId)));

            AddToCollection(splitMatchServices, query?.Select(s => s.ToDomain()).Except(
            fullMatchServices,
            new AnonEqualityComparer<ServiceEntity>(
            (f, s) => f.Name == s.Name,
            o => o.Id ^ o.Name.GetHashCode() ^ o.Description.GetHashCode()
            ))
            .ToList());
        }

        private void AddToCollection(List<ServiceEntity> collection, List<ServiceEntity> set)
        {
            foreach (var item in set)
            {
                if (!collection.Any(c => c.Id == item.Id))
                {
                    collection.Add(item);
                }
            }
        }
    }
}
