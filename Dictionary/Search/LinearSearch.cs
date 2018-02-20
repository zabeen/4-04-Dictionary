using System.Collections.Generic;

namespace Dictionary.Search
{
    public class LinearSearch : IDictionarySearch
    {
        public bool FindWordInDictionary(string wordToFind, IEnumerable<string> dictionary)
        {
            foreach (var word in dictionary)
            {
                if (word.Equals(wordToFind))
                    return true;
            }

            return false;
        }
    }
}
