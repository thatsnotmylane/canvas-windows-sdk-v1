using Newtonsoft.Json;

namespace Canvas.v1.Models.Analytics
{
    public class TardinessBreakdown
    {
        [JsonProperty("total")]
        public int Total { get; set; }
        [JsonProperty("on_time")]
        public int OnTime { get; set; }
        [JsonProperty("late")]
        public int Late { get; set; }
        [JsonProperty("missing")]
        public int Missing { get; set; }
        [JsonProperty("floating")]
        public int Floating { get; set; }
    }
}