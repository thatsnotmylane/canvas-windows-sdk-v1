using Canvas.v1.Managers;
using Newtonsoft.Json;

namespace Canvas.v1.Models.Analytics
{
    public class StudentSummary
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("page_views")]
        public int PageViews { get; set; }
        [JsonProperty("participations")]
        public int Participations { get; set; }
        [JsonProperty("tardiness_breakdown")]
        public TardinessBreakdown TardinessBreakdown { get; set; }
    }
}