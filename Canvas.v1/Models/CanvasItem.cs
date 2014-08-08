using Newtonsoft.Json;

namespace Canvas.v1.Models
{
    /// <summary>
    /// A base model containing properties common to many Canvas entities.
    /// </summary>
    public abstract class CanvasItem
    {
        /// <summary>
        /// The ID of the item.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; private set; }

        /// <summary>
        /// The name of the item.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; private set; }
    }
}