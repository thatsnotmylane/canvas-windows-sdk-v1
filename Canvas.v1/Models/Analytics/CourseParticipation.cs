using System.Collections.Generic;
using Newtonsoft.Json;

namespace Canvas.v1.Models.Analytics
{
    public class CourseParticipation
    {
        [JsonProperty("page_views")]
        public Dictionary<string, PageView> PageViews { get; set; }
        [JsonProperty("participations")]
        public string[] Participations { get; set; }
    }
}