using System;
using System.Collections.Generic;
using System.Linq;
using LBHFSSPublicAPI.V1.Gateways;
using FluentAssertions;
using LBHFSSPublicAPI.V1.Infrastructure;
using NUnit.Framework;
using LBHFSSPublicAPI.Tests.TestHelpers;
using LBHFSSPublicAPI.V1.Boundary.Request;
using LBHFSSPublicAPI.V1.Domain;
using LBHFSSPublicAPI.V1.Factories;

namespace LBHFSSPublicAPI.Tests.V1.Gateways
{
    [TestFixture]
    public class ServicesGatewayTests : DatabaseTests
    {
        private ServicesGateway _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _classUnderTest = new ServicesGateway(DatabaseContext);
        }

        #region Get Single Service by Id

        [TestCase(TestName = "Given a valid id that has a match when the gateway is called the gateway will return a service")]
        public void GivenIdThatHasAMatchWhenGatewayMethodIsCalledThenItReturnsMatchingServiceDomainObject()
        {
            // arrange
            var services = EntityHelpers.CreateServices();
            DatabaseContext.Services.AddRange(services);
            DatabaseContext.SaveChanges();

            var expectedResult = DatabaseContext.Services.First();
            var expectedId = expectedResult.Id;

            // act
            var gatewayResult = _classUnderTest.GetService(expectedId);

            // assert
            gatewayResult.Should().NotBeNull();
            gatewayResult.Should().BeEquivalentTo(expectedResult, options =>
            {
                options.Excluding(ex => ex.ServiceAnalytics);
                return options;
            });
        }

        [TestCase(TestName = "Given an id that does not exist in the database when the GetService method is called it returns null")]
        public void GivenIdThatDoesNotHaveAMatchWhenGetServiceGatewayMethodIsCalledThenItReturnsNull()
        {
            // arrange
            var services = EntityHelpers.CreateServices();
            DatabaseContext.Services.AddRange(services);
            DatabaseContext.SaveChanges();

            var id = Randomm.Id();

            // act
            var gatewayResult = _classUnderTest.GetService(id);

            // assert
            gatewayResult.Should().BeNull();
        }

        [TestCase(TestName = "Given a valid id that has a match when the gateway is called the gateway will add a record to service analytics")]
        public void GivenIdThatHasAMatchWhenGatewayMethodIsCalledThenTheServiceAnalyticsRecordGetsUpdated()
        {
            // arrange
            var services = EntityHelpers.CreateServices();
            DatabaseContext.Services.AddRange(services);
            DatabaseContext.SaveChanges();

            var expectedResult = DatabaseContext.Services.First();
            var expectedId = expectedResult.Id;

            // act
            var gatewayResult = _classUnderTest.GetService(expectedId);

            // assert
            expectedResult.ServiceAnalytics.Count.Should().Be(1);
        }
        #endregion

        #region Search Services
        [TestCase(TestName = "Given search parameters when the SearchService method is called it returns matching records")]
        public void GivenSearchParametersWhenSearchServicesGatewayMethodIsCalledThenItReturnsMatchingResults()
        {
            // arrange
            var services = EntityHelpers.CreateServices(10);
            var searchTerm = Randomm.Text();
            services.First().Name += searchTerm;
            var expectedData = new List<Service>();
            expectedData.Add(services.First());
            var requestParams = new SearchServicesRequest();
            requestParams.Search = searchTerm;
            DatabaseContext.Services.AddRange(services);
            DatabaseContext.SaveChanges();
            // act
            var gatewayResult = _classUnderTest.SearchServices(requestParams);
            var fullMatches = gatewayResult.FullMatchServices;

            // assert
            gatewayResult.Should().NotBeNull();
            fullMatches.Should().NotBeNull();
            fullMatches.Count.Should().Be(1);
        }

