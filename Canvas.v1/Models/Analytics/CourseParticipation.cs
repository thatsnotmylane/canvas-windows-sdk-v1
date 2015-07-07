using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Canvas.v1.Models.Analytics
{
    public class CourseParticipation
    {
        [JsonProperty("date")]
        public DateTime Date { get; set; }
        [JsonProperty("participations")]
        public int Participations { get; set; }
        [JsonProperty("views")]
        public int Views { get; set; }
    }
}