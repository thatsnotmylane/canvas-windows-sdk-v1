using Newtonsoft.Json;

namespace Canvas.v1.Models.Request
{
    /// <summary>
    /// A request class for making email requests
    /// </summary>
    public class BoxEmailRequest
    {
        [JsonProperty(PropertyName = "access")]
        public string Access { get; set; }
    }
}
