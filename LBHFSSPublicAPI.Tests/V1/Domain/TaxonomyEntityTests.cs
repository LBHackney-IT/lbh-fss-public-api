using System;
using LBHFSSPublicAPI.V1.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace LBHFSSPublicAPI.Tests.V1.Domain
{
    [TestFixture]
    public class TaxonomyEntityTests
    {
        [Test]
        public void TaxonomyEntitiesHaveAnId()
        {
            var entity = new TaxonomyEntity();
            entity.Id.Should().BeGreaterOrEqualTo(0);
        }

        [Test]
        public void TaxonomyEntitiesHaveAName()
        {
            var entity = new TaxonomyEntity();
            var name = "Test";
            entity.Name = name;
            entity.Name.Should().BeSameAs(name);
        }

        [Test]
        public void TaxonomyEntitiesHaveAParentId()
        {
            var entity = new TaxonomyEntity();
            entity.ParentId = 0;
            entity.ParentId.Should().Be(0);
        }

        [Test]
        public void TaxonomyEntitiesHaveAVocabulary()
        {
            var entity = new TaxonomyEntity();
            var vocabulary = "Test";
            entity.Vocabulary = vocabulary;
            entity.Vocabulary.Should().BeSameAs(vocabulary);
        }

        [Test]
        public void TaxonomyEntitiesHaveACreatedAt()
        {
            var entity = new TaxonomyEntity();
            var date = new DateTime(2019, 02, 21);
            entity.CreatedAt = date;
            entity.CreatedAt.Should().BeSameDateAs(date);
        }
    }
}
