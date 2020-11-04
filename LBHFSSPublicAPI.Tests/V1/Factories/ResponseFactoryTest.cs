using FluentAssertions;
using LBHFSSPublicAPI.Tests.TestHelpers;
using Response = LBHFSSPublicAPI.V1.Boundary.Response;
using LBHFSSPublicAPI.V1.Domain;
using LBHFSSPublicAPI.V1.Factories;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;

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
                    sl.Uprn == l.Uprn &&
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

        // No need to test the ToResponseServices (Converting List of domain to List of Reponse) because it's a basic Linq query consisting of
        // 1 Select statement. At that point, it would be a test of Microsoft's 'Select' implementation rather than a test of a method (due to it being a wrapper)

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

        [TestCase(TestName = "Given 2 non-empty lists of service response objects, When Factory SearchServiceUsecaseResponse method is called, Then it returns a GetServiceResponseList boundary object with those 2 lists concatinated AND metadata fields set to null.")]
        public void FactoryConverts2NonEmptyListOfServiceResponseToGetServiceResponseListBoundary()
        {
            // arrange
            var gatewayResponse = Randomm.SSGatewayResult();
            var fullMatchServices = gatewayResponse.FullMatchServices.ToResponseServices();
            var splitMatchServices = gatewayResponse.SplitMatchServices.ToResponseServices();

            var expectedServicesList = fullMatchServices.Concat(splitMatchServices);

            // act
            var factoryResult = ServiceFactory.SearchServiceUsecaseResponse(fullMatchServices, splitMatchServices);

            // assert
            factoryResult.Services.Should().BeEquivalentTo(expectedServicesList);
            factoryResult.Metadata.Error.Should().BeNull();
            factoryResult.Metadata.PostCode.Should().BeNull();
            factoryResult.Metadata.PostCodeLatitude.Should().BeNull();
            factoryResult.Metadata.PostCodeLongitude.Should().BeNull();
        }

        [TestCase(TestName = "Given 1 empty and 1 non-empty list of service response objects, When Factory SearchServiceUsecaseResponse method is called, Then it returns a GetServiceResponseList boundary object with a list containing all the elements from the non-empty list.")] // essentially a check that it doesn't fall over under these circumstances
        public void FactoryConverts1EmptyAnd1NonEmptyListOfServiceResponseToGetServiceResponseListBoundary()
        {
            // arrange
            var gatewayResponse = Randomm.SSGatewayResult();
            var fullMatchServices = gatewayResponse.FullMatchServices.ToResponseServices();
            var splitMatchServices = gatewayResponse.SplitMatchServices.ToResponseServices();
            var emptyList = new List<Response.Service>();

            // act
            var factoryResult1 = ServiceFactory.SearchServiceUsecaseResponse(fullMatchServices, emptyList);
            var factoryResult2 = ServiceFactory.SearchServiceUsecaseResponse(emptyList, splitMatchServices);

            // assert
            factoryResult1.Services.Should().BeEquivalentTo(fullMatchServices);
            factoryResult2.Services.Should().BeEquivalentTo(splitMatchServices);
        }

        [TestCase(TestName = "Given 2 non-empty lists of service response objects, When Factory SearchServiceUsecaseResponse method is called, Then it returns a GetServiceResponseList boundary object with those 2 lists concatinated AND metadata fields set to null.")] // essentially a check that it doesn't fall over under these circumstances
        public void FactoryConverts2EmptyListOfServiceResponseToGetServiceResponseListBoundary()
        {
            // arrange
            var emptyList1 = new List<Response.Service>();
            var emptyList2 = new List<Response.Service>();
            var emptyList3 = new List<Response.Service>();                                  // for the sake of different object refs

            // act
            var factoryResult = ServiceFactory.SearchServiceUsecaseResponse(emptyList1, emptyList2);

            // assert
            factoryResult.Services.Should().BeEquivalentTo(emptyList3);
        }
    }
}
