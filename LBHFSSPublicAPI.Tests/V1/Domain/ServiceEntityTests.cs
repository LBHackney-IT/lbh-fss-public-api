using System;
using LBHFSSPublicAPI.V1.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace LBHFSSPublicAPI.Tests.V1.Domain
{
    [TestFixture]
    public class ServiceEntityTests
    {
        [Test]
        public void ServiceEntitiesHaveAnId()
        {
            var entity = new ServiceEntity();
            entity.Id.Should().BeGreaterOrEqualTo(0);
        }

        // #database_ef_changes_v2-17-09-2020: Is this test still valid? (service revisions removed)
        //[Test]
        //public void ServiceEntitiesHaveARevisionId()
        //{
        //    var entity = new ServiceEntity();
        //    entity.RevisionId = 1;
        //    entity.RevisionId.Should().Be(1);
        //}

        [Test]
        public void ServiceEntitiesHaveAnOrganizationId()
        {
            var entity = new ServiceEntity();
            entity.OrganizationId = 1;
            entity.OrganizationId.Should().Be(1);
        }

        [Test]
        public void ServiceEntitiesHaveACreatedAt()
        {
            var entity = new ServiceEntity();
            var date = new DateTime(2019, 02, 21);
            entity.CreatedAt = date;
            entity.CreatedAt.Should().BeSameDateAs(date);
        }
    }
}
