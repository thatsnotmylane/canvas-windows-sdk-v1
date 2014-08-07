namespace Canvas.v1.Config
{
    public static class Constants
    {
        /*** Base API URIs ***/
        public const string BoxApiHostUriString = "https://canvas.instructure.com/";
        public const string BoxApiUriString = "https://canvas.instructure.com/api/v1/";
        public const string BoxUploadApiUriString = "https://upload.box.com/api/2.0/";

        /*** API Endpoints ***/
        public const string AuthCodeString = @"login/oauth2/auth";
        public const string AuthTokenEndpointString = @"login/oauth2/token";
        
        public const string CoursesString = @"courses/";
        
        /*** API Full Endpoint Strings ***/
        public const string AuthCodeEndpointString = BoxApiHostUriString + AuthCodeString;
        public const string CoursesEndpointString = BoxApiUriString + CoursesString;
        
        /*** Endpoint Paths ***/
        public const string ItemsPathString = @"{0}/items";
        public const string VersionsPathString = @"{0}/versions";
        public const string CopyPathString = @"{0}/copy";
        public const string CommentsPathString = @"{0}/comments";
        public const string ThumbnailPathString = @"{0}/thumbnail.png";
        public const string PreviewPathString = @"{0}/preview.png";
        public const string TrashPathString = @"{0}/trash";
        public const string DiscussionsPathString = @"{0}/discussions";
        public const string CollaborationsPathString = @"{0}/collaborations";
        public const string TrashItemsPathString = @"trash/items";
        public const string TrashFolderPathString = @"{0}/trash";
        public const string GroupMembershipPathString = @"{0}/memberships";
        public const string ContentPathString = @"{0}/content";

        /*** Auth ***/
        public const string AuthHeaderKey = "Authorization";
        public const string V1AuthString = "BoxAuth api_key={0}&auth_token={1}";
        public const string V2AuthString = "Bearer {0}";

        /*** Return types ***/
        public const string TypeFile = "file";
        public const string TypeFolder = "folder";
        public const string TypeComment = "comment";
        public const string TypeWebLink = "web_link";
        public const string TypeCollaboration = "collaboration";
        public const string TypeFileVersion = "file_version";
        public const string TypeGroup = "group";
        public const string TypeGroupMembership = "group_membership";
        public const string TypeUser = "user";
        public const string TypeEnterprise = "enterprise";

        /*** File Preview ***/
        public const int DefaultRetryDelay = 1000; // milliseconds

        public static class RequestParameters
        {
            /*** Keys ***/
            public const string GrantType = "grant_type";
            public const string Code = "code";
            public const string ClientId = "client_id";
            public const string ClientSecret = "client_secret";
            public const string Token = "token";
            public const string BoxDeviceId = "box_device_id";
            public const string BoxDeviceName = "box_device_name";

            public const string UserAgent = "User-Agent";
            public const string AcceptEncoding = "Accept-Encoding";

            /*** Values ***/
            public const string RefreshToken = "refresh_token";
            public const string AuthorizationCode = "authorization_code";
        }
    }
}
