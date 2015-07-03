using System;
using Newtonsoft.Json;

namespace Canvas.v1.Models.Analytics
{
    public class CourseAssignment : AssignmentBase
    {
        [JsonProperty("tardiness_breakdown")]
        public TardinessBreakdown TardinessBreakdown { get; set; }
    }
}