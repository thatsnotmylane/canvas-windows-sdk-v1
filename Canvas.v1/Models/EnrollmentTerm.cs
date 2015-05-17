using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Canvas.v1.Models
{
    public class EnrollmentTerm
    {
        /// <summary>
        /// The ID of the item.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        /// <summary>
        /// The name of the item.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// The SIS identifier for the term. This field is only included if the user has permission to manage SIS information.
        /// </summary>
        [JsonProperty(PropertyName = "sis_term_id")]
        public string SisTermId { get; set; }

        /// <summary>
        /// The start time of the term
        /// </summary>
        [JsonProperty(PropertyName = "start_at")]
        public DateTime? StartAt { get; set; }

        /// <summary>
        /// The end time of the term
        /// </summary>
        [JsonProperty(PropertyName = "end_at")]
        public DateTime? EndAt { get; set; }

        /// <summary>
        /// The state of the user's enrollment in the course.
        /// </summary>
        [JsonProperty(PropertyName = "workflow_state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public EnrollmentTermWorkflowState WorkflowState { get; set; }
    }
}