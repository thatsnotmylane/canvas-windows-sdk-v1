using System;
using Newtonsoft.Json;

namespace Canvas.v1.Models.Analytics
{
    public class StudentAssignment : AssignmentBase
    {
        [JsonProperty("submission")]
        public Submission Submission { get; set; }
    }
}