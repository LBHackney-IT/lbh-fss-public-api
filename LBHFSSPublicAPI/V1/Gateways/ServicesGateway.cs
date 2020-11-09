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
                .Include(s => s.ServiceTaxonomies)
                .ThenInclude(st => st.Taxonomy)
                .FirstOrDefault(x => x.Id == id)
                .ToDomain();
            return service;
        }

        public SearchServiceGatewayResult SearchServices(SearchServicesRequest requestParams)
        {
            var baseQuery = _context.Services
                .Include(s => s.Image)
                .Include(s => s.Organization)
                .Include(s => s.ServiceLocations)
                .Include(s => s.ServiceTaxonomies)
                .ThenInclude(st => st.Taxonomy)
                .Where(s => s.Status == "active")
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

                var moreThan1SearchInputWord = splitWords.Count > 1;        // fuzzy search won't execute if only 1 word entered

                splitWords = splitWords.Where(k => k.Length > 3).ToList();  // filter short words

                var matchedSynonyms = _context.SynonymWords                 // This variable is not being used! Does it need to be here?
                    .Include(sw => sw.Group)
                    .ThenInclude(sg => sg.SynonymWords)
                    .Where(x => x.Word.ToLower().Contains(searchInputText))
                    .Select(sw => sw.Group.SynonymWords.Select(sw => synonyms.Add(sw.Word.ToLower())) // hard to track side effects >_<
                        .ToList())
                    .ToList();

                Predicate<Service> containsUserInput  = service => service.Name.ToLower().Contains(searchInputText);
                Predicate<Service> containsAnySynonym = service => synonyms.Any(sn => service.Name.ToLower().Contains(sn));

                var filters = new List<Predicate<Service>>();
                filters.Add(containsUserInput);

                if (synonyms.Count > 0)
                    filters.Add(containsAnySynonym);

                fullMatchServicesQuery = fullMatchServicesQuery.Where(s => filters.Any(p => p(s)));

                fullMatchServices = fullMatchServicesQuery.Select(s => s.ToDomain()).ToList();

                if (moreThan1SearchInputWord)
                {
                    splitMatchServicesQuery = splitMatchServicesQuery.Where(s => splitWords.Any(w => s.Name.ToLower().Contains(w)));

                    // Find out whether Splitwordsyn - It needs separate synonyms list - Would need tests for it anyway
                    // splitMatchServicesQuery = splitMatchServicesQuery.Where(s => synonyms2.Any(sn2 => s.Name.ToLower().Contains(sn2)));

                    if (demographicTaxonomies != null && demographicTaxonomies.Count != 0)
                        splitMatchServicesQuery = splitMatchServicesQuery
                            .Where(s => s.ServiceTaxonomies
                            .Any(st => demographicTaxonomies.Contains(st.TaxonomyId)));

                    if (categoryTaxonomies != null && categoryTaxonomies.Count != 0)
                        splitMatchServicesQuery = splitMatchServicesQuery
                            .Where(s => s.ServiceTaxonomies
                            .Any(st => categoryTaxonomies.Contains(st.TaxonomyId)));

                    splitMatchServices = splitMatchServicesQuery.Select(s => s.ToDomain()).Except(
                        fullMatchServices,
                        new AnonEqualityComparer<ServiceEntity>(
                            (f, s) => f.Name == s.Name,
                            o => o.Id ^ o.Name.GetHashCode() ^ o.Description.GetHashCode()
                        ))
                        .ToList();
                }
            }
            else
            {
                fullMatchServices = fullMatchServicesQuery.Select(s => s.ToDomain()).ToList();
            }            

            return new SearchServiceGatewayResult(fullMatchServices, splitMatchServices);
        }
    }
}
