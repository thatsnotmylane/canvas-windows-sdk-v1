using Canvas.v1.Models;

namespace Canvas.v1.Auth.EventArgs
{
    public class SessionAuthenticatedEventArgs : System.EventArgs
    {
        public SessionAuthenticatedEventArgs(OAuth2Session session)
        {
            Session = session;
        }
        public OAuth2Session Session { get; set;}
    }
}
