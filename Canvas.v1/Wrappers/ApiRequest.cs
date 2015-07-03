using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Canvas.v1.Wrappers.Contracts;

namespace Canvas.v1.Wrappers
{
    public class ApiRequest : IApiRequest
    {
        /// <summary>
        /// Instantiates a new Box request with the provided host URI
        /// </summary>
        /// <param name="hostUri"></param>
        public ApiRequest(Uri hostUri) : this(hostUri, string.Empty, null) { }

        /// <summary>
        /// Instantiates a new Box request with the provided host URI
        /// </summary>
        /// <param name="hostUri"></param>
        public ApiRequest(Uri hostUri, string path) : this(hostUri, path, null) { }

        /// <summary>
        /// Instantiates a new Box request with the provided host URI
        /// </summary>
        /// <param name="hostUri"></param>
        public ApiRequest(Uri hostUri, AsUser asUser) : this(hostUri, string.Empty, asUser) { }

        /// <summary>
        /// Instantiates a new Box request with the provided host URI and path
        /// </summary>
        /// <param name="hostUri"></param>
        /// <param name="path"></param>
        public ApiRequest(Uri hostUri, string path, AsUser asUser)
        {
            Host = hostUri;
            Path = path;
            AsUser = asUser;

            HttpHeaders = new Dictionary<string, string>();
            Parameters = new Dictionary<string, string>();
            PayloadParameters = new Dictionary<string, string>();

            // Initialize Defaults
            ContentEncoding = Encoding.UTF8;
        }

        public Uri Host { get; private set; }

        public string Path { get; private set; }
        public AsUser AsUser { get; private set; }

        public virtual RequestMethod Method { get; set; }

        public Dictionary<string, string> HttpHeaders { get; private set; }

        public Dictionary<string, string> Parameters { get; private set; }

        public Dictionary<string, string> PayloadParameters { get; private set; }

        public string ContentType { get; set; }

        public Encoding ContentEncoding { get; set; }

        public Uri Uri { get { return ConstructUri(); }
        }

        private Uri ConstructUri()
        {
            var uri = new Uri(Host, Path);
            return (AsUser == null) ? uri : AsUser.AppendQuery(uri);
        }

        public string Payload { get; set; }

        public string Authorization { get; set; }

        public TimeSpan? Timeout { get; set; }

        /// <summary>
        /// Returns the full Uri including host, path, and querystring
        /// </summary>
        public Uri AbsoluteUri
        {
            get
            {
                return new Uri(Uri,
                    Parameters.Count == 0 ? string.Empty :
                    string.Format("?{0}", GetQueryString()));
            }
        }

        /// <summary>
        /// Returns the query string of the parameters dictionary
        /// </summary>
        /// <returns></returns>
        public string GetQueryString()
        {
            if (Parameters.Count == 0)
                return string.Empty;

            var paramStrings = Parameters
                                .Where(p => !string.IsNullOrEmpty(p.Value))
                                .Select(p => string.Format("{0}={1}", p.Key, p.Value));

            return string.Join("&", paramStrings);
        }
    }

    /// <summary>
    /// The available request types
    /// </summary>
    public enum RequestMethod
    {
        Get,
        Post,
        Put,
        Delete,
        Options
    }
}
