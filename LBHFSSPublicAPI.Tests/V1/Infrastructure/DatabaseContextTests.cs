using System.Linq;
using FluentAssertions;
using LBHFSSPublicAPI.Tests.TestHelpers;
using LBHFSSPublicAPI.Tests.V1.Helper;
using LBHFSSPublicAPI.V1.Infrastructure;
using NUnit.Framework;

namespace LBHFSSPublicAPI.Tests.V1.Infrastructure
{
    [TestFixture]
    public class DatabaseContextTests : DatabaseTests
    {
        [Test]
        public void CanGetAUserEntity()
        {
            var user = EntityHelpers.CreateUser();
            DatabaseContext.Add(user);
            DatabaseContext.SaveChanges();
            var result = DatabaseContext.Users.ToList().FirstOrDefault();
            result.Should().BeEquivalentTo(user);
        }

        [Test]
        public void CanGetARoleEntity()
        {
            var role = EntityHelpers.CreateRole();
            DatabaseContext.Add(role);
            DatabaseContext.SaveChanges();
            var result = DatabaseContext.Roles.ToList().FirstOrDefault();
            result.Should().BeEquivalentTo(role);
        }

        [Test]
        public void CanGetAnOrganizationEntity()
        {
            var organization = DatabaseEntityHelper.CreateOrganization();
            organization.Id = 0;
            DatabaseContext.Add(organization);
            DatabaseContext.SaveChanges();
            var result = DatabaseContext.Organizations.ToList().FirstOrDefault();
            result.Should().BeEquivalentTo(organization);
        }

        [Test]
        public void CanGetAUserOrganizationEntity()
        {
            var userOrganization = EntityHelpers.CreateUserOrganization();
            DatabaseContext.Add(userOrganization);
            DatabaseContext.SaveChanges();
            var result = DatabaseContext.UserOrganizations.ToList().FirstOrDefault();
            result.Should().BeEquivalentTo(userOrganization);
        }

        // [Test]
        // public void CanGetAUserRoleEntity()
        // {
        //     var userRole = EntityHelpers.CreateUserRole();
        //     DatabaseContext.Add(userRole);
        //     DatabaseContext.SaveChanges();
        //     var result = DatabaseContext.UserRoles.ToList().FirstOrDefault();
        //     result.Should().BeEquivalentTo(userRole);
        // }

        [Test]
        public void CanGetATaxonomyEntity()
        {
            var taxonomy = EntityHelpers.CreateTaxonomy();
            DatabaseContext.Add(taxonomy);
            DatabaseContext.SaveChanges();
            var result = DatabaseContext.Taxonomies.ToList().FirstOrDefault();
            result.Should().BeEquivalentTo(taxonomy);
        }

        [Test]
        public void CanGetASynonymGroupEntity()
        {
            var synonymGroup = EntityHelpers.CreateSynonymGroup();
            DatabaseContext.Add(synonymGroup);
            DatabaseContext.SaveChanges();
            var result = DatabaseContext.SynonymGroups.ToList().FirstOrDefault();
            result.Should().BeEquivalentTo(synonymGroup);
        }

        [Test]
        public void CanGetASynonymWordEntity()
        {
            var synonymWord = EntityHelpers.CreateSynonymWord();
            DatabaseContext.Add(synonymWord);
            DatabaseContext.SaveChanges();
            var result = DatabaseContext.SynonymWords.ToList().FirstOrDefault();
            result.Should().BeEquivalentTo(synonymWord);
        }

        [Test]
        public void CanGetAServiceEntity()
        {
            var service = EntityHelpers.CreateService();
            DatabaseContext.Add(service);
            DatabaseContext.SaveChanges();
            var result = DatabaseContext.Services.ToList().FirstOrDefault();
            result.Should().BeEquivalentTo(service);
        }

        [Test]
        public void CanGetASessionEntity()
        {
            var session = EntityHelpers.CreateSession();
            DatabaseContext.Add(session);
            DatabaseContext.SaveChanges();
            var result = DatabaseContext.Sessions.ToList().FirstOrDefault();
            result.Should().BeEquivalentTo(session);
        }

        [Test]
        public void CanGetAServiceLocationEntity()
        {
            var serviceLocation = EntityHelpers.CreateServiceLocation();
            DatabaseContext.Add(serviceLocation);
            DatabaseContext.SaveChanges();
            var result = DatabaseContext.ServiceLocations.ToList().FirstOrDefault();
            result.Should().BeEquivalentTo(serviceLocation);
        }
    }
}
