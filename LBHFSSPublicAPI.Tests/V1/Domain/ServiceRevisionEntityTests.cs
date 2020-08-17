using System;
using LBHFSSPublicAPI.V1.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace LBHFSSPublicAPI.Tests.V1.Domain
{
    [TestFixture]
    public class ServiceRevisionEntityTests
    {
        [Test]
        public void ServiceRevisionEntitiesHaveAnId()
        {
            var entity = new ServiceRevisionEntity();
            entity.Id.Should().BeGreaterOrEqualTo(0);
        }

        [Test]
        public void ServiceRevisionEntitiesHaveAnAuthorId()
        {
            var entity = new ServiceRevisionEntity();
            entity.AuthorId = 1;
            entity.AuthorId.Should().Be(1);
        }

        [Test]
        public void ServiceRevisionEntitiesHaveAnOrganizationId()
        {
            var entity = new ServiceRevisionEntity();
            var description = "Test";
            entity.Description = description;
            entity.Description.Should().BeSameAs(description);
        }

        [Test]
        public void ServiceRevisionEntitiesHaveFacebook()
        {
            var entity = new ServiceRevisionEntity();
            var facebook = "Test";
            entity.Facebook = facebook;
            entity.Facebook.Should().BeSameAs(facebook);
        }

        [Test]
        public void ServiceRevisionEntitiesHaveAInstagram()
        {
            var entity = new ServiceRevisionEntity();
            var instagram = "Test";
            entity.Instagram = instagram;
            entity.Instagram.Should().BeSameAs(instagram);
        }

        [Test]
        public void ServiceRevisionEntitiesHaveALinkedin()
        {
            var entity = new ServiceRevisionEntity();
            var linkedin = "Test";
            entity.Linkedin = linkedin;
            entity.Linkedin.Should().BeSameAs(linkedin);
        }

        [Test]
        public void ServiceRevisionEntitiesHaveAName()
        {
            var entity = new ServiceRevisionEntity();
            var name = "Test";
            entity.Name = name;
            entity.Name.Should().BeSameAs(name);
        }

        [Test]
        public void ServiceRevisionEntitiesHaveAReviewedAt()
        {
            var entity = new ServiceRevisionEntity();
            var reviewedAt = new DateTime(2019, 02, 21);
            entity.ReviewedAt = reviewedAt;
            entity.ReviewedAt.Should().BeSameDateAs(reviewedAt);
        }

        [Test]
        public void ServiceRevisionEntitiesHaveAReviewerMessage()
        {
            var entity = new ServiceRevisionEntity();
            var reviewerMessage = "Test";
            entity.ReviewerMessage = reviewerMessage;
            entity.ReviewerMessage.Should().BeSameAs(reviewerMessage);
        }

        [Test]
        public void ServiceRevisionEntitiesHaveAReviewerUid()
        {
            var entity = new ServiceRevisionEntity();
            entity.ReviewerUid = 1;
            entity.ReviewerUid.Should().Be(1);
        }

        [Test]
        public void ServiceRevisionEntitiesHaveAStatus()
        {
            var entity = new ServiceRevisionEntity();
            var status = "Test";
            entity.Status = status;
            entity.Status.Should().BeSameAs(status);
        }

        [Test]
        public void ServiceRevisionEntitiesHaveASubmittedAt()
        {
            var entity = new ServiceRevisionEntity();
            var submittedAt = new DateTime(2019, 02, 21);
            entity.SubmittedAt = submittedAt;
            entity.SubmittedAt.Should().BeSameDateAs(submittedAt);
        }

        [Test]
        public void ServiceRevisionEntitiesHaveATelephone()
        {
            var entity = new ServiceRevisionEntity();
            var telephone = "Test";
            entity.Telephone = telephone;
            entity.Telephone.Should().BeSameAs(telephone);
        }

        [Test]
        public void ServiceRevisionEntitiesHaveATwitter()
        {
            var entity = new ServiceRevisionEntity();
            var twitter = "Test";
            entity.Twitter = twitter;
            entity.Twitter.Should().BeSameAs(twitter);
        }

        [Test]
        public void ServiceRevisionEntitiesHaveAWebsite()
        {
            var entity = new ServiceRevisionEntity();
            var website = "www.test.com";
            entity.Website = website;
            entity.Website.Should().BeSameAs(website);
        }

        [Test]
        public void ServiceRevisionEntitiesHaveACreatedAt()
        {
            var entity = new ServiceRevisionEntity();
            var date = new DateTime(2019, 02, 21);
            entity.CreatedAt = date;
            entity.CreatedAt.Should().BeSameDateAs(date);
        }
    }
}
