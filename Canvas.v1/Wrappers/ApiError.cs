using Newtonsoft.Json;

namespace Canvas.v1.Wrappers
{
    /// <summary>
    /// Canvas representation of an Error
    /// </summary>
    public class ApiError
    {
        /// <summary>
        /// The report ID of the error. This value will always be present in the event of an error
        /// </summary>
        [JsonProperty(PropertyName = "error_report_id")]
        public string ErrorReportId { get; set; }

        /// <summary>
        /// A collection of error messages.
        /// </summary>
        [JsonProperty(PropertyName = "errors")]
        public Error[] Errors { get; set; }
    }

    public class Error
    {
        /// <summary>
        /// Associated message with the error
        /// </summary>  
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}
