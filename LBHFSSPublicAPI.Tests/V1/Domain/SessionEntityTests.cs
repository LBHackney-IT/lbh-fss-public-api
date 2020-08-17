using System;
using LBHFSSPublicAPI.V1.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace LBHFSSPublicAPI.Tests.V1.Domain
{
    [TestFixture]
    public class SessionEntityTests
    {
        [Test]
        public void SessionEntitiesHaveAnId()
        {
            var entity = new SessionEntity();
            entity.Id.Should().BeGreaterOrEqualTo(0);
        }

        [Test]
        public void SessionEntitiesHaveAnIpAddress()
        {
            var entity = new SessionEntity();
            var ipAddress = "172.61.41.22";
            entity.IpAddress = ipAddress;
            entity.IpAddress.Should().BeSameAs(ipAddress);
        }

        [Test]
        public void SessionEntitiesHaveALastAccessAt()
        {
            var entity = new SessionEntity();
            var date = new DateTime(2019, 02, 21);
            entity.LastAccessAt = date;
            entity.LastAccessAt.Should().BeSameDateAs(date);
        }

        [Test]
        public void SessionEntitiesHaveAPayload()
        {
            var entity = new SessionEntity();
            var payload = "Test";
            entity.Payload = payload;
            entity.Payload.Should().BeSameAs(payload);
        }

        [Test]
        public void SessionEntitiesHaveAUserAgent()
        {
            var entity = new SessionEntity();
            var userAgent = "UserAgent";
            entity.UserAgent = userAgent;
            entity.UserAgent.Should().BeSameAs(userAgent);
        }

        [Test]
        public void SessionEntitiesHaveAUserId()
        {
            var entity = new SessionEntity();
            entity.UserId = 1;
            entity.UserId.Should().Be(1);
        }

        [Test]
        public void SessionEntitiesHaveACreatedAt()
        {
            var entity = new SessionEntity();
            var date = new DateTime(2019, 02, 21);
            entity.CreatedAt = date;
            entity.CreatedAt.Should().BeSameDateAs(date);
        }
    }
}
