namespace Canvas.v1.Config
{
    public static class Constants
    {
        /*** Base API URIs ***/
        public const string ApiHostUriString = "https://canvas.instructure.com/";
        public const string ApiUriString = "https://canvas.instructure.com/api/v1/";

        /*** API Endpoints ***/
        public const string AuthCodeString = @"login/oauth2/auth";
        public const string AuthTokenEndpointString = @"login/oauth2/token";

        public const string CoursesString = @"courses/";
        public const string AccountsString = @"accounts/";
        public const string UsersString = @"users/";

        /*** Auth ***/
        public const string AuthHeaderKey = "Authorization";
        public const string V2AuthString = "Bearer {0}";

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

            public const string UserAgent = "User-Agent";
            public const string AcceptEncoding = "Accept-Encoding";

            /*** Values ***/
            public const string RefreshToken = "refresh_token";
            public const string AuthorizationCode = "authorization_code";
        }

    }
}
