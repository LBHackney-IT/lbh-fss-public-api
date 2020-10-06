using System;
using LBHFSSPublicAPI.V1.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace LBHFSSPublicAPI.Tests.V1.Domain
{
    public class UserOrganizationEntityTests
    {
        [Test]
        public void UserOrganizationEntitiesHaveAnId()
        {
            var entity = new UserOrganizationEntity();
            entity.Id.Should().BeGreaterOrEqualTo(0);
        }

        [Test]
        public void UserOrganizationEntitiesHaveAOrganizationId()
        {
            var entity = new UserOrganizationEntity();
            var organizationId = 1;
            entity.OrganizationId = organizationId;
            entity.OrganizationId.Should().Be(organizationId);
        }

        [Test]
        public void UserOrganizationEntitiesHaveACreatedAt()
        {
            var entity = new UserOrganizationEntity();
            var date = new DateTime(2019, 02, 21);
            entity.CreatedAt = date;
            entity.CreatedAt.Should().BeSameDateAs(date);
        }
    }
}
