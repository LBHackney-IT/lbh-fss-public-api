using System;
using System.Collections.Generic;
using System.Linq;
using LBHFSSPublicAPI.V1.Boundary.Request;
using LBHFSSPublicAPI.V1.Domain;
using LBHFSSPublicAPI.V1.Factories;
using LBHFSSPublicAPI.V1.Gateways.Interfaces;
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

        public ICollection<ServiceEntity> SearchServices(SearchServicesRequest requestParams)
        {
            var synonyms = new HashSet<string>();
            var demographicTaxonomies = _context.Taxonomies
                .Where(t => t.Vocabulary == "demographic" && requestParams.TaxonomyIds.Any(ti => ti == t.Id))
                .Select(t => t.Id).ToList();
            var categoryTaxonomies = _context.Taxonomies
                .Where(t => t.Vocabulary == "category" && requestParams.TaxonomyIds.Any(ti => ti == t.Id))
                .Select(t => t.Id).ToList();
            if (!string.IsNullOrWhiteSpace(requestParams.Search))
            {
                var searchInputText = requestParams.Search.ToUpper();
                var keywords = searchInputText.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                keywords.Append(searchInputText);           // the concatinated value might be in the synonyms database

                keywords = keywords.Where(k => k.Length > 3).ToArray();

                foreach (var keyword in keywords)
                    synonyms.Add(keyword);

                var matchedSynonyms = _context.SynonymWords // This variable is not being used! Does it even need to be here?
                    .Include(sw => sw.Group)
                    .ThenInclude(sg => sg.SynonymWords)
                    .Where(x => x.Word.ToUpper().Contains(requestParams.Search.ToUpper()))
                    .Select(sw => sw.Group.SynonymWords.Select(sw => synonyms.Add(sw.Word.ToUpper()))
                        .ToList())
                    .ToList();
            }
            var services = _context.Services
                .Include(s => s.Image)
                .Include(s => s.Organization)
                .Include(s => s.ServiceLocations)
                .Include(s => s.ServiceTaxonomies)
                .ThenInclude(st => st.Taxonomy)
                .AsEnumerable()
                .Where(s => s.Status == "active")
                .Where(s => synonyms.Count == 0 || synonyms.Any(b => s.Name.ToUpper().Contains(b)))
                .Where(s => demographicTaxonomies == null || demographicTaxonomies.Count == 0
                                                          || s.ServiceTaxonomies.Any(st => demographicTaxonomies.Contains(st.TaxonomyId)))
                .Where(s => categoryTaxonomies == null || categoryTaxonomies.Count == 0
                                                          || s.ServiceTaxonomies.Any(st => categoryTaxonomies.Contains(st.TaxonomyId)))
                .Select(s => s.ToDomain())
                .ToList();
            return services;
        }
    }
}
