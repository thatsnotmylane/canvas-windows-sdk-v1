using Newtonsoft.Json;

namespace Canvas.v1.Models
{
    public class BoxSortOrder
    {
        [JsonProperty(PropertyName = "by")]
        public BoxSortBy By { get; private set; }

        [JsonProperty(PropertyName = "direction")]
        public BoxSortDirection Direction { get; private set; }
    }
}
