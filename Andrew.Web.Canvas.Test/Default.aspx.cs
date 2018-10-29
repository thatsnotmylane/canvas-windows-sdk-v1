using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using Canvas.v1.Managers;
using Canvas.v1.Config;
using Canvas.v1.Converter;
using Canvas.v1.Request;
using Canvas.v1.Services;
using Canvas.v1.Auth;
using Canvas.v1;
using Canvas.v1.Models;
using Canvas.v1.Models.Request;
using Canvas.v1.Extensions;
using Canvas.v1.Wrappers;
using Canvas.v1.Wrappers.Contracts;

namespace Andrew.Web.Canvas.Test
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string uri = ConfigurationManager.AppSettings["RedirectUir"];
			
			string domain = ConfigurationManager.AppSettings["Domain"];
			string oauth_path = ConfigurationManager.AppSettings["OauthPath"];
			string client_id = ConfigurationManager.AppSettings["ClientID"];

            var tokens = System.IO.File.ReadAllLines(@"C:\Users\Andrew\github\canvas-windows-sdk-v1\Andrew.Web.Canvas.Test\state.txt");

            if (tokens != null && tokens.Count() == 3)
            {
                DoStuff(tokens, domain, client_id).Wait();
            }
            else
            {
                string url = String.Format("https://{3}{2}?client_id={0}&response_type=code&redirect_uri={1}", client_id, uri, oauth_path, domain);
                Response.Redirect(url);
            }
        }

        public async static Task DoStuff(string[] tokens, string domain, string client_id)
        {
            var access_token = tokens[0];
            var refresh_token = tokens[1];
            var token_type = tokens[2];
            var client_secret = ConfigurationManager.AppSettings["ClientSecret"];
            var redirect_uri_str = ConfigurationManager.AppSettings["RedirectUri"];
            var redirect_uri = (Uri)null;
            if (string.IsNullOrEmpty(redirect_uri_str) == false)
                redirect_uri = new Uri(redirect_uri_str);

            var canvas_config = new CanvasConfig(domain, client_id, client_secret, redirect_uri);

            var oauth2session = new OAuth2Session(access_token, refresh_token, 0, token_type);

            var client = new Client(canvas_config, new AuthRepository(canvas_config, new RequestService(new HttpRequestHandler()), new JsonConverter(), oauth2session));


            var self = await client.AccountsManager.GetSelf();

            return;
        }
    }
}