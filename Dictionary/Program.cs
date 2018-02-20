using Dictionary.Search;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Dictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Downloading dictionary...");
            var dictionary = DownloadDictionary(@"https://raw.githubusercontent.com/dwyl/english-words/master/words.txt").ToList();

            Console.WriteLine("Searching...");

            var linearTask = Task.Run(() => WriteOutAverageSearchTime<LinearSearch>(dictionary, 10000));
            var binaryTask = Task.Run(() => WriteOutAverageSearchTime<BinarySearch>(dictionary, 10000));

            linearTask.Wait();
            binaryTask.Wait();

            Console.WriteLine("\nAll done!");
            Console.ReadLine();
        }

        private static IEnumerable<string> DownloadDictionary(string url)
        {
            var dictionary = new WebClient().DownloadString(url).Split('\n').ToList();
            dictionary.RemoveAll(word => word.Equals(string.Empty));
            dictionary.Sort();
            return dictionary;
        }

        private static void WriteOutAverageSearchTime<TSearch>(IEnumerable<string> dictionary, int noOfRuns) where TSearch : IDictionarySearch, new()
        {
            var search = new TSearch();
            var dict = dictionary.ToList();
            var random = new Random();
            var runTimes = new List<long>();

            for (var i = 0; i < noOfRuns; i++)
            {
                var randomWord = random.Next(0, dict.Count - 1);
                if (!TimeWordSearch(search, dict[randomWord], dict, out var runTime))
                {
                    Console.WriteLine(
                        $"Error: {search.GetType().Name} did not find {dict[randomWord]}; test terminated.");
                    return;
                }
                runTimes.Add(runTime);
            }

            Console.WriteLine(
                $"{search.GetType().Name} average speed {runTimes.Average(l => l)}ms over {noOfRuns} runs.");
        }

        private static bool TimeWordSearch(IDictionarySearch search, string wordToFind, IEnumerable<string> dictionary, out long runTime)
        {
            var timer = new Stopwatch();
            timer.Start();
            var wordExists = search.FindWordInDictionary(wordToFind, dictionary);
            timer.Stop();
            runTime = timer.ElapsedMilliseconds;

            return wordExists;
        }
    }
}
