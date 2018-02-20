﻿using Dictionary.Search;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;

namespace Dictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            var dictionary = DownloadDictionary(@"https://raw.githubusercontent.com/dwyl/english-words/master/words.txt").ToList();

            WriteOutAverageSearchTime<LinearSearch>("bob", dictionary, 10000);
            WriteOutAverageSearchTime<BinarySearch>("bob", dictionary, 10000);

            Console.ReadLine();
        }

        private static IEnumerable<string> DownloadDictionary(string url)
        {
            var dictionary = new WebClient().DownloadString(url).Split('\n').ToList();
            dictionary.RemoveAll(word => word.Equals(string.Empty));
            dictionary.Sort();
            return dictionary;
        }

        private static void WriteOutAverageSearchTime<TSearch>(string wordToFind, IEnumerable<string> dictionary, int noOfRuns) 
            where TSearch : IDictionarySearch, new()
        {
            var search = new TSearch();
            var dict = dictionary.ToList();
            var runTimes = new List<long>();
            
            for (var i = 0; i < noOfRuns; i++)
            {
                var timer = new Stopwatch();
                timer.Start();

                if (!search.FindWordInDictionary(wordToFind, dict))
                    break;

                timer.Stop();
                runTimes.Add(timer.ElapsedMilliseconds);
            }
            
            Console.WriteLine($"{search.GetType().Name} - x{noOfRuns} runs - average run time: {runTimes.Average(l => l)}ms");
        }
    }
}
