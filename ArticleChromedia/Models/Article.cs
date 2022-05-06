using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ArticleChromedia
{
    /// <summary>
    /// Models an article
    /// </summary>
    public class Article
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Author { get; set; }

        [JsonProperty("num_comments")]
        public int NumComments { get; set; } = 0;

        [JsonProperty("story_id")]
        public string StoryId { get; set; }

        [JsonProperty("story_title")]
        public string StoryTitle { get; set; }

        [JsonProperty("story_url")]
        public string StoryUrl { get; set; }

        [JsonProperty("parent_id")]
        public string ParentId { get; set; }

        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }

        public bool HasName => !string.IsNullOrEmpty(Title) || !string.IsNullOrEmpty(StoryTitle);

        public string Name => !string.IsNullOrEmpty(Title) ? Title : !string.IsNullOrEmpty(StoryTitle) ? StoryTitle : "";
    }
}
