using Newtonsoft.Json;

namespace Canvas.v1.Models.Request
{
    /// <summary>
    /// A request class for making collaboration user requests
    /// </summary>
    public class BoxCollaborationUserRequest : BoxRequestEntity
    {

        /// <summary>
        /// An email address (does not need to be a Box user)
        /// </summary>
        [JsonProperty(PropertyName = "login")]
        public string Login { get; set; }

    }
}
