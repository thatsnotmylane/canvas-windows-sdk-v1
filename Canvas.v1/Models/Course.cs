using Newtonsoft.Json;

namespace Canvas.v1.Models
{
    /// <summary>
    /// A Canvas course.
    /// </summary>
    public class Course
    {
        /// <summary>
        /// The ID of the course
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public long Id { get; private set; }
    }
}
