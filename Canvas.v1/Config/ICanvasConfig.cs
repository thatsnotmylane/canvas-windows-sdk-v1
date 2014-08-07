using System;

namespace Canvas.v1.Config
{
    public interface ICanvasConfig
    {
        Uri CanvasApiHostUri { get; }
        Uri CanvasApiUri { get; }

        string ClientId { get; }
        string ConsumerKey { get; }
        string ClientSecret { get; }
        Uri RedirectUri { get; }

        string UserAgent { get; set; }
        
        /// <summary>
        /// Sends compressed responses from Box for faster response times
        /// </summary>
        CompressionType? AcceptEncoding { get; }

        Uri AuthCodeBaseUri { get; }
        Uri AuthCodeUri { get; }
        Uri CoursesEndpointUri { get; }
    }
}
