using System.Collections.Generic;
using FluentAssertions;
using LBHFSSPublicAPI.Tests.TestHelpers;
using LBHFSSPublicAPI.V1.Helpers;
using NUnit.Framework;

namespace LBHFSSPublicAPI.Tests.V1.Helper
{
    /// <summary>
    /// The purpose of these tests will test the search ability of the string extension ContainsWord and the
    /// enumerable extension, AnyWord. These are both found in
    /// </summary>
    [TestFixture]
    public class ExtensionTest
    {
        [TestCase(TestName = "Given a search keyword is given, a match of the whole word returns true using ContainsWord string extension.")]
        public void SearchWordIsFoundInText()
        {
            const string searchTerm = "rent";
            const string searchTextFound = "We manage mortgage and rent arrears as a service";//Contains rent as a whole word
            var resultSuccess = searchTextFound.ContainsWord(searchTerm);
            resultSuccess.Should().Be(true);
        }

        [TestCase(TestName = "Given a search UPPERCASE keyword is given, a match of the whole word returns true using ContainsWord string extension.")]
        public void SearchWordIsFoundInText2()
        {
            const string searchTerm = "RENT";
            const string searchTextFound = "We manage mortgage and rent arrears as a service";//Contains rent as a whole word
            var resultSuccess = searchTextFound.ContainsWord(searchTerm);
            resultSuccess.Should().Be(true);
        }

        [TestCase(TestName = "Given a search mixed CASE keyword is given, a match of the whole word returns true using ContainsWord string extension.")]
        public void SearchWordIsFoundInText3()
        {
            const string searchTerm = "REnt";
            const string searchTextFound = "We manage mortgage and rent, and arrears as a service";//Contains rent as a whole word
            var resultSuccess = searchTextFound.ContainsWord(searchTerm);
            resultSuccess.Should().Be(true);
        }

        [TestCase(TestName = "Given a search mixed CASE keyword is given in the search text, a match of the whole word returns true using ContainsWord string extension.")]
        public void SearchWordIsFoundInText4()
        {
            const string searchTerm = "rent";
            const string searchTextFound = "We manage mortgage and REnt, and arrears as a service";//Contains rent as a whole word
            var resultSuccess = searchTextFound.ContainsWord(searchTerm);
            resultSuccess.Should().Be(true);
        }

        [TestCase(TestName = "Given a search keyword is given, a match of the whole word returns false when its part of another word using ContainsWord string extension.")]
        public void SearchWordIsNotFoundInText()
        {
            const string searchTerm = "rent";
            const string searchTextNotFound = "We currently provide a good service";//Contains rent as part of a word
            var resultFailure = searchTextNotFound.ContainsWord(searchTerm);
            resultFailure.Should().Be(false);
        }

        [TestCase(TestName = "Given a search keyword is given, a match of the whole word returns false when its round at the start of another word using ContainsWord string extension.")]
        public void SearchWordIsNotFoundInText2()
        {
            const string searchTerm = "rent";
            const string searchTextNotFound = "We provide a rental service";//Contains rent as part of a word
            var resultFailure = searchTextNotFound.ContainsWord(searchTerm);
            resultFailure.Should().Be(false);
        }

        [TestCase(TestName = "Given a search keyword is given, a match of the whole word returns true using ContainsWord string extension when the word is placed at the end.")]
        public void SearchWordIsFoundAtEndInText()
        {
            const string searchTerm = "rent";
            const string searchTextFound = "We provide services to rent.";//Contains rent as a whole word
            var resultSuccess = searchTextFound.ContainsWord(searchTerm);
            resultSuccess.Should().Be(true);
        }

        [TestCase(TestName = "Given a set of synonyms is given, a match of the whole word returns true using AnyWord enumerable extension.")]
        public void SearchSynonymIsFoundInText()
        {
            List<string> synonyms = new List<string>() { "money", "rent", "income" };
            const string searchTextFound = "We manage mortgage and rent arrears as a service";//contains rent whole word
            var resultSuccess = synonyms.AnyWord(searchTextFound);
            resultSuccess.Should().Be(true);
        }

        [TestCase(TestName = "Given a set of synonyms is given, a match of the whole word returns false when its part of another word using AnyWord enumerable extension.")]
        public void SearchSynonymIsNotFoundInText()
        {
            List<string> synonyms = new List<string>() { "money", "rent", "income" };
            const string searchTextNotFound = "We currently provide a good service";//contains rent as part of a word
            var resultFailure = synonyms.AnyWord(searchTextNotFound);
            resultFailure.Should().Be(false);
        }

        [TestCase(TestName = "Given a set of synonyms is given, a match of the whole word returns false when its the start of another word using AnyWord enumerable extension.")]
        public void SearchSynonymIsNotFoundInText2()
        {
            List<string> synonyms = new List<string>() { "money", "rent", "income" };
            const string searchTextNotFound = "We currently provide a rental service";//contains rent as part of a word
            var resultFailure = synonyms.AnyWord(searchTextNotFound);
            resultFailure.Should().Be(false);
        }

        [TestCase(TestName = "Given a set of synonyms is given, a match of the whole word returns false when its the start of another word using AnyWord enumerable extension when the word is placed at the end.")]
        public void SearchSynonymIsNotFoundInText3()
        {
            List<string> synonyms = new List<string>() { "money", "rent", "income" };
            const string searchTextNotFound = "We provide services to rent.";//contains rent as part of a word
            var resultFailure = synonyms.AnyWord(searchTextNotFound);
            resultFailure.Should().Be(true);
        }
    }
}
