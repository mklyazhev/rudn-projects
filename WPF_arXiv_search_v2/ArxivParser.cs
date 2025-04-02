using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace WPF_arXiv_search
{
    public class ArxivParser
    {
        private const string BaseUrl = "http://export.arxiv.org/api/query?";

        public ArxivFeed Search(string query, int start = 0, int maxResults = 5)
        {
            var encodedQuery = Uri.EscapeDataString($"all:\"{query}\"");
            var url = $"{BaseUrl}search_query={encodedQuery}&start={start}&max_results={maxResults}";

            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                response = response.Replace("<author>", "<contributor>").Replace("</author>", "</contributor>");
                return ParseFeed(response);
            }
        }

        private ArxivFeed ParseFeed(string xmlData)
        {
            var doc = XDocument.Parse(xmlData);
            var ns = XNamespace.Get("http://www.w3.org/2005/Atom");
            var openSearchNs = XNamespace.Get("http://a9.com/-/spec/opensearch/1.1/");

            var feed = new ArxivFeed
            {
                Title = doc.Root?.Element(ns + "title")?.Value,
                Updated = doc.Root?.Element(ns + "updated")?.Value,
                TotalResults = doc.Root?.Element(openSearchNs + "totalResults")?.Value,
                Entries = new List<ArxivEntry>()
            };

            if (doc.Root != null)
            {
                foreach (var entry in doc.Root.Elements(ns + "entry"))
                {
                    var contributors = entry.Elements(ns + "contributor")
                        .Select(c => c.Element(ns + "name")?.Value)
                        .Where(name => !string.IsNullOrEmpty(name))
                        .ToList();

                    feed.Entries.Add(new ArxivEntry
                    {
                        Title = entry.Element(ns + "title")?.Value,
                        Summary = entry.Element(ns + "summary")?.Value,
                        Id = entry.Element(ns + "id")?.Value,
                        Contributors = contributors
                    });
                }
            }

            return feed;
        }
    }

    public class ArxivFeed
    {
        public string Title { get; set; }
        public string Updated { get; set; }
        public string TotalResults { get; set; }
        public List<ArxivEntry> Entries { get; set; } = new List<ArxivEntry>();
    }

    public class ArxivEntry
    {
        public string Title { get; set; }
        public List<string> Contributors { get; set; } = new List<string>();
        public string Summary { get; set; }
        public string Id { get; set; }
    }
}