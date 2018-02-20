using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Policy;

namespace Dictionary.Search
{
    public class BinarySearch : IDictionarySearch
    {
        public bool FindWordInDictionary(string wordToFind, IEnumerable<string> dictionary)
        {
            var dict = dictionary.ToList();
            var first = 0;
            var last = dict.Count - 1;

            while (true)
            {
                if (first > last)
                    return false;

                var middleIndex = (int)Math.Floor((decimal)(first + last) / 2);
                var middleWord = string.Copy(dict[middleIndex]);
                var comparison = string.Compare(wordToFind, middleWord, StringComparison.Ordinal);

                if (comparison < 0)
                {
                    last = middleIndex - 1;
                    continue;
                }

                if (comparison > 0)
                {
                    first = middleIndex + 1;
                    continue;
                }

                return true;
            }
        }
    }
}
