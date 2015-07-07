using System;
using Newtonsoft.Json;

namespace Canvas.v1.Models.Analytics
{
    public class StudentActivity
    {
        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}