        [TestCase(TestName = "When SearchService Service controller method is called, Then it returns all ACTIVE services")] // ignores deleted ones // Assuming no default pagination - there's none yet
        public void SearchServiceEndpointReturnsAllActiveServices() // behaviour does not depend on filters
        {
            // arrange
            var services = EntityHelpers.CreateServices(10).ToList();
            //List<ServiceEntity> expectedData = services.Select(s => s.ToDomain()).ToList();

            var deletedService = EntityHelpers.CreateService();
            deletedService.Status = "deleted";
            deletedService.OrganizationId = null;
            deletedService.Organization = null;
            services.Add(deletedService);                           // added 11th service (broken)

            DatabaseContext.Services.AddRange(services);
            DatabaseContext.SaveChanges();

            // act
            var gatewayResult = _classUnderTest.SearchServices(new SearchServicesRequest());
            var fullMatches = gatewayResult.FullMatchServices;
            var splitMatches = gatewayResult.SplitMatchServices;
            var returnedServices = fullMatches.Concat(splitMatches).ToList();

            // assert
            gatewayResult.Should().NotBeNull();
            fullMatches.Should().NotBeNull();
            splitMatches.Should().NotBeNull();

            fullMatches.Should().BeOfType<List<ServiceEntity>>();
            splitMatches.Should().BeOfType<List<ServiceEntity>>();

            returnedServices.Should().NotContain(s => s.Status == "deleted");
            returnedServices.Count.Should().Be(10);
        }

        [TestCase(TestName = "Given user search input That does not have a match But its split part's synonym does, When SearchService gateway method is called, Then it returns records matching related synonym group And stored under splitMatchServices field.")]
        public void EvenIfWholeOrSplitUserInputDoesNotHaveAMatchThenSplitInputSynonymMatchesAreReturned()
        {
            // arrange
            var searchWord1 = Randomm.Word();                            // word that won't match any results, however 1 of its synonyms from synonym group will
            var searchWord2 = Randomm.Word();                            // same as above, but the matching synonym will be in another synonym group
            var irrelevantWord = Randomm.Word();                         // a control word, that won't match anything, nor its synonyms will.

            var userInput = $"{searchWord1} "
                          + $"{irrelevantWord} "
                          + $"{searchWord2}";

            var request = new SearchServicesRequest();
            request.Search = userInput;

            var bridgeSyn1Word = Utility.SuperSetOfString(searchWord1);     // A superset word of search word 1 that will relate to a synonym word inside synonym group 1 - this word has no match in the DB
            var bridgeSyn2Word = Utility.SuperSetOfString(searchWord2);     // A superset word of search word 2 that will relate to a synonym word inside synonym group 2 - this word has no match in the DB


            var synWord1 = Randomm.Word();                            // synonym within the same synonym group (1) as a word related to search word 1 - this word has a match in the DB
            var synWord2 = Randomm.Word();                            // synonym within the same synonym group (1) as a word related to search word 1 - this word has a match in the DB
            var synWord3 = Randomm.Word();                            // synonym within the same synonym group (2) as a word related to search word 2 - this word has a match in the DB


            var synonymGroup1 = EntityHelpers.CreateSynonymGroupWithWords();            // relevant group with dummy synonym words
            var synonymGroup2 = EntityHelpers.CreateSynonymGroupWithWords();            // relevant group with dummy synonym words
            var dummySynGroup = EntityHelpers.CreateSynonymGroupWithWords();            // dummy synonym group that should not be picked up

            var bridgeSynonym1 = EntityHelpers.SynWord(synonymGroup1, bridgeSyn1Word);  // synonym that has no match in DB, however it bridges user input search word with the synonym group 1
            var bridgeSynonym2 = EntityHelpers.SynWord(synonymGroup2, bridgeSyn2Word);

            var matchSynonym1 = EntityHelpers.SynWord(synonymGroup1, synWord1);         // creating a synonym word object to insert that will have a match, creating a link with synonym group 1
            var matchSynonym2 = EntityHelpers.SynWord(synonymGroup1, synWord2);
            var matchSynonym3 = EntityHelpers.SynWord(synonymGroup2, synWord3);


            synonymGroup1.SynonymWords.Add(bridgeSynonym1);                  // added bridge synonym to the synonym group
            synonymGroup2.SynonymWords.Add(bridgeSynonym2);

            synonymGroup1.SynonymWords.Add(matchSynonym1);                   // added match synonym into a synonym group
            synonymGroup1.SynonymWords.Add(matchSynonym2);
            synonymGroup2.SynonymWords.Add(matchSynonym3);


            var services = EntityHelpers.CreateServices(5);                  // creating list of dummy services that should not be found

            var matchService1 = EntityHelpers.CreateService();               // service that is intended to be found through the synonym of synonym group 1
            var matchService2 = EntityHelpers.CreateService();               // service that is intended to be found through the synonym of synonym group 1
            var matchService3 = EntityHelpers.CreateService();               // service that is intended to be found through the synonym of synonym group 2

            matchService1.Name += synWord1;                                  // creating a link between a service and a match synonym 1
            matchService2.Name += synWord2;                                  // creating a link between a service and a match synonym 2
            matchService3.Name += synWord3;                                  // creating a link between a service and a match synonym 3

            services.AddMany(matchService1, matchService2, matchService3);   // include match services into a to be inserted services collection

            DatabaseContext.SynonymGroups.AddRange(synonymGroup1);           // adding synonym groups containing synonym words into a database
            DatabaseContext.SynonymGroups.AddRange(synonymGroup2);
            DatabaseContext.SynonymGroups.AddRange(dummySynGroup);
            DatabaseContext.Services.AddRange(services);                     // adding services into a database

            DatabaseContext.SaveChanges();

            // act
            var gatewayResult = _classUnderTest.SearchServices(request);
            var splitMatches = gatewayResult.SplitMatchServices;
            var fullMatches = gatewayResult.FullMatchServices;

            // assert
            splitMatches.Should().Contain(s => s.Name.Contains(synWord1));
            splitMatches.Should().Contain(s => s.Name.Contains(synWord2));
            splitMatches.Should().Contain(s => s.Name.Contains(synWord3));
            splitMatches.Should().HaveCount(3);
            fullMatches.Should().HaveCount(0);
        }

