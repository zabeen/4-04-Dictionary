using System.Collections.Generic;
using Dictionary.Dictionary;
using Dictionary.Search;
using NUnit.Framework;
using System.Linq;

namespace Dictionary.Tests
{
    [TestFixture]
    public class SearchTestRunner
    {
        private readonly List<string> _dictionary;

        public SearchTestRunner()
        {
            _dictionary = new DictionaryCreator()
                .DownloadDictionary(@"https://raw.githubusercontent.com/tomprogers/english-words/tpr/24-sort-words/words.txt")
                .ToList();
        }

        [Test]
        public void BinarySearchCanFindAllWordsInDictionary()
        {          
            var test = new SearchTest<BinarySearch>(_dictionary);
            test.SearchFindsAllWordsPresentInDictionary();
        }

        [Test]
        public void BinarySearchCanFindSpecifiedWord()
        {
            var test = new SearchTest<BinarySearch>(_dictionary);
            test.SearchFindsSpecifiedWord("archetypes");
        }
    }
}
