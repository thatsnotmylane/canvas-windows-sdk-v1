using System;
using Newtonsoft.Json;

namespace Canvas.v1.Models.Analytics
{
    public abstract class AssignmentBase
    {
        [JsonProperty("assignment_id")]
        public int AssignmentId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("points_possible")]
        public int PointsPossible { get; set; }

        [JsonProperty("due_at")]
        public DateTime? DueAt { get; set; }

        [JsonProperty("unlock_at")]
        public DateTime? UnlockAt { get; set; }

        [JsonProperty("muted")]
        public bool Muted { get; set; }

        [JsonProperty("min_score")]
        public int? MinScore { get; set; }

        [JsonProperty("max_score")]
        public int? MaxScore { get; set; }

        [JsonProperty("median")]
        public int? Median { get; set; }

        [JsonProperty("first_quartile")]
        public int? FirstQuartile { get; set; }

        [JsonProperty("third_quartile")]
        public int? ThirdQuartile { get; set; }
    }
}