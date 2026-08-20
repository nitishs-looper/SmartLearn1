using System;
using System.Collections.Generic;
namespace SmartLearn1
{
    public static class SearchEngine
    {
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
                }
            }

            return results;
        }
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
