using System.Collections.Generic;

namespace Dictionary.Search
{
    public interface IDictionarySearch
    {
        bool FindWordInDictionary(string wordToFind, IEnumerable<string> dictionary);
    }
}
