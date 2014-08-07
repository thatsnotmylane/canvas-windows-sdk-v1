using Newtonsoft.Json;

namespace Canvas.v1.Models.Permissions
{
    public class BoxFolderPermission : BoxItemPermission
    {

        /// <summary>
        /// Permission to invite additional users to be collaborators
        /// </summary>
        [JsonProperty(PropertyName = "can_invite_collaborator")]
        public bool CanInviteCollaborator { get; private set; }
        
    }
}
