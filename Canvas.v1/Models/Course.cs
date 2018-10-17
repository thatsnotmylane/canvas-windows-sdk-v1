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
        /// The account associated with the course
        /// </summary>
        [JsonProperty(PropertyName = "account_id")]
        public long? AccountId { get; set; }

        /// <summary>
        /// The root account associated with the course
        /// </summary>
        [JsonProperty(PropertyName = "root_account_id")]
        public long? RootAccountId { get; set; }

        /// <summary>
        /// The SIS identifier for the course, if defined. This field is only included if the user has permission to view SIS information.
        /// </summary>
        [JsonProperty(PropertyName = "sis_course_id")]
        public string SisCourseId { get; set; }

        /// <summary>
        /// The integration identifier for the course, if defined. This field is only included if the user has permission to view SIS information.
        /// </summary>
        [JsonProperty(PropertyName = "integration_id")]
        public string IntegrationId { get; set; }

        /// <summary>
        /// The course code
        /// </summary>
        [JsonProperty(PropertyName = "course_code")]
        public string CourseCode { get; set; }

        /// <summary>
        /// The course calendar
        /// </summary>
        [JsonProperty(PropertyName = "calendar")]
        public Calendar Calendar { get; set; }

        /// <summary>
        /// The current state of the course
        /// </summary>
        [JsonProperty(PropertyName = "workflow_state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public CourseWorkflowState WorkflowState { get; set; }

        /// <summary>
        /// The start date for the course, if applicable
        /// </summary>
        [JsonProperty(PropertyName = "start_at")]
        public DateTime? StartAt { get; set; }

        /// <summary>
        /// The end date for the course, if applicable
        /// </summary>
        [JsonProperty(PropertyName = "end_at")]
        public DateTime? EndAt { get; set; }

        /// <summary>
        /// Whether to weight final grade based on assignment group percentages
        /// </summary>
        [JsonProperty(PropertyName = "apply_assignment_group_weights")]
        public bool ApplyAssignmentGroupWeights { get; set; }

        /// <summary>
        /// Whether to hide final grades
        /// </summary>
        [JsonProperty(PropertyName = "hide_final_grades")]
        public bool HideFinalGrades { get; set; }

        /// <summary>
        /// Whether the course syllabus is public
        /// </summary>
        [JsonProperty(PropertyName = "public_syllabus")]
        public bool PublicSyllabus { get; set; }

        /// <summary>
        /// The type of page that users will see when they first visit the course. 
        /// </summary>
        [JsonProperty(PropertyName = "default_view")]
        [JsonConverter(typeof(StringEnumConverter))]
        public CourseDefaultView DefaultView { get; set; }

        /// <summary>
        /// The storage quota for the course, in MB
        /// </summary>
        [JsonProperty(PropertyName = "storage_quota_mb")]
        public int StorageQuotaMB { get; set; }

        /// <summary>
        /// The ID of the enrollment term associated with the course
        /// </summary>
        [JsonProperty(PropertyName = "enrollment_term_id")]
        public int EnrollmentTermId { get; set; }

        /// <summary>
        /// The enrollment term associated with the course
        /// </summary>
        [JsonProperty(PropertyName = "term")]
        public EnrollmentTerm EnrollmentTerm { get; set; }

        /// <summary>
        /// Secret Code that can be used to enroll in a course that allows open enrollment.
        /// </summary>
        [JsonProperty(PropertyName = "self_enrollment_code")]
        public string SelfEnrollmentCode { get; set; }

        /// <summary>
        /// ID:, Name:, CourseCode, WorkflowState, StartAt, EndAt
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Id: {0}, Name: {1}, CourseCode: {2}, WorkflowState: {3}, StartAt: {4}, EndAt: {5}", Id, Name, CourseCode, WorkflowState, StartAt, EndAt);
        }
    }

    /// <summary>
    /// Object to create a new course
    /// </summary>
    public class NewCourseRequest
    {
        /// <summary>
        /// The account associated with the course
        /// </summary>
        [JsonProperty(PropertyName = "account_id")]
        public long? AccountId { get; set; }

        /// <summary>
        /// The name of the new course
        /// </summary>
        [JsonProperty(PropertyName = "course[name]")]
        public string Name
        { get; set; }

        /// <summary>
        /// The Course Code for the Course, a short abreviation, or classification for the course.
        /// </summary>
        [JsonProperty(PropertyName = "course[course_code]")]
        public string CourseCode
        { get; set; }

        /// <summary>
        /// Course start date in ISO8601 format, e.g. 2011-01-01T01:00Z
        /// </summary>
        [JsonProperty(PropertyName = "course[start_at]")]
        public DateTime StartAt
        { get; set; }

        /// <summary>
        /// Course start date in ISO8601 format, e.g. 2011-01-01T01:00Z
        /// </summary>
        [JsonProperty(PropertyName = "course[end_at]")]
        public DateTime EndAt
        { get; set; }

        /// <summary>
        /// The name of the licensing. Should be one of the following abbreviations (a descriptive name is included in parenthesis for reference):
        ///
        ///'private' (Private Copyrighted)
        ///
        ///'cc_by_nc_nd' (CC Attribution Non-Commercial No Derivatives)
        ///
        ///'cc_by_nc_sa' (CC Attribution Non-Commercial Share Alike)
        ///
        ///'cc_by_nc' (CC Attribution Non-Commercial)
        ///
        ///'cc_by_nd' (CC Attribution No Derivatives)
        ///
        ///'cc_by_sa' (CC Attribution Share Alike)
        ///
        ///'cc_by' (CC Attribution)
        ///
        ///'public_domain' (Public Domain).
        /// </summary>
        [JsonProperty(PropertyName = "course[license]")]
        public string Licence
        { get; set; }

        /// <summary>
        /// Set to true if course is public to both authenticated and unauthenticated users.
        /// </summary>
        [JsonProperty(PropertyName = "course[is_public]")]
        public bool IsPublic
        { get; set; }

        /// <summary>
        /// Set to true if course is public only to authenticated users.
        /// </summary>
        [JsonProperty(PropertyName = "course[is_public_to_auth_users]")]
        public bool IsPublicToAuthUsers
        { get; set; }

        /// <summary>
        /// Set to true to make the course syllabus public.
        /// </summary>
        [JsonProperty(PropertyName = "course[public_syllabus]")]
        public bool PublicSyllabus
        { get; set; }

        /// <summary>
        /// Set to true to make the course syllabus public for authenticated users.
        /// </summary>
        [JsonProperty(PropertyName = "course[public_syllabus_to_auth]")]
        public bool PublicSyllabusForAuthenticated
        { get; set; }

        /// <summary>
        /// A publicly visible description of the course.
        /// </summary>
        [JsonProperty(PropertyName = "course[public_description]")]
        public string PublicDescription
        { get; set; }

        /// <summary>
        /// If true, students will be able to modify the course wiki.
        /// </summary>
        [JsonProperty(PropertyName = "course[allow_student_wiki_edits]")]
        public bool AllowStudentWikiEdits
        { get; set; }

        /// <summary>
        /// If true, course members will be able to comment on wiki pages.
        /// </summary>
        [JsonProperty(PropertyName = "course[allow_wiki_comments]")]
        public bool AllowWikiComments
        { get; set; }

        /// <summary>
        /// If true, students can attach files to forum posts.
        /// </summary>
        [JsonProperty(PropertyName = "course[allow_student_forum_attachments]")]
        public bool AllowStudentForumAttachments
        { get; set; }

        /// <summary>
        /// Set to true if the course is open enrollment.
        /// </summary>
        [JsonProperty(PropertyName = "course[open_enrollment]")]
        public bool OpenEnrollment
        { get; set; }

        /// <summary>
        /// Set to true if the course is self enrollment.
        /// </summary>
        [JsonProperty(PropertyName = "course[self_enrollment]")]
        public bool SelfEnrollment
        { get; set; }

        /// <summary>
        /// Set to true to restrict user enrollments to the start and end dates of the course.
        /// </summary>
        [JsonProperty(PropertyName = "course[restrict_enrollments_to_course_dates]")]
        public bool RestrictEnrollmentsToCourseDates
        { get; set; }

        /// <summary>
        /// The unique ID of the term to create to course in.
        /// </summary>
        [JsonProperty(PropertyName = "course[term_id]")]
        public int TermId
        { get; set; }

        /// <summary>
        /// The unique SIS identifier.
        /// </summary>
        [JsonProperty(PropertyName = "course[sis_course_id]")]
        public string SISCourseId
        { get; set; }

        /// <summary>
        /// The unique Integration identifier.
        /// </summary>
        [JsonProperty(PropertyName = "course[integration_id]`")]
        public string IntegrationId
        { get; set; }

        /// <summary>
        /// If this option is set to true, the totals in student grades summary will be hidden.
        /// </summary>
        [JsonProperty(PropertyName = "course[hide_final_grades]")]
        public bool HideFinalGrades
        { get; set; }

        /// <summary>
        /// Set to true to weight final grade based on assignment groups percentages.
        /// </summary>
        [JsonProperty(PropertyName = "course[apply_assignment_group_weights]")]
        public bool ApplyAssignmentGroupWeights
        { get; set; }

        /// <summary>
        /// The time zone for the course. Allowed time zones are IANA time zones or friendlier Ruby on Rails time zones.
        /// </summary>
        [JsonProperty(PropertyName = "course[time_zone]")]
        public string CourseTimeZone
        { get; set; }

        /// <summary>
        /// If this option is set to true, the course will be available to students immediately.
        /// </summary>
        [JsonProperty(PropertyName = "offer")]
        public bool Offer
        { get; set; }

        /// <summary>
        /// Set to true to enroll the current user as the teacher.
        /// </summary>
        [JsonProperty(PropertyName = "enroll_me")]
        public bool EnrollMe
        { get; set; }

        /// <summary>
        /// The type of page that users will see when they first visit the course
        /// 
        ///'feed' Recent Activity Dashboard
        ///
        ///'modules' Course Modules/Sections Page
        ///
        ///'assignments' Course Assignments List
        ///
        ///'syllabus' Course Syllabus Page
        ///
        ///other types may be added in the future
        ///
        ///Allowed values:
        ///feed, wiki, modules, syllabus, assignments
        /// </summary>
        [JsonProperty(PropertyName = "course[default_view]")]
        public string DefaultView
        { get; set; }

        /// <summary>
        /// The syllabus body for the course
        /// </summary>
        [JsonProperty(PropertyName = "course[syllabus_body]")]
        public string SyllabusBody
        { get; set; }

        /// <summary>
        /// The grading standard id to set for the course. If no value is provided for this argument the current grading_standard will be un-set from this course.
        /// </summary>
        [JsonProperty(PropertyName = "course[grading_standard_id]")]
        public int GradingStandardId
        { get; set; }

        /// <summary>
        /// Optional. Specifies the format of the course. (Should be 'on_campus', 'online', or 'blended')
        /// </summary>
        [JsonProperty(PropertyName = "course[course_format]")]
        public string CourseFormat
        { get; set; }

        /// <summary>
        /// When true, will first try to re-activate a deleted course with matching sis_course_id if possible.
        /// </summary>
        [JsonProperty(PropertyName = "enable_sis_reactivation")]
        public bool EnableSISReactivation
        { get; set; }

        /// <summary>
        /// AccountId: {0}, Name: {1}, CourseCode: {2}, StartAt: {3}, EndAt: {4}
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("AccountId: {0}, Name: {1}, CourseCode: {2}, StartAt: {3}, EndAt: {4}", AccountId, Name, CourseCode, StartAt, EndAt);
        }
    }

    [Flags]
    public enum CourseInclude
    {
        Unknown = 0x00,
        Needs_Grading_Count = 0x01,
        Syllabus_Body = 0x02,
        Total_Scores = 0x04,
        Term = 0x08,
        Course_Progress = 0x10,
        Sections = 0x20,
        Storage_Quota_Used_Mb = 0x40,
    }
}
