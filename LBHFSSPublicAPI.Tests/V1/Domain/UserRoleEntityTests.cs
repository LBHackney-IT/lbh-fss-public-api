using System;
using LBHFSSPublicAPI.V1.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace LBHFSSPublicAPI.Tests.V1.Domain
{
    [TestFixture]
    public class UserRoleEntityTests
    {
        [Test]
        public void UserRoleEntitiesHaveAnId()
        {
            var entity = new UserRoleEntity();
            entity.Id.Should().BeGreaterOrEqualTo(0);
        }

        [Test]
        public void UserRoleEntitiesHaveARoleId()
        {
            var entity = new UserRoleEntity();
            var roleId = 1;
            entity.RoleId = roleId;
            entity.RoleId.Should().Be(roleId);
        }

        [Test]
        public void UserRoleEntitiesHaveACreatedAt()
        {
            var entity = new UserRoleEntity();
            var date = new DateTime(2019, 02, 21);
            entity.CreatedAt = date;
            entity.CreatedAt.Should().BeSameDateAs(date);
        }
    }
}