        [TestCase(TestName = "Given search parameters when the SearchService method is called it returns records matching applied synonym group")]
        public void GivenSearchParametersWhenSearchServicesGatewayMethodIsCalledThenItReturnsMatchingSynonymGroupResults()
        {
            // arrange
            var synonymGroup1 = EntityHelpers.CreateSynonymGroupWithWords(5);
            var synonymGroup2 = EntityHelpers.CreateSynonymGroupWithWords(3);
            synonymGroup2.SynonymWords.ToList()[1].Word = synonymGroup1.SynonymWords.ToList()[1].Word;
            var services = EntityHelpers.CreateServices();
            services.ForEach(s => s.Name = "irrelevant");
            var serviceToFind1 = EntityHelpers.CreateService();
            var serviceToFind2 = EntityHelpers.CreateService();
            var searchTerm = synonymGroup1.SynonymWords.ToList()[1].Word;
            var requestParams = new SearchServicesRequest();
            requestParams.Search = searchTerm;
            serviceToFind1.Name += synonymGroup1.SynonymWords.ToList()[4].Word;
            serviceToFind2.Name += synonymGroup2.SynonymWords.ToList()[2].Word;
            DatabaseContext.SynonymGroups.Add(synonymGroup1);
            DatabaseContext.SynonymGroups.Add(synonymGroup2);
            DatabaseContext.Services.AddRange(services);
            DatabaseContext.Services.Add(serviceToFind1);
            DatabaseContext.Services.Add(serviceToFind2);
            DatabaseContext.SaveChanges();
            var expectedData = new List<Service>();
            expectedData.Add(serviceToFind1);
            expectedData.Add(serviceToFind2);

            // act
            var gatewayResult = _classUnderTest.SearchServices(requestParams);
            var fullMatches = gatewayResult.FullMatchServices;
            var splitMatches = gatewayResult.SplitMatchServices;
            var returnedServices = fullMatches.Concat(splitMatches).ToList();

            // assert
            gatewayResult.Should().NotBeNull();
            fullMatches.Should().NotBeNull();
            splitMatches.Should().NotBeNull();

            returnedServices.Count.Should().Be(expectedData.Count);
        }

