using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Canvas.v1.Models
{
    /// <summary>
    /// A Canvas course.
    /// </summary>
    public class Course : CanvasItem
    {
        /// <summary>
        /// The SIS identifier for the course, if defined. This field is only included if the user has permission to view SIS information.
        /// </summary>
        [JsonProperty(PropertyName = "sis_course_id")]
        public string SisCourseId { get; private set; }

        /// <summary>
        /// The integration identifier for the course, if defined. This field is only included if the user has permission to view SIS information.
        /// </summary>
        [JsonProperty(PropertyName = "integration_id")]
        public string IntegrationId { get; private set; }
        
        /// <summary>
        /// The course code
        /// </summary>
        [JsonProperty(PropertyName = "course_code")]
        public string CourseCode { get; private set; }

        /// <summary>
        /// The course calendar
        /// </summary>
        [JsonProperty(PropertyName = "calendar")]
        public Calendar Calendar { get; private set; }

        /// <summary>
        /// The current state of the course
        /// </summary>
        [JsonProperty(PropertyName = "workflow_state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public CourseWorkflowState WorkflowState { get; private set; }

        /// <summary>
        /// The account associated with the course
        /// </summary>
        [JsonProperty(PropertyName = "account_id")]
        public string AccountId { get; private set; }

        /// <summary>
        /// The root account associated with the course
        /// </summary>
        [JsonProperty(PropertyName = "root_account_id")]
        public string RootAccountId { get; private set; }

        /// <summary>
        /// The start date for the course, if applicable
        /// </summary>
        [JsonProperty(PropertyName = "start_at")]
        public DateTime? StartAt { get; private set; }

        /// <summary>
        /// The end date for the course, if applicable
        /// </summary>
        [JsonProperty(PropertyName = "end_at")]
        public DateTime? EndAt { get; private set; }

        /// <summary>
        /// Whether to weight final grade based on assignment group percentages
        /// </summary>
        [JsonProperty(PropertyName = "apply_assignment_group_weights")]
        public bool ApplyAssignmentGroupWeights { get; private set; }

        /// <summary>
        /// Whether to hide final grades
        /// </summary>
        [JsonProperty(PropertyName = "hide_final_grades")]
        public bool HideFinalGrades { get; private set; }

        /// <summary>
        /// Whether the course syllabus is public
        /// </summary>
        [JsonProperty(PropertyName = "public_syllabus")]
        public bool PublicSyllabus { get; private set; }

        /// <summary>
        /// The type of page that users will see when they first visit the course. 
        /// </summary>
        [JsonProperty(PropertyName = "default_view")]
        [JsonConverter(typeof(StringEnumConverter))]
        public CourseDefaultView DefaultView { get; private set; }

        /// <summary>
        /// The storage quota for the course, in MB
        /// </summary>
        [JsonProperty(PropertyName = "storage_quota_mb")]
        public int StorageQuotaMB { get; private set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, Name: {1}, CourseCode: {2}, WorkflowState: {3}, StartAt: {4}, EndAt: {5}", Id, Name, CourseCode, WorkflowState, StartAt, EndAt);
        }
    }

    [Flags]
    public enum CourseWorkflowState
    {
        Undefined   = 0x00,
        Created     = 0x01,
        Claimed     = 0x02,
        Unpublished = 0x04,
        Available   = 0x08,
        Completed   = 0x10,
        Deleted     = 0x20,         
    }

    /// <summary>
    /// The type of page that users will see when they first visit a course. Other types may be added in the future.
    /// </summary>
    public enum CourseDefaultView
    {
        /// <summary>
        /// Recent Activity Dashboard 
        /// </summary>
        Feed,
        /// <summary>
        /// Wiki Front Page 
        /// </summary>
        Wiki,
        /// <summary>
        /// Course Modules/Sections Page 
        /// </summary>
        Modules,
        /// <summary>
        /// Course Assignments List 
        /// </summary>
        Assignments,
        /// <summary>
        /// Course Syllabus Page 
        /// </summary>
        Syllabus,
    }
}
