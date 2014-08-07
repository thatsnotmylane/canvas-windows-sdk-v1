using System;

namespace Canvas.v1.Config
{
    public class CanvasConfig : ICanvasConfig
    {

        /// <summary>
        /// Instantiates a Box config with all of the standard defaults
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <param name="redirectUriString"></param>
        public CanvasConfig(string clientId, string clientSecret, Uri redirectUri)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            RedirectUri = redirectUri;
        }

        public virtual Uri CanvasApiHostUri { get { return new Uri(Constants.ApiHostUriString); } }
        public virtual Uri CanvasApiUri { get { return new Uri(Constants.ApiUriString); } }

        public virtual string ClientId { get; private set; }
        public virtual string ConsumerKey { get; private set; }
        public virtual string ClientSecret { get; private set; }
        public virtual Uri RedirectUri { get; set; }

        public string UserAgent { get; set; }

        /// <summary>
        /// Sends compressed responses from Box for faster response times
        /// </summary>
        public CompressionType? AcceptEncoding { get; set; }

        public virtual Uri AuthCodeBaseUri { get { return new Uri(CanvasApiHostUri, Constants.AuthCodeString); } }
        public virtual Uri AuthCodeUri { get { return new Uri(AuthCodeBaseUri, string.Format("?response_type=code&client_id={0}&redirect_uri={1}", ClientId, RedirectUri)); } }
        public virtual Uri CoursesEndpointUri { get { return new Uri(CanvasApiUri, Constants.CoursesString); } }
        public virtual Uri AccountsEndpointUri { get { return new Uri(CanvasApiUri, Constants.AccountsString); } }
        public virtual Uri UsersEndpointUri { get { return new Uri(CanvasApiUri, Constants.UsersString); } }
    }

    public enum CompressionType
    {
        gzip, 
        deflate
    }
}