        [TestCase(TestName = "Given user provided search term consisting of multiple words, When services get filtered in SearchService method, Then the returned result includes service matches of individual search term words.")]
        public void UponFilteringServicesByAMultiWordInputTheReturnedResultsIncludePartialMatches()
        {
            // arrange
            var word1 = Randomm.Word();
            var word2 = Randomm.Word();
            var userSearchInput = $"{word1} {word2}";

            var request = new SearchServicesRequest() { Search = userSearchInput };

            var services = EntityHelpers.CreateServices(5).ToList();            // dummy services
            var serviceToFind1 = EntityHelpers.CreateService();                 // full match
            serviceToFind1.Name += userSearchInput;
            var serviceToFind2 = EntityHelpers.CreateService();                 // word 1 match
            serviceToFind2.Name += word1;
            var serviceToFind3 = EntityHelpers.CreateService();                 // word 2 match
            serviceToFind3.Name += word2;

            services.Add(serviceToFind1);
            services.Add(serviceToFind2);
            services.Add(serviceToFind3);
            DatabaseContext.Services.AddRange(services);
            DatabaseContext.SaveChanges();

            // act
            var gatewayResult = _classUnderTest.SearchServices(request);
            var fullMatches = gatewayResult.FullMatchServices;
            var splitMatches = gatewayResult.SplitMatchServices;
            var returnedServices = fullMatches.Concat(splitMatches).ToList();

            // assert
            gatewayResult.Should().NotBeNull();
            fullMatches.Should().NotBeNull();
            splitMatches.Should().NotBeNull();

            returnedServices.Should().Contain(s => s.Name.Contains(userSearchInput, StringComparison.OrdinalIgnoreCase));
            returnedServices.Should().Contain(s => s.Name.Contains(word1, StringComparison.OrdinalIgnoreCase));
            returnedServices.Should().Contain(s => s.Name.Contains(word2, StringComparison.OrdinalIgnoreCase));
            returnedServices.Should().HaveCount(3);
        }

        [TestCase(TestName = "Given user provided search term consisting of multiple words, When services get filtered in SearchService method, Then the returned services are categorized into Full user input match Or Split match.")] // done so to ensure the less relevant services are in the separate collection
        public void SearchServiceGatewaySeparatesOutFullMatchResultsFromSplitMatch()
        {
            // arrange
            var word1 = Randomm.Word();
            var word2 = Randomm.Word();
            var userSearchInput = $"{word1} {word2}";

            var request = new SearchServicesRequest() { Search = userSearchInput };

            var services = new List<Service>();
            var serviceToFind1 = EntityHelpers.CreateService();                 // full match
            serviceToFind1.Name += userSearchInput;
            var serviceToFind2 = EntityHelpers.CreateService();                 // split match 1
            serviceToFind2.Name += word1;
            var serviceToFind3 = EntityHelpers.CreateService();                 // split match 2
            serviceToFind3.Name += word2;

            services.Add(serviceToFind1);
            services.Add(serviceToFind2);
            services.Add(serviceToFind3);
            DatabaseContext.Services.AddRange(services);
            DatabaseContext.SaveChanges();

            // act
            var gatewayResult = _classUnderTest.SearchServices(request);
            var fullMatches = gatewayResult.FullMatchServices;
            var splitMatches = gatewayResult.SplitMatchServices;

            // assert
            gatewayResult.Should().NotBeNull();
            fullMatches.Should().NotBeNull();
            splitMatches.Should().NotBeNull();

            fullMatches.Should().Contain(s => s.Name.Contains(userSearchInput, StringComparison.OrdinalIgnoreCase));
            fullMatches.Should().HaveCount(1);

            splitMatches.Should().Contain(s => s.Name.Contains(word1, StringComparison.OrdinalIgnoreCase));
            splitMatches.Should().Contain(s => s.Name.Contains(word2, StringComparison.OrdinalIgnoreCase));
            splitMatches.Should().NotContain(s => s.Name.Contains(userSearchInput, StringComparison.OrdinalIgnoreCase));
            splitMatches.Should().HaveCount(2);
        }

