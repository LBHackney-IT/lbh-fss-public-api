using System.Linq;
using FluentAssertions;
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
            var user = DatabaseEntityHelper.CreateUser();
            DatabaseContext.Add(user);
            DatabaseContext.SaveChanges();
            var result = DatabaseContext.Users.ToList().FirstOrDefault();
            result.Should().BeEquivalentTo(user);
        }

        [Test]
        public void CanGetARoleEntity()
        {
            var role = DatabaseEntityHelper.CreateRole();
            DatabaseContext.Add(role);
            DatabaseContext.SaveChanges();
            var result = DatabaseContext.Roles.ToList().FirstOrDefault();
            result.Should().BeEquivalentTo(role);
        }

        [Test]
        public void CanGetAUserRoleEntity()
        {
            var user = DatabaseEntityHelper.CreateUser();
            DatabaseContext.Add(user);
            var role = DatabaseEntityHelper.CreateRole();
            DatabaseContext.Add(role);
            DatabaseContext.SaveChanges();
            var userRole = DatabaseEntityHelper.CreateUserRole();
            userRole.Id = user.Id;
            userRole.Role = role;
            DatabaseContext.Add(userRole);
            DatabaseContext.SaveChanges();
            var result = DatabaseContext.UserRoles.ToList().FirstOrDefault();
            result.Should().BeEquivalentTo(userRole);
        }

        [Test]
        public void CanGetAnOrganizationEntity()
        {
            var organization = DatabaseEntityHelper.CreateOrganization();
            DatabaseContext.Add(organization);
            DatabaseContext.SaveChanges();
            var result = DatabaseContext.Organizations.ToList().FirstOrDefault();
            result.Should().BeEquivalentTo(organization);
        }

        [Test]
        public void CanGetAUserOrganizationEntity()
        {
            var user = DatabaseEntityHelper.CreateUser();
            DatabaseContext.Add(user);
            var organization = DatabaseEntityHelper.CreateOrganization();
            DatabaseContext.Add(organization);
            DatabaseContext.SaveChanges();
            var userOrganization = DatabaseEntityHelper.CreateUserOrganization();
            userOrganization.User = user;
            userOrganization.Organization = organization;
            DatabaseContext.Add(userOrganization);
            DatabaseContext.SaveChanges();
            var result = DatabaseContext.UserOrganizations.ToList().FirstOrDefault();
            result.Should().BeEquivalentTo(userOrganization);
        }

        [Test]
        public void CanGetATaxonomyEntity()
        {
            var taxonomy = DatabaseEntityHelper.CreateTaxonomy();
            DatabaseContext.Add(taxonomy);
            DatabaseContext.SaveChanges();
            var result = DatabaseContext.Taxonomies.ToList().FirstOrDefault();
            result.Should().BeEquivalentTo(taxonomy);
        }

        [Test]
        public void CanGetASynonymGroupEntity()
        {
            var synonymGroup = DatabaseEntityHelper.CreateSynonymGroup();
            DatabaseContext.Add(synonymGroup);
            DatabaseContext.SaveChanges();
            var result = DatabaseContext.SynonymGroups.ToList().FirstOrDefault();
            result.Should().BeEquivalentTo(synonymGroup);
        }

        [Test]
        public void CanGetASynonymWordEntity()
        {
            var synonymGroup = DatabaseEntityHelper.CreateSynonymGroup();
            DatabaseContext.Add(synonymGroup);
            var synonymWord = DatabaseEntityHelper.CreateSynonymWord();
            synonymWord.Group = synonymGroup;
            DatabaseContext.Add(synonymWord);
            DatabaseContext.SaveChanges();
            var result = DatabaseContext.SynonymWords.ToList().FirstOrDefault();
            result.Should().BeEquivalentTo(synonymWord);
        }

        [Test]
        public void CanGetAServiceEntity()
        {
            var organization = DatabaseEntityHelper.CreateOrganization();
            DatabaseContext.Add(organization);
            var service = DatabaseEntityHelper.CreateService();
            service.Organization = organization;
            DatabaseContext.Add(service);
            DatabaseContext.SaveChanges();
            var result = DatabaseContext.Services.ToList().FirstOrDefault();
            result.Should().BeEquivalentTo(service);
        }

        [Test]
        public void CanGetASessionEntity()
        {
            var user = DatabaseEntityHelper.CreateUser();
            DatabaseContext.Add(user);
            var session = DatabaseEntityHelper.CreateSession();
            session.User = user;
            DatabaseContext.Add(session);
            DatabaseContext.SaveChanges();
            var result = DatabaseContext.Sessions.ToList().FirstOrDefault();
            result.Should().BeEquivalentTo(session);
        }

        [Test]
        public void CanGetAServiceRevisionEntity()
        {
            var organization = DatabaseEntityHelper.CreateOrganization();
            DatabaseContext.Add(organization);
            var author = DatabaseEntityHelper.CreateUser();
            DatabaseContext.Add(author);
            var reviewer = DatabaseEntityHelper.CreateUser();
            DatabaseContext.Add(reviewer);
            var service = DatabaseEntityHelper.CreateService();
            service.Organization = organization;
            DatabaseContext.Add(service);
            var serviceRevision = DatabaseEntityHelper.CreateServiceRevision();
            serviceRevision.Author = author;
            serviceRevision.ReviewerU = reviewer;
            serviceRevision.Service = service;
            DatabaseContext.Add(serviceRevision);
            DatabaseContext.SaveChanges();
            var result = DatabaseContext.ServiceRevisions.ToList().FirstOrDefault();
            result.Should().BeEquivalentTo(serviceRevision);
        }

        [Test]
        public void CanGetAServiceLocationEntity()
        {
            var organization = DatabaseEntityHelper.CreateOrganization();
            DatabaseContext.Add(organization);
            var author = DatabaseEntityHelper.CreateUser();
            DatabaseContext.Add(author);
            var reviewer = DatabaseEntityHelper.CreateUser();
            DatabaseContext.Add(reviewer);
            var service = DatabaseEntityHelper.CreateService();
            service.Organization = organization;
            DatabaseContext.Add(service);
            var serviceRevision = DatabaseEntityHelper.CreateServiceRevision();
            serviceRevision.Author = author;
            serviceRevision.ReviewerU = reviewer;
            serviceRevision.Service = service;
            DatabaseContext.Add(serviceRevision);
            var serviceLocation = DatabaseEntityHelper.CreateServiceLocation();
            serviceLocation.Revision = serviceRevision;
            DatabaseContext.Add(serviceLocation);
            DatabaseContext.SaveChanges();
            var result = DatabaseContext.ServiceLocations.ToList().FirstOrDefault();
            result.Should().BeEquivalentTo(serviceLocation);
        }
    }
}
