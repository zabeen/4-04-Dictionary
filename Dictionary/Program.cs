using Dictionary.Dictionary;
using Dictionary.Search;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Dictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Searching...");

            var linearTask = Task.Run(() => WriteOutAverageSearchTime<LinearSearch>(10000));
            var binaryTask = Task.Run(() => WriteOutAverageSearchTime<BinarySearch>(10000));

            linearTask.Wait();
            binaryTask.Wait();

            Console.WriteLine("\nAll done!");
            Console.ReadLine();
        }

        private static void WriteOutAverageSearchTime<TSearch>(int noOfRuns) where TSearch : IDictionarySearch, new()
        {
            var dictionary = new DictionaryCreator()
                .DownloadDictionary(@"https://raw.githubusercontent.com/tomprogers/english-words/tpr/24-sort-words/words.txt")
                .ToList();

            var search = new TSearch();
            var random = new Random();
            var runTimes = new List<long>();

            for (var i = 0; i < noOfRuns; i++)
            {
                var randomWord = dictionary[random.Next(0, dictionary.Count - 1)];
                if (!TimeWordSearch(search, randomWord, dictionary, out var runTime))
                {
                    Console.WriteLine(
                        $"Error: {search.GetType().Name} did not find {randomWord}; test terminated.");
                    return;
                }
                runTimes.Add(runTime);
            }

            Console.WriteLine(
                $"{search.GetType().Name} over {noOfRuns} runs - " +
                $"mean: {runTimes.Average()}ms; " +
                $"range: {runTimes.Min()}-{runTimes.Max()}ms"
                );
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
