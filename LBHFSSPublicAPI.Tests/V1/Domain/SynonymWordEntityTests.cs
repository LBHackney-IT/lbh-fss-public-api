using System;
using LBHFSSPublicAPI.V1.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace LBHFSSPublicAPI.Tests.V1.Domain
{
    [TestFixture]
    public class SynonymWordEntityTests
    {
        [Test]
        public void SynonymWordEntitiesHaveAnId()
        {
            var entity = new SynonymWordEntity();
            entity.Id.Should().BeGreaterOrEqualTo(0);
        }

        [Test]
        public void SynonymWordEntitiesHaveAGroupId()
        {
            var entity = new SynonymWordEntity();
            entity.GroupId = 1;
            entity.GroupId.Should().Be(1);
        }

        [Test]
        public void SynonymWordEntitiesHaveAWord()
        {
            var entity = new SynonymWordEntity();
            var word = "Test";
            entity.Word = word;
            entity.Word.Should().BeSameAs(word);
        }

        [Test]
        public void SynonymWordEntitiesHaveACreatedAt()
        {
            var entity = new SynonymWordEntity();
            var date = new DateTime(2019, 02, 21);
            entity.CreatedAt = date;
            entity.CreatedAt.Should().BeSameDateAs(date);
        }
    }
}
