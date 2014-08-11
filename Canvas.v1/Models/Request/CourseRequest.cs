using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Canvas.v1.Models.Request
{
    /// <summary>
    /// A request class for making folder requests
    /// </summary>
    public class CourseRequest 
    {
        /// <summary>
        /// The unique ID of the account to create to course under.
        /// </summary>
        [JsonProperty(PropertyName = "account_id")]
        public int AccountId { get; set; }

        /// <summary>
        /// The course to create.
        /// </summary>
        [JsonProperty(PropertyName = "course")]
        public Course Course { get; set; }
    }

}
