using System;
using LBHFSSPublicAPI.V1.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace LBHFSSPublicAPI.Tests.V1.Domain
{
    [TestFixture]
    public class RoleEntityTests
    {
        [Test]
        public void RoleEntitiesHaveAnId()
        {
            var entity = new RoleEntity();
            entity.Id.Should().BeGreaterOrEqualTo(0);
        }

        [Test]
        public void RoleEntitiesHaveAName()
        {
            var entity = new RoleEntity();
            var name = "Test";
            entity.Name = name;
            entity.Name.Should().BeSameAs(name);
        }

        [Test]
        public void RoleEntitiesHaveACreatedAt()
        {
            var entity = new RoleEntity();
            var date = new DateTime(2019, 02, 21);
            entity.CreatedAt = date;
            entity.CreatedAt.Should().BeSameDateAs(date);
        }
    }
}
