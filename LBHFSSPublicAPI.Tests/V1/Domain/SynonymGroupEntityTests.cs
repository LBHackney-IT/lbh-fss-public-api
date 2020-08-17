using System;
using LBHFSSPublicAPI.V1.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace LBHFSSPublicAPI.Tests.V1.Domain
{
    [TestFixture]
    public class SynonymGroupEntityTests
    {
        [Test]
        public void SynonymGroupEntitiesHaveAnId()
        {
            var entity = new SynonymGroupEntity();
            entity.Id.Should().BeGreaterOrEqualTo(0);
        }

        [Test]
        public void SynonymGroupEntitiesHaveAName()
        {
            var entity = new SynonymGroupEntity();
            var name = "Test";
            entity.Name = name;
            entity.Name.Should().BeSameAs(name);
        }

        [Test]
        public void SynonymGroupEntitiesHaveACreatedAt()
        {
            var entity = new SynonymGroupEntity();
            var date = new DateTime(2019, 02, 21);
            entity.CreatedAt = date;
            entity.CreatedAt.Should().BeSameDateAs(date);
        }
    }
}