        [TestCase(TestName = "Given user provided search term consisting of multiple words, When services get filtered in SearchService method, Then the returned result does not include service matches of individual search term words that consist of 3 or less characters.")]
        public void WhenDoingPartialTextSearchMatchesWordsOf3orLessCharactersLongGetIgnored()  //it's too short - these will be searched and found inside other words in DB
        {
            // arrange
            var shortWordList = new List<string> { "and", "a", "an", "the", "bfg", "42" };
            var shortWord = shortWordList.RandomItem();

            var word = Randomm.Word().Replace(shortWord, "test");// have to ensure the shortword is not contained in the actual word for the sake of test
            var userSearchInput = $"{shortWord} {word}";

            var request = new SearchServicesRequest() { Search = userSearchInput };

            var services = EntityHelpers.CreateServices(5).ToList();//dummy services
            services.ForEach(s => s.Name = s.Name.Replace(word, "ssj"));//make sure they don't match the search word
            //assuming there's no full match. due to full match containing a shortword, the assertion at the bottom wouldn't be able to test what's needed.
            var serviceToFind = EntityHelpers.CreateService();// word 1 match
            serviceToFind.Name = serviceToFind.Name.Replace(shortWord, "test");//ensuring random hash does not contain shortword. for the assertion bellow to work as intended, the service name's hash part should not contain shortword.
            serviceToFind.Name += word;

            var serviceToNotFind = EntityHelpers.CreateService();                   // shortword no match. this ensures that the test can fail if the implementation is wrong or not present.
            serviceToNotFind.Name = serviceToNotFind.Name.Replace(word, "1234");    // make sure the mismatching service does not contain a desired search term
            serviceToNotFind.Name += shortWord;

            services.Add(serviceToFind);
            services.Add(serviceToNotFind);
            DatabaseContext.Services.AddRange(services);
            DatabaseContext.SaveChanges();

            // act
            var gatewayResult = _classUnderTest.SearchServices(request);
            var splitMatches = gatewayResult.SplitMatchServices;

            // assert
            gatewayResult.Should().NotBeNull();
            splitMatches.Should().NotBeNull();

            splitMatches.Should().NotContain(s => s.Name.Contains(shortWord, StringComparison.OrdinalIgnoreCase));
            splitMatches.Should().HaveCount(1);
        }

        [TestCase(TestName = "Given multiple taxonomy id search parameters when the SearchService method is called it returns records matching taxonomy ids")]
        public void GivenMultipleTaxonomyIdSearchParametersWhenSearchServicesGatewayMethodIsCalledThenItReturnsMatchingTaxonomyIdResults()
        {
            var taxonomy1 = EntityHelpers.CreateTaxonomy();
            var taxonomy2 = EntityHelpers.CreateTaxonomy();
            taxonomy1.Vocabulary = "demographic";
            taxonomy2.Vocabulary = "category";
            var services = EntityHelpers.CreateServices();
            var serviceToFind1 = EntityHelpers.CreateService();
            var serviceToFind2 = EntityHelpers.CreateService();
            var serviceTaxonomy1 = EntityHelpers.CreateServiceTaxonomy();
            var serviceTaxonomy2 = EntityHelpers.CreateServiceTaxonomy();
            var serviceTaxonomy3 = EntityHelpers.CreateServiceTaxonomy();
            var serviceTaxonomy4 = EntityHelpers.CreateServiceTaxonomy();
            serviceTaxonomy1.Service = serviceToFind1;
            serviceTaxonomy1.Taxonomy = taxonomy1;
            serviceTaxonomy2.Service = serviceToFind1;
            serviceTaxonomy2.Taxonomy = taxonomy2;
            serviceTaxonomy3.Service = serviceToFind2;
            serviceTaxonomy3.Taxonomy = taxonomy1;
            serviceTaxonomy4.Service = serviceToFind2;
            serviceTaxonomy4.Taxonomy = taxonomy2;
            DatabaseContext.Services.AddRange(services);
            DatabaseContext.Services.Add(serviceToFind1);
            DatabaseContext.Services.Add(serviceToFind2);
            DatabaseContext.ServiceTaxonomies.Add(serviceTaxonomy1);
            DatabaseContext.ServiceTaxonomies.Add(serviceTaxonomy2);
            DatabaseContext.ServiceTaxonomies.Add(serviceTaxonomy3);
            DatabaseContext.ServiceTaxonomies.Add(serviceTaxonomy4);
            DatabaseContext.SaveChanges();
            var requestParams = new SearchServicesRequest();
            requestParams.TaxonomyIds = new List<int> { taxonomy1.Id, taxonomy2.Id };
            var expectedData = new List<Service>();
            expectedData.Add(serviceToFind1);
            expectedData.Add(serviceToFind2);
            var gatewayResult = _classUnderTest.SearchServices(requestParams);
            var fullMatches = gatewayResult.FullMatchServices;
            gatewayResult.Should().NotBeNull();
            fullMatches.Should().NotBeNull();
            fullMatches.Count.Should().Be(expectedData.Count);
        }

