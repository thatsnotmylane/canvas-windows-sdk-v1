using Canvas.v1.Models;

namespace Canvas.v1.Auth.EventArgs
{
    public class SessionAuthenticatedEventArgs : System.EventArgs
    {
        public SessionAuthenticatedEventArgs(OAuthSession session)
        {
            Session = session;
        }
        public OAuthSession Session { get; set;}
    }
}
