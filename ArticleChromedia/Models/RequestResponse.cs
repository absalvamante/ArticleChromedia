using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArticleChromedia
{
    /// <summary>
    /// Models a response received
    /// </summary>
    public class RequestResponse
    {
        public int Page { get; set; }

        [JsonProperty("per_page")]
        public int PerPage { get; set; }

        public int Total { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }

        public List<Article> Data { get; set; }
    }
}
