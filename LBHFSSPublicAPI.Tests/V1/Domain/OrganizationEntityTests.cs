using System;
using LBHFSSPublicAPI.V1.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace LBHFSSPublicAPI.Tests.V1.Domain
{
    public class OrganizationEntityTests
    {
        [Test]
        public void OrganizationEntitiesHaveAnId()
        {
            var entity = new OrganizationEntity();
            entity.Id.Should().BeGreaterOrEqualTo(0);
        }

        [Test]
        public void OrganizationEntitiesHaveAName()
        {
            var entity = new OrganizationEntity();
            var name = "Test";
            entity.Name = name;
            entity.Name.Should().BeSameAs(name);
        }

        [Test]
        public void OrganizationEntitiesHaveACreatedAt()
        {
            var entity = new OrganizationEntity();
            var date = new DateTime(2019, 02, 21);
            entity.CreatedAt = date;
            entity.CreatedAt.Should().BeSameDateAs(date);
        }
    }
}
