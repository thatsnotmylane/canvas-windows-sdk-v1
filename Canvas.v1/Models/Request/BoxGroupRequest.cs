using Newtonsoft.Json;

namespace Canvas.v1.Models.Request
{
    /// <summary>
    /// The request class for making group requests
    /// </summary>
    public class BoxGroupRequest
    {
        /// <summary>
        /// The id of the group
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// The name of the group
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
