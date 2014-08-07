using Newtonsoft.Json;

namespace Canvas.v1.Models
{
    /// <summary>
    /// Box representation of a folder
    /// </summary>
    public class Course
    {
        public const string FieldId = "id";

        /// <summary>
        /// Indicates whether this folder will be synced by the Box sync clients or not. Can be synced, not_synced, or partially_synced
        /// </summary>
        [JsonProperty(PropertyName = FieldId)]
        public long Id { get; private set; }
    }
}
