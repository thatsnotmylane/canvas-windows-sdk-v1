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
        public string SisImportId { get; set; }

        /// <summary>
        /// Optional: This field can be requested with certain API calls, and will return the users primary email address.
        /// </summary>
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        /// <summary>
        /// Optional: This field can be requested with certain API calls, and will return the users locale.
        /// </summary>
        [JsonProperty(PropertyName = "locale")]
        public string Locale { get; set; }

        /// <summary>
        /// Optional: This field is only returned in certain API calls, and will return a timestamp representing the last time the user logged in to Canvas.
        /// </summary>
        [JsonProperty(PropertyName = "last_login")]
        public DateTime? LastLogin { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, LoginId: {1}, Name: {2}, Email: {3}, LastLogin: {4}", Id, LoginId, Name, Email, LastLogin);
        }
    }

    [Flags]
    public enum UserEnrollmentType
    {
        Undefined = 0x00,
        Teacher = 0x01,
        Student = 0x02,
        TA = 0x04,
        Observer = 0x08,
        Designer = 0x10,
    }

    [Flags]
    public enum UserEnrollmentRole
    {
        Undefined = 0x00,
        TeacherEnrollment = 0x01,
        StudentEnrollment = 0x02,
        TAEnrollment = 0x04,
        ObserverEnrollment = 0x08,
        DesignerEnrollment = 0x10,
    }

    public enum UserInclude
    {
        Undefined = 0x00,
        Email = 0x01,
        Enrollments = 0x02,
        Locked = 0x04,
        AvatarURL = 0x08,
        TestStudent = 0x10,
    }
}