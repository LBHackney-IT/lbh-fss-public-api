using System.Collections;
using System.Collections.Generic;
using AutoFixture;
using LBHFSSPublicAPI.V1.Infrastructure;

namespace LBHFSSPublicAPI.Tests.TestHelpers
{
    public static class EntityHelpers
    {
        public static Service CreateService()
        {
            var service = Randomm.Build<Service>()
                .Without(s => s.Id)
                .With(s => s.Organization, CreateOrganization())
                .With(s => s.Image, CreateFile)
                .Create();
            return service;
        }

        public static User CreateUser()
        {
            return Randomm.Build<User>().Without(u => u.Id).Create();
        }

        public static File CreateFile()
        {
            return Randomm.Build<File>().Without(f => f.Id).Create();
        }

        public static Role CreateRole()
        {
            return Randomm.Build<Role>()
                .Without(r => r.Id)
                .Create();
        }

        public static SynonymGroup CreateSynonymGroup()
        {
            return Randomm.Build<SynonymGroup>()
                .Without(s => s.Id)
                .Create();
        }

        public static SynonymWord CreateSynonymWord()
        {
            return Randomm.Build<SynonymWord>()
                .Without(s => s.Id)
                .With(s => s.Group, CreateSynonymGroup())
                .Create();
        }
        public static Organization CreateOrganization()
        {
            var organization = Randomm.Build<Organization>()
                .Without(o => o.Id)
                .With(o => o.ReviewerU, CreateUser())
                .Create();
            return organization;
        }

        public static ServiceLocation CreateServiceLocation()
        {
            var serviceLocation = Randomm.Build<ServiceLocation>()
                .Without(sl => sl.Id)
                .With(sl => sl.Service, CreateService())
                .Create();
            return serviceLocation;
        }

        public static Session CreateSession()
        {
            var session = Randomm.Build<Session>()
                .Without(o => o.Id)
                .With(o => o.User, CreateUser())
                .Create();
            return session;
        }

        public static Taxonomy CreateTaxonomy()
        {
            var taxonomy = Randomm.Build<Taxonomy>()
                .Without(t => t.Id)
                .Create();
            return taxonomy;
        }

        public static UserOrganization CreateUserOrganization()
        {
            var userOrganization = Randomm.Build<UserOrganization>()
                .Without(uo => uo.Id)
                .With(uo => uo.User, CreateUser())
                .With(uo => uo.Organization, CreateOrganization())
                .Create();
            return userOrganization;
        }

        public static ICollection<Taxonomy> CreateTaxonomies(int count = 3)
        {
            var taxonomies = new List<Taxonomy>();
            for (var a = 0; a < count; a++)
            {
                taxonomies.Add(CreateTaxonomy());
            }

            return taxonomies;
        }

        // public static UserRole CreateUserRole()
        // {
        //     var userRole = Randomm.Build<UserRole>()
        //         .Without(ur => ur.Id)
        //         .With(ur => ur.IdNavigation, CreateUser())
        //         .With(ur => ur.Role, CreateRole())
        //         .Create();
        //     return userRole;
        // }
    }
}
