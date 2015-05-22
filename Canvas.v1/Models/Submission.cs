using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Canvas.v1.Models
{
    /// <summary>
    /// A submitted assignment for a user
    /// </summary>
    public class Submission
    {
        /// <summary>
        /// The submission's assignment ID
        /// </summary>
        [JsonProperty(PropertyName = "assignment_id")]
        public long AssignmentId { get; set; }

        /// <summary>
        /// The submission's assignment
        /// </summary>
//        [JsonProperty(PropertyName = "assignment")]
//        public Assignment Assignment { get; set; }

        /// <summary>
        /// The submission's course
        /// </summary>
        [JsonProperty(PropertyName = "course")]
        public Course Course { get; set; }

        /// <summary>
        /// This is the submission attempt number.
        /// </summary>
        [JsonProperty(PropertyName = "attempt")]
        public int Attempt { get; set; }

        /// <summary>
        /// The content of the submission, if it was submitted directly in a text field.
        /// </summary>
        [JsonProperty(PropertyName = "body")]
        public string Body { get; set; }

        /// <summary>
        /// The grade for the submission, translated into the assignment grading scheme (so a letter grade, for example).
        /// </summary>
        [JsonProperty(PropertyName = "grade")]
        public string Grade { get; set; }

        /// <summary>
        /// A boolean flag which is false if the student has re-submitted since the submission was last graded.
        /// </summary>
        [JsonProperty(PropertyName = "grade_matches_current_submission")]
        public bool GradeMatchesCurrentSubmission { get; set; }

        /// <summary>
        /// URL to the submission. This will require the user to log in.
        /// </summary>
        [JsonProperty(PropertyName = "html_url")]
        public string HtmlUrl { get; set; }

        /// <summary>
        /// URL to the submission preview. This will require the user to log in.
        /// </summary>
        [JsonProperty(PropertyName = "preview_url")]
        public string PreviewUrl { get; set; }

        /// <summary>
        /// The raw score
        /// </summary>
        [JsonProperty(PropertyName = "score")]
        public decimal? Score{ get; set; }

        /// <summary>
        /// Associated comments for a submission (optional)
        /// </summary>
        [JsonProperty(PropertyName = "submission_comments")]
        public string SubmissionComments { get; set; }

        /// <summary>
        /// The type of submisson
        /// </summary>
        [JsonProperty(PropertyName = "submission_type")]
        public SubmissionType SubmissionType { get; set; }

        /// <summary>
        /// The timestamp when the assignment was submitted
        /// </summary>
        [JsonProperty(PropertyName = "submitted_at")]
        public DateTime? SubmittedAt { get; set; }

        /// <summary>
        /// The url of the submission (for SubmssionType = Online_Url)
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        /// <summary>
        /// The id of the user who created the submission
        /// </summary>
        [JsonProperty(PropertyName = "user_id")]
        public long UserId { get; set; }

        /// <summary>
        /// The id of the user who graded the submission
        /// </summary>
        [JsonProperty(PropertyName = "grader_id")]
        public long GraderId { get; set; }

        /// <summary>
        /// The user that made the submission (optional)
        /// </summary>
        [JsonProperty(PropertyName = "user")]
        public User User { get; set; }

        /// <summary>
        /// Whether the submission was made after the applicable due date
        /// </summary>
        [JsonProperty(PropertyName = "late")]
        public bool Late { get; set; }

        /// <summary>
        /// Whether the assignment is visible to the user who submitted the assignment.
        /// Submissions where this value is false no longer count towards the
        /// student's grade and the assignment can no longer be accessed by the student.
        /// This value becomes false for submissions that do not have a grade and
        /// whose assignment is no longer assigned to the student's section.
        /// </summary>
        [JsonProperty(PropertyName = "assignment_visible")]
        public bool AssignmentVisible { get; set; }

    }

    public enum SubmissionType
    {
        Online_Text_Entry,
        Online_Url,
        Online_Upload,
        Media_Recording,
    }
}
