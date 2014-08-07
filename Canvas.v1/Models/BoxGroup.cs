using System;
using Newtonsoft.Json;

namespace Canvas.v1.Models
{
    /// <summary>
    /// Box representation of a group
    /// </summary>
    public class BoxGroup : BoxEntity
    {
        public const string FieldName = "name";
        public const string FieldCreatedAt = "created_at";
        public const string FieldModifiedAt = "modified_at";

        /// <summary>
        /// The name of the group
        /// </summary>
        [JsonProperty(PropertyName = FieldName)]
        public string Name { get; private set; }

        /// <summary>
        /// When this group was created on Box's servers
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedAt)]
        public DateTime? CreatedAt { get; private set; }

        /// <summary>
        /// When this group was last updated on Box's servers
        /// </summary>
        [JsonProperty(PropertyName = FieldModifiedAt)]
        public DateTime? ModifiedAt { get; private set; }
    }
}
