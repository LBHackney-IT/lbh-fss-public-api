using System;
using LBHFSSPublicAPI.V1.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace LBHFSSPublicAPI.Tests.V1.Domain
{
    [TestFixture]
    public class UserEntityTests
    {
        [Test]
        public void UserEntitiesHaveAnId()
        {
            var entity = new UserEntity();
            entity.Id.Should().BeGreaterOrEqualTo(0);
        }

        [Test]
        public void UserEntitiesHaveAStatus()
        {
            var entity = new UserEntity();
            var status = "Test";
            entity.Status = status;
            entity.Status.Should().BeSameAs(status);
        }

        [Test]
        public void UserEntitiesHaveAName()
        {
            var entity = new UserEntity();
            var name = "Test";
            entity.Name = name;
            entity.Name.Should().BeSameAs(name);
        }

        [Test]
        public void UserEntitiesHaveASubId()
        {
            var entity = new UserEntity();
            var subId = "Test";
            entity.SubId = subId;
            entity.SubId.Should().BeSameAs(subId);
        }

        [Test]
        public void UserEntitiesHaveAnEmail()
        {
            var entity = new UserEntity();
            var email = "test@test.com";
            entity.Email = email;
            entity.Email.Should().BeSameAs(email);
        }

        [Test]
        public void UserEntitiesHaveACreatedAt()
        {
            var entity = new UserEntity();
            var date = new DateTime(2019, 02, 21);
            entity.CreatedAt = date;
            entity.CreatedAt.Should().BeSameDateAs(date);
        }
    }
}