        [TestCase(TestName = "Given that there are services in the database, if either category or demographic taxonomy id search parameters is provided services with matching taxonomy are returned")]
        public void GivenSingleTaxonomyIdSearchParametersWhenSearchServicesGatewayMethodIsCalledThenItReturnsResults()
        {
            var taxonomy1 = EntityHelpers.CreateTaxonomy();
            var taxonomy2 = EntityHelpers.CreateTaxonomy();
            var taxonomy3 = EntityHelpers.CreateTaxonomy();
            taxonomy1.Vocabulary = "demographic";
            taxonomy2.Vocabulary = "category";
            taxonomy3.Vocabulary = "demographic";
            var services = EntityHelpers.CreateServices();
            var serviceToFind1 = EntityHelpers.CreateService();
            var serviceToFind2 = EntityHelpers.CreateService();
            var serviceTaxonomy1 = EntityHelpers.CreateServiceTaxonomy();
            var serviceTaxonomy2 = EntityHelpers.CreateServiceTaxonomy();
            serviceTaxonomy1.Service = serviceToFind1;
            serviceTaxonomy1.Taxonomy = taxonomy1;
            serviceTaxonomy2.Service = serviceToFind2;
            serviceTaxonomy2.Taxonomy = taxonomy1;
            DatabaseContext.Services.AddRange(services);
            DatabaseContext.Services.Add(serviceToFind1);
            DatabaseContext.Services.Add(serviceToFind2);
            DatabaseContext.ServiceTaxonomies.Add(serviceTaxonomy1);
            DatabaseContext.ServiceTaxonomies.Add(serviceTaxonomy2);
            DatabaseContext.Taxonomies.Add(taxonomy3);
            DatabaseContext.SaveChanges();
            var requestParams = new SearchServicesRequest();
            requestParams.TaxonomyIds = new List<int> { taxonomy1.Id };
            var expectedData = new List<Service>();
            expectedData.Add(serviceToFind1);
            expectedData.Add(serviceToFind2);
            var gatewayResult = _classUnderTest.SearchServices(requestParams);
            var fullMatches = gatewayResult.FullMatchServices;
            gatewayResult.Should().NotBeNull();
            fullMatches.Should().NotBeNull();
            fullMatches.Count.Should().Be(expectedData.Count);
        }

        [TestCase(TestName =
            "Given that there are services in the database, if category and demographic taxonomy id search parameters are provided and no services match both, nothing is returned")]
        public void GivenTaxonomyIdSearchParametersWhenSearchServicesGatewayMethodIsCalledThenItReturnsNothing()
        {
            var taxonomy1 = EntityHelpers.CreateTaxonomy();
            var taxonomy2 = EntityHelpers.CreateTaxonomy();
            var taxonomy3 = EntityHelpers.CreateTaxonomy();
            taxonomy1.Vocabulary = "demographic";
            taxonomy2.Vocabulary = "category";
            taxonomy3.Vocabulary = "demographic";
            var services = EntityHelpers.CreateServices();
            var serviceToFind1 = EntityHelpers.CreateService();
            var serviceToFind2 = EntityHelpers.CreateService();
            var serviceTaxonomy1 = EntityHelpers.CreateServiceTaxonomy();
            var serviceTaxonomy2 = EntityHelpers.CreateServiceTaxonomy();
            serviceTaxonomy1.Service = serviceToFind1;
            serviceTaxonomy1.Taxonomy = taxonomy1;
            serviceTaxonomy2.Service = serviceToFind2;
            serviceTaxonomy2.Taxonomy = taxonomy2;
            DatabaseContext.Services.AddRange(services);
            DatabaseContext.Services.Add(serviceToFind1);
            DatabaseContext.Services.Add(serviceToFind2);
            DatabaseContext.ServiceTaxonomies.Add(serviceTaxonomy1);
            DatabaseContext.ServiceTaxonomies.Add(serviceTaxonomy2);
            DatabaseContext.Taxonomies.Add(taxonomy3);
            DatabaseContext.SaveChanges();
            var requestParams = new SearchServicesRequest();
            requestParams.TaxonomyIds = new List<int> { taxonomy2.Id, taxonomy3.Id };
            var gatewayResult = _classUnderTest.SearchServices(requestParams);
            var fullMatches = gatewayResult.FullMatchServices;
            gatewayResult.Should().NotBeNull();
            fullMatches.Should().NotBeNull();
            fullMatches.Count.Should().Be(0);
        }

