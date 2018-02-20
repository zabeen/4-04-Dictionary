using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Dictionary.Dictionary
{
    public class DictionaryCreator
    {
        public IEnumerable<string> DownloadDictionary(string url)
        {
            var dictionary = new WebClient().DownloadString(url).Split('\n').ToList();
            dictionary.RemoveAll(word => word.Equals(string.Empty));
            dictionary.Sort(StringComparer.Ordinal);
            return dictionary;
        }
    }
}
