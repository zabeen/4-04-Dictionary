using Dictionary.Search;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Dictionary.Tests
{
    public class SearchTest<TSearch> where TSearch : IDictionarySearch, new()
    {
        private readonly List<string> _dictionary;
        private readonly TSearch _search = new TSearch();

        public SearchTest(IEnumerable<string> dictionary)
        {
            _dictionary = dictionary.ToList();
        }

        public void SearchFindsAllWordsPresentInDictionary()
        {
            Parallel.ForEach(_dictionary, word =>
            {
                Assert.IsTrue(_search.FindWordInDictionary(word, _dictionary));
            });
        }

        public void SearchFindsSpecifiedWord(string wordToFind)
        {
            Assert.IsTrue(_search.FindWordInDictionary(wordToFind, _dictionary));
        }
    }
}