        [TestCase(TestName = "Given search parameters when the SearchService method is called it returns records matching service name or description")]
        public void GivenSearchParametersWhenSearchServicesGatewayMethodIsCalledThenItReturnsResultsMatchingNameAndDescription()
        {
            // arrange
            var services = EntityHelpers.CreateServices(10);
            var searchTerm = Randomm.Text();

            searchTerm = " " + searchTerm;//15 Feb 2021 - Change made so we only search for whole words in the service description! - So we add a space.

            services.First().Name += searchTerm;
            services[1].Description += searchTerm;
            var expectedData = new List<Service>();
            expectedData.Add(services.First());
            expectedData.Add(services[1]);
            var requestParams = new SearchServicesRequest();
            requestParams.Search = searchTerm;
            DatabaseContext.Services.AddRange(services);
            DatabaseContext.SaveChanges();

            // act
            var gatewayResult = _classUnderTest.SearchServices(requestParams);
            var fullMatches = gatewayResult.FullMatchServices;

            // assert
            gatewayResult.Should().NotBeNull();
            fullMatches.Should().NotBeNull();
            fullMatches.Count.Should().Be(2);
        }

        [TestCase(TestName = "Given search parameters when the SearchService method is called it returns matching records with organisation name ranked higher than service name")]
        public void GivenSearchParametersWhenSearchServicesGatewayMethodIsCalledThenItReturnsMatchingRecordsWithOrgNameHigherThanServiceName()
        {
            // arrange
            var services = EntityHelpers.CreateServices(10);
            var searchTerm = Randomm.Text();
            services.First().Name += searchTerm;
            services[1].Organization.Name += searchTerm;
            var expectedData = new List<Service>();
            expectedData.Add(services.First());
            expectedData.Add(services[1]);
            var requestParams = new SearchServicesRequest();
            requestParams.Search = searchTerm;
            DatabaseContext.Services.AddRange(services);
            DatabaseContext.SaveChanges();

            // act
            var gatewayResult = _classUnderTest.SearchServices(requestParams);
            var fullMatches = gatewayResult.FullMatchServices;

            // assert
            gatewayResult.Should().NotBeNull();
            fullMatches.Should().NotBeNull();
            fullMatches.Count.Should().Be(2);
            fullMatches[0].Name.Should().Be(services[1].Name);
            fullMatches[1].Name.Should().Be(services[0].Name);
        }

