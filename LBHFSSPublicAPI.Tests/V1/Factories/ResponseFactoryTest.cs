using FluentAssertions;
using LBHFSSPublicAPI.Tests.TestHelpers;
using LBHFSSPublicAPI.V1.Domain;
using LBHFSSPublicAPI.V1.Factories;
using NUnit.Framework;
using System.Linq;

namespace LBHFSSPublicAPI.Tests.V1.Factories
{
    [TestFixture]
    public class ResponseFactoryTest
    {
        // TODO: Add more granural tests.

        [TestCase(TestName =
            "Given a service domain object, When ToResponseService factory extension method is called, Then it returns a correctly populated service response boundary object")]
        public void FactoryConvertsServiceDomainIntoServiceResponse()
        {
            // arrange
            var domainService = EntityHelpers.CreateService().ToDomain(); // would need domain generator, but domain object is messed up now.

            // act
            var responseService = domainService.ToResponseService();

            // assert
            responseService.Id.Should().Be(domainService.Id);
            responseService.Name.Should().Be(domainService.Name);
            responseService.Categories.Should().OnlyContain(c =>
                c.Vocabulary == "category" &&
                domainService.ServiceTaxonomies.Any(st =>
                    st.Taxonomy != null &&
                    st.Taxonomy.Id == c.Id &&
                    st.Taxonomy.Name == c.Name &&
                    st.Taxonomy.Description == c.Description &&
                    st.Taxonomy.Weight == c.Weight
                    ));

            responseService.Contact.Email.Should().Be(domainService.Email);
            responseService.Contact.Telephone.Should().Be(domainService.Telephone);
            responseService.Contact.Website.Should().Be(domainService.Website);

            responseService.Demographic.Should().OnlyContain(d =>
                d.Vocabulary == "demographic" &&
                domainService.ServiceTaxonomies.Any(st =>
                    st.Taxonomy.Id == d.Id &&
                    st.Taxonomy.Name == d.Name
                    ));

            responseService.Description.Should().Be(domainService.Description);
            responseService.Images.Original.Should().Be(domainService.Image.Url);

            responseService.Locations.Should().HaveCount(domainService.ServiceLocations.Count);
            responseService.Locations.Should().OnlyContain(l =>
                domainService.ServiceLocations.Any(sl =>
                    (double?) sl.Latitude == l.Latitude &&
                    (double?) sl.Longitude == l.Longitude &&
                    sl.Uprn.ToString() == l.Uprn &&
                    sl.Address1 == l.Address1 &&
                    sl.Address2 == l.Address2 &&
                    sl.City == l.City &&
                    sl.StateProvince == sl.StateProvince &&
                    sl.PostalCode == l.PostalCode &&
                    sl.Country == l.Country &&
                    null == l.Distance
                    ));

            responseService.Organization.Id.Should().Be(domainService.Organization.Id);
            responseService.Organization.Name.Should().Be(domainService.Organization.Name);
            responseService.Organization.Status.Should().Be(domainService.Organization.Status);

            responseService.Referral.Email.Should().Be(domainService.ReferralEmail);
            responseService.Referral.Website.Should().Be(domainService.ReferralLink);

            responseService.Social.Facebook.Should().Be(domainService.Facebook);
            responseService.Social.Instagram.Should().Be(domainService.Instagram);
            responseService.Social.Linkedin.Should().Be(domainService.Linkedin);
            responseService.Social.Twitter.Should().Be(domainService.Twitter);

            responseService.Status.Should().Be(domainService.Status);
        }

        [TestCase(TestName = "Given a service domain object, When Factory ToResponse extension method is called, Then it returns correctly populated GetServiceResponse boundary object.")]
        public void FactoryConvertsServiceDomainToGetServiceResponseBoundary()
        {
            // arrange
            var domainService = EntityHelpers.CreateService().ToDomain(); // would need domain generator, but domain object is messed up now.
            var expectedService = domainService.ToResponseService();

            // act
            var responseBoundary = domainService.ToResponse();

            // assert
            responseBoundary.Service.Should().BeEquivalentTo(expectedService);
            responseBoundary.Metadata.Error.Should().BeNull();
            responseBoundary.Metadata.PostCode.Should().BeNull();
            responseBoundary.Metadata.PostCodeLatitude.Should().BeNull();
            responseBoundary.Metadata.PostCodeLongitude.Should().BeNull();
        }

        [TestCase(TestName = "Given a list of service domain objects, When Factory ToResponse extension method is called, Then it returns correctly populated GetServiceResponseList boundary object.")]
        public void FactoryConvertsAListOfServiceDomainToGetServiceResponseListBoundary()
        {
            // arrange
            var domainServices = EntityHelpers.CreateServices().ToDomain(); // would need domain generator, but domain object is messed up now.
            var expectedServices = domainServices.Select(ds => ds.ToResponseService()).ToList();

            // act
            var responseBoundary = domainServices.ToResponse();

            // assert
            responseBoundary.Services.Should().BeEquivalentTo(expectedServices);
            responseBoundary.Metadata.Error.Should().BeNull();
            responseBoundary.Metadata.PostCode.Should().BeNull();
            responseBoundary.Metadata.PostCodeLatitude.Should().BeNull();
            responseBoundary.Metadata.PostCodeLongitude.Should().BeNull();
        }
    }
}
