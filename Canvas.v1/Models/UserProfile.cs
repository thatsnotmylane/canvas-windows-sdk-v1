using Newtonsoft.Json;

namespace Canvas.v1.Models
{
    /// <summary>
    /// A Canvas user profile
    /// </summary>
    public class UserProfile
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; private set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; private set; }

        [JsonProperty(PropertyName = "short_name")]
        public string ShortName { get; private set; }

        [JsonProperty(PropertyName = "sortable_name")]
        public string SortableName { get; private set; }
    }
}