        [TestCase(TestName = "Given user search input that include split part's synonym matches, When SearchService gateway method is called, the split part synonym matches are returned in the appropriate rank.")]
        public void WholeAndSplitUserInputMatchesAreReturnedInCorrectRank()
        {
            // arrange
            var searchWord1 = Randomm.Word();                            // word that won't match any results, however 1 of its synonyms from synonym group will
            var searchWord2 = Randomm.Word();                            // same as above, but the matching synonym will be in another synonym group
            var irrelevantWord = Randomm.Word();                         // a control word, that won't match anything, nor its synonyms will.

            var userInput = $"{searchWord1} "
                          + $"{irrelevantWord} "
                          + $"{searchWord2}";

            var request = new SearchServicesRequest();
            request.Search = userInput;

            var bridgeSyn1Word = Utility.SuperSetOfString(searchWord1);     // A superset word of search word 1 that will relate to a synonym word inside synonym group 1 - this word has no match in the DB
            var bridgeSyn2Word = Utility.SuperSetOfString(searchWord2);     // A superset word of search word 2 that will relate to a synonym word inside synonym group 2 - this word has no match in the DB


            var synWord1 = Randomm.Word();                            // synonym within the same synonym group (1) as a word related to search word 1 - this word has a match in the DB
            var synWord2 = Randomm.Word();                            // synonym within the same synonym group (1) as a word related to search word 1 - this word has a match in the DB
            var synWord3 = Randomm.Word();                            // synonym within the same synonym group (2) as a word related to search word 2 - this word has a match in the DB


            var synonymGroup1 = EntityHelpers.CreateSynonymGroupWithWords();            // relevant group with dummy synonym words
            var synonymGroup2 = EntityHelpers.CreateSynonymGroupWithWords();            // relevant group with dummy synonym words
            var dummySynGroup = EntityHelpers.CreateSynonymGroupWithWords();            // dummy synonym group that should not be picked up

            var bridgeSynonym1 = EntityHelpers.SynWord(synonymGroup1, bridgeSyn1Word);  // synonym that has no match in DB, however it bridges user input search word with the synonym group 1
            var bridgeSynonym2 = EntityHelpers.SynWord(synonymGroup2, bridgeSyn2Word);

            var matchSynonym1 = EntityHelpers.SynWord(synonymGroup1, synWord1);         // creating a synonym word object to insert that will have a match, creating a link with synonym group 1
            var matchSynonym2 = EntityHelpers.SynWord(synonymGroup1, synWord2);
            var matchSynonym3 = EntityHelpers.SynWord(synonymGroup2, synWord3);


            synonymGroup1.SynonymWords.Add(bridgeSynonym1);                  // added bridge synonym to the synonym group
            synonymGroup2.SynonymWords.Add(bridgeSynonym2);

            synonymGroup1.SynonymWords.Add(matchSynonym1);                   // added match synonym into a synonym group
            synonymGroup1.SynonymWords.Add(matchSynonym2);
            synonymGroup2.SynonymWords.Add(matchSynonym3);


            var services = EntityHelpers.CreateServices(5);                  // creating list of dummy services that should not be found

            var matchService1 = EntityHelpers.CreateService();               // service that is intended to be found through the synonym of synonym group 1
            var matchService2 = EntityHelpers.CreateService();               // service that is intended to be found through the synonym of synonym group 1
            var matchService3 = EntityHelpers.CreateService();               // service that is intended to be found through the synonym of synonym group 2
            var matchService4 = EntityHelpers.CreateService();               // service that is intended to be found through the main search term

            matchService1.Name += searchWord2;                                  // creating a link between a service and a match synonym 1
                                                                                // creating a link between a service and a match synonym 2
            matchService2.Description += " " + synWord2;//15 Feb 2021 - Change made so we only search for whole words in the service description! - So we add a space.

            matchService3.Organization.Name += synWord3;                     // creating a link between a service and a match synonym 3
            matchService4.Organization.Name += searchWord1;                  // creating a link between a service and a main search word

            services.AddMany(matchService1, matchService2, matchService3, matchService4);   // include match services into a to be inserted services collection

            DatabaseContext.SynonymGroups.AddRange(synonymGroup1);           // adding synonym groups containing synonym words into a database
            DatabaseContext.SynonymGroups.AddRange(synonymGroup2);
            DatabaseContext.SynonymGroups.AddRange(dummySynGroup);
            DatabaseContext.Services.AddRange(services);                     // adding services into a database

            DatabaseContext.SaveChanges();

            // act
            var gatewayResult = _classUnderTest.SearchServices(request);
            var splitMatches = gatewayResult.SplitMatchServices;
            var fullMatches = gatewayResult.FullMatchServices;

            // assert
            splitMatches.Should().HaveCount(4);
            fullMatches.Should().HaveCount(0);
            splitMatches[0].Name.Should().Be(matchService4.Name);
            splitMatches[1].Name.Should().Be(matchService1.Name);
            splitMatches[2].Name.Should().Be(matchService3.Name);
            splitMatches[3].Name.Should().Be(matchService2.Name);
        }
        #endregion
    }
}
