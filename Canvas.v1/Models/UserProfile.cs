using Newtonsoft.Json;

namespace Canvas.v1.Models
{
    /// <summary>
    /// A Canvas user profile
    /// </summary>
    public class UserProfile : UserBase
    {
        /// <summary>
        /// Optional: This field can be requested with certain API calls, and will return the users primary email address.
        /// </summary>
        [JsonProperty(PropertyName = "primary_email")]
        public string PrimaryEmail { get; private set; }

        /// <summary>
        /// The professional/academic title for the user.
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; private set; }

        /// <summary>
        /// Free-form biographical information entered by the user.
        /// </summary>
        [JsonProperty(PropertyName = "bio")]
        public string Bio { get; private set; }
    }
}