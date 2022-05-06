using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ArticleChromedia
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Set default handling for null value when deserializing objects
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            string input = null;
            do
            {
                // Ask for limit
                Console.Clear();
                Console.Write("Limit: ");
                input = Console.ReadLine();

                // Check if limit is valid
                if(string.IsNullOrEmpty(input))
                {
                    continue;
                }

                if (!int.TryParse(input, out var limit))
                {
                    Console.WriteLine("Limit is required.");
                    continue;
                }

                // Get the top articles based on limit
                var articles = topArticles(limit).GetAwaiter().GetResult();

                // Display top articles
                foreach(var article in articles)
                {
                    Console.WriteLine(article);
                }

                // Allow user to see the list before continuing
                Console.ReadKey();
            }
            while (!string.IsNullOrEmpty(input));
        }

        private static async Task<string[]> topArticles(int limit)
        {            
            var page = 1;
            var totalPages = 1;
            var articles = new List<Article>();
            var httpClient = new HttpClient();

            do
            {
                // Create http request
                var url = $"https://jsonmock.hackerrank.com/api/articles?page={page}";
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(url),
                    Method = HttpMethod.Get
                };

                // Send request
                var response = await httpClient.SendAsync(request);

                // Check response status
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Unable to get article due to an exception");
                }
                
                // Read content
                var content = await response.Content.ReadAsStringAsync();

                // Deserialize content
                var responseOject = JsonConvert.DeserializeObject<RequestResponse>(content);

                // Consolidate received articles
                articles.AddRange(responseOject.Data);

                // Get total pages
                totalPages = responseOject.TotalPages;

                // Increment current page
                page++;
            }
            while (page <= totalPages);

            return articles
                        .Where(x => x.HasName)
                        .OrderByDescending(x => x.NumComments)
                        .ThenByDescending(x => x.Name)
                        .Select(x => x.Name)
                        .Take(limit)
                        .ToArray();
        }

    }
}
