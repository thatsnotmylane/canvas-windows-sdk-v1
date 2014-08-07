using Newtonsoft.Json;

namespace Canvas.v1.Models.Permissions
{
    public class BoxFilePermission : BoxItemPermission
    {

        /// <summary>
        /// Permission to view the file preview
        /// </summary>
        [JsonProperty(PropertyName = "can_preview")]
        public bool CanPreview { get; private set; }
    }
}
