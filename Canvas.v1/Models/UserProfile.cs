using System;
using Newtonsoft.Json;

namespace Canvas.v1.Models
{
    /// <summary>
    /// A Canvas user profile
    /// </summary>
    public class UserProfile
    {
        /// <summary>
        /// The ID of the user.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; private set; }

        /// <summary>
        /// The name of the user.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; private set; }

        /// <summary>
        /// A short name the user has selected, for use in conversations or other less formal places through the site.
        /// </summary>
        [JsonProperty(PropertyName = "short_name")]
        public string ShortName { get; private set; }

        /// <summary>
        /// The name of the user that is should be used for sorting groups of users, such as in the gradebook.
        /// </summary>
        [JsonProperty(PropertyName = "sortable_name")]
        public string SortableName { get; private set; }

        /// <summary>
        /// The unique login id for the user.  This is what the user uses to log in to Canvas.
        /// </summary>
        [JsonProperty(PropertyName = "login_id")]
        public string LoginId { get; private set; }

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

        /// <summary>
        /// The SIS ID associated with the user.  This field is only included if the user came from a SIS import and has permissions to view SIS information.
        /// </summary>
        [JsonProperty(PropertyName = "sis_user_id")]
        public string SisUserId { get; private set; }

        /// <summary>
        /// The SIS login ID associated with the user. 
        /// </summary>
        [Obsolete("This field will be removed in a future version of the API. Please use the SisUserId or LoginId.")]
        [JsonProperty(PropertyName = "sis_login_id")]
        public string SisLoginId { get; private set; }

        /// <summary>
        /// If avatars are enabled, this field will be included and contain a url to retrieve the user's avatar.
        /// </summary>
        [JsonProperty(PropertyName = "avatar_url")]
        public string AvatarUrl { get; private set; }

        /// <summary>
        /// Optional: This field is only returned in ceratin API calls, and will return the IANA time zone name of the user's preferred timezone.
        /// </summary>
        [JsonProperty(PropertyName = "time_zone")]
        public string TimeZone { get; private set; }
    }
}