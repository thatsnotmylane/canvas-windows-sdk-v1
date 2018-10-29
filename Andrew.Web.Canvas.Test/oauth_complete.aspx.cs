using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Collections.Specialized;
using System.IO;
using System.Configuration;
using BigSIS.Integrations.SDKs.Canvas.v1.Managers;
using BigSIS.Integrations.SDKs.Canvas.v1.Config;
using BigSIS.Integrations.SDKs.Canvas.v1.Converter;
using BigSIS.Integrations.SDKs.Canvas.v1.Request;
using BigSIS.Integrations.SDKs.Canvas.v1.Services;
using BigSIS.Integrations.SDKs.Canvas.v1.Auth;
using BigSIS.Integrations.SDKs.Canvas.v1;
using BigSIS.Integrations.SDKs.Canvas.v1.Models;
using BigSIS.Integrations.SDKs.Canvas.v1.Models.Request;
using BigSIS.Integrations.SDKs.Canvas.v1.Extensions;
using BigSIS.Integrations.SDKs.Canvas.v1.Wrappers;
using BigSIS.Integrations.SDKs.Canvas.v1.Wrappers.Contracts;

namespace Andrew.Web.Canvas.Test
{
    public partial class oauth_complete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var code = Request.Params["code"];
            var uri = ConfigurationManager.AppSettings["RedirectUri"];
            var config = new OAuth2.Configuration.RuntimeClientConfiguration();
            var clientid = ConfigurationManager.AppSettings["ClientID"];
            var clientsecret = ConfigurationManager.AppSettings["ClientSecret"];
            var domain = ConfigurationManager.AppSettings["Domain"];
            var redirect_uri = new Uri(uri);
            

            
        
            var service = new RequestService(new HttpRequestHandler());
            var canvas_config = new CanvasConfig(domain, clientid, clientsecret, redirect_uri);
            //auth = new AuthRepository(config, service, CONVERTER, new OAuth2Session("7~DHwAbT82Vj4LujkiK5f0sHxvIjuAJMC5lg23G54R0qARwckBzBOttvI3WjDxBQa9", "", -1, "Bearer"));
            var auth = new AuthRepository(canvas_config, service, new JsonConverter(), new OAuth2Session("","", 0, ""));
            

            var sess = auth.AuthenticateAsync(code).ConfigureAwait(false).GetAwaiter().GetResult();
            
            var auth2 = new AuthRepository(canvas_config, service, new JsonConverter(), sess);
            var access_token = auth2.Session.AccessToken;
            var refresh_token = auth2.Session.RefreshToken;
            var expires_in = auth2.Session.ExpiresIn;
            var token_type = auth2.Session.TokenType;

            string[] lines = { access_token, refresh_token, token_type };
            
            System.IO.File.WriteAllLines(Server.MapPath("~/state.txt"), lines);

            
            var client = new Client(canvas_config, auth2);
            
            var thing = client.AccountsManager.GetSelf();
            var all_courses = client.AccountsManager.GetCourses(thing.Id);


            var asdf = 1;
        }
    }
}