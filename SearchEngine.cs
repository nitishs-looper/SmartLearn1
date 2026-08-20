using System;
using System.Collections.Generic;
namespace SmartLearn1
{
    // Simple static search helper for ISearchable entities
    public static class SearchEngine
    {
        // Search through provided items and return those that match the keyword
        public static List<ISearchable> Search(List<ISearchable> items, string keyword)
        {
            var results = new List<ISearchable>();
            if (items == null || string.IsNullOrWhiteSpace(keyword)) return results;

            foreach (var item in items)
            {
                if (item == null) continue;
                try
                {
                    if (item.MatchesSearch(keyword)) results.Add(item);
                }
                catch
                {
                    // Ignore items that fail during matching
                }
            }

            return results;
        }

        // Display search results with count and individual summaries
        public static void DisplayResults(List<ISearchable> results)
        {
            if (results == null || results.Count == 0)
            {
                Console.WriteLine("No results found.");
                return;
            }

            Console.WriteLine($"Search Results ({results.Count})");
            Console.WriteLine(new string('=', 20));
            foreach (var r in results)
            {
                Console.WriteLine($"- {r.GetSearchSummary()}");
            }
        }
    }
}
