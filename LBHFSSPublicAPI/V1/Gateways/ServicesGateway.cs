using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            if (string.IsNullOrWhiteSpace(requestParams.Search))
                return _context.Services
                        .Select(s => s.ToDomain())
                        .ToList();
            synonyms.Add(requestParams.Search.ToUpper());
            var matchedSynonyms = _context.SynonymWords
                .Include(sw => sw.Group)
                .ThenInclude(sg => sg.SynonymWords)
                .Where(x => x.Word.ToUpper().Contains(requestParams.Search.ToUpper()))
                .Select(sw => sw.Group.SynonymWords.Select(sw => synonyms.Add(sw.Word.ToUpper()))
                    .ToList())
                .ToList();
            var services = _context.Services
                .Include(s => s.Image)
                .Include(s => s.Organization)
                .Include(s => s.ServiceLocations)
                .Include(s => s.ServiceTaxonomies)
                .ThenInclude(st => st.Taxonomy)
                .AsEnumerable()
                .Where(x => synonyms.Any(b => x.Name.ToUpper().Contains(b)))
                .Select(s => s.ToDomain())
                .ToList();
            return services;
        }
    }
}
