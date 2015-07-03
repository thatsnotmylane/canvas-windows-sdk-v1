using Newtonsoft.Json;

namespace Canvas.v1.Models.Analytics
{
    public class PageView
    {
        [JsonProperty("assignments")]
        public int Assignments { get; set; }
        [JsonProperty("files")]
        public int Files { get; set; }
        [JsonProperty("general")]
        public int General { get; set; }
        [JsonProperty("grades")]
        public int Grades { get; set; }
        [JsonProperty("pages")]
        public int Pages { get; set; }
        [JsonProperty("other")]
        public int Other { get; set; }
    }
}