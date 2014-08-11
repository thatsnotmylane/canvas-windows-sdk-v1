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

        Uri AuthCodeBaseUri { get; }
        Uri AuthCodeUri { get; }
        Uri CoursesEndpointUri { get; }
        Uri AccountsEndpointUri { get; }
        Uri UsersEndpointUri { get; }
    }
}
