using Newtonsoft.Json;

namespace Canvas.v1.Models
{
    public class BoxWebLink : BoxItem
    {
        public const string FieldUrl = "url";

        [JsonProperty(PropertyName = FieldUrl)]
        public string Url { get; private set; }
    }
}
