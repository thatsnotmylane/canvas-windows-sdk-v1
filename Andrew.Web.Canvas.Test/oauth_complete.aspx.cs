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
            var clientid = ConfigurationManager.AppSettings["ClientID"];
            var clientsecret = ConfigurationManager.AppSettings["ClientSecret"];
            var domain = ConfigurationManager.AppSettings["Domain"];
            var redirect_uri = new Uri(uri);


            

            var service = new RequestService(new HttpRequestHandler());
            _Default._Config = new CanvasConfig(domain, clientid, clientsecret, redirect_uri);
            _Default._AuthRepository = new AuthRepository(_Default._Config, service, new JsonConverter(), new OAuth2Session("","", 0, ""));
            

            _Default._Session = _Default._AuthRepository.AuthenticateAsync(code).ConfigureAwait(false).GetAwaiter().GetResult();

            _Default._AuthRepository = new AuthRepository(_Default._Config, service, new JsonConverter(), _Default._Session);
            var access_token = _Default._AuthRepository.Session.AccessToken;
            var refresh_token = _Default._AuthRepository.Session.RefreshToken;
            var expires_in = _Default._AuthRepository.Session.ExpiresIn;
            var token_type = _Default._AuthRepository.Session.TokenType;

            string[] lines = { access_token, refresh_token, token_type };
            
            System.IO.File.WriteAllLines(Server.MapPath("~/state.txt"), lines);
            Response.Redirect("https://localhost:44376/Default.aspx");
        }
    }
}