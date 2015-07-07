using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Canvas.v1.Models.Analytics
{
    public class StudentParticipation
    {
        [JsonProperty("page_views")]
        public Dictionary<DateTime, int> PageViews { get; set; } 

        [JsonProperty("participations")]
        public IEnumerable<StudentActivity> Participations { get; set; } 
    }
}
