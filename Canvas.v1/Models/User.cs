using System;
using Newtonsoft.Json;

namespace Canvas.v1.Models
{
    /// <summary>
    /// A Canvas user
    /// </summary>
    public class User : UserBase
    {
        /// <summary>
        /// The id of the SIS import.  This field is only included if the user came from a SIS import and has permissions to manage SIS information.
        /// </summary>
        [JsonProperty(PropertyName = "sis_import_id")]
        public string SisImportId { get; private set; }

        /// <summary>
        /// Optional: This field can be requested with certain API calls, and will return the users primary email address.
        /// </summary>
        [JsonProperty(PropertyName = "primary_email")]
        public string PrimaryEmail { get; private set; }

        /// <summary>
        /// Optional: This field can be requested with certain API calls, and will return the users locale.
        /// </summary>
        [JsonProperty(PropertyName = "locale")]
        public string Locale { get; private set; }

        /// <summary>
        /// Optional: This field is only returned in certain API calls, and will return a timestamp representing the last time the user logged in to Canvas.
        /// </summary>
        [JsonProperty(PropertyName = "last_login")]
        public DateTime? LastLogin { get; private set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, LoginId: {1}, Name: {2}, PrimaryEmail: {3}, LastLogin: {4}", Id, LoginId, Name, PrimaryEmail, LastLogin);
        }
    }
}