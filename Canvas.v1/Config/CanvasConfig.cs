using System;

namespace Canvas.v1.Config
{
    public class CanvasConfig : ICanvasConfig
    {
        /// <summary>
        /// Instantiates a Box config with all of the standard defaults
        /// </summary>
        /// <param name="canvasDomain"></param>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <param name="redirectUri"></param>
        /// <param name="redirectUriString"></param>
        public CanvasConfig(string canvasDomain, string clientId, string clientSecret, Uri redirectUri)
        {
            CanvasDomain = canvasDomain;
            ClientId = clientId;
            ClientSecret = clientSecret;
            RedirectUri = redirectUri;
        }

        public virtual Uri CanvasApiHostUri { get { return new Uri(string.Format("https://{0}/", CanvasDomain)); } }
        public virtual Uri CanvasApiUri { get { return new Uri(string.Format("https://{0}/api/v1/", CanvasDomain)); } }

        public string CanvasDomain { get; set; }
        public virtual string ClientId { get; private set; }
        public virtual string ConsumerKey { get; private set; }
        public virtual string ClientSecret { get; private set; }
        public virtual Uri RedirectUri { get; set; }

        public string UserAgent { get; set; }

        public virtual Uri AuthCodeBaseUri { get { return new Uri(CanvasApiHostUri, Constants.AuthCodeString); } }
        public virtual Uri AuthCodeUri { get { return new Uri(AuthCodeBaseUri, string.Format("?response_type=code&client_id={0}&redirect_uri={1}", ClientId, RedirectUri)); } }
        public virtual Uri CoursesEndpointUri { get { return new Uri(CanvasApiUri, Constants.CoursesString); } }
        public virtual Uri AccountsEndpointUri { get { return new Uri(CanvasApiUri, Constants.AccountsString); } }
        public virtual Uri UsersEndpointUri { get { return new Uri(CanvasApiUri, Constants.UsersString); } }
    }

    [Flags]
    public enum CompressionType
    {
        gzip, 
        deflate
    }
}
