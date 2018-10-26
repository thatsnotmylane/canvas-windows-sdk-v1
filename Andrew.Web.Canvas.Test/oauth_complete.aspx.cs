using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OAuth2.Client;
using OAuth2.Client.Impl;
using OAuth2.Configuration;
using OAuth2.Infrastructure;
using OAuth2.Models;
using System.Net;
using System.Collections.Specialized;
using System.IO;
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
    public partial class oauth_complete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var code = Request.Params["code"];


            var request_factory = new OAuth2.Infrastructure.RequestFactory();
            request_factory.CreateRequest();
            request_factory.CreateClient();

            string uri = "https://localhost:44376/oauth_complete.aspx";


            var config = new OAuth2.Configuration.RuntimeClientConfiguration();
            var clientid = "131850000000000003";
            var clientsecret = "2i5KAQ1y3RcBtkL3HoaxX2RTiQAlMBf7AIaX7EydyUjoLzs92kfcFztRhE3mU2mm";
            var redirect_uri = new Uri(uri);

            var request = WebRequest.Create("https://instantadmin.instructure.com/login/oauth2/token");
            request.Method = "POST";
            //            var post_data = "grant_type:authorization_code
            //client_id: 131850000000000003
            //client_secret: 2i5KAQ1y3RcBtkL3HoaxX2RTiQAlMBf7AIaX7EydyUjoLzs92kfcFztRhE3mU2mm
            // redirect_uri:https://localhost:44376/oauth_complete.aspx
            //            code: CODE"
            

            
        
            var service = new RequestService(new HttpRequestHandler());
            var canvas_config = new CanvasConfig("instantadmin.instructure.com", clientid, clientsecret, new Uri("https://localhost:44376/oauth_complete.aspx"));
            //auth = new AuthRepository(config, service, CONVERTER, new OAuth2Session("7~DHwAbT82Vj4LujkiK5f0sHxvIjuAJMC5lg23G54R0qARwckBzBOttvI3WjDxBQa9", "", -1, "Bearer"));
            var auth = new AuthRepository(canvas_config, service, new JsonConverter(), new OAuth2Session("","", 0, ""));
            

            var sess = auth.AuthenticateAsync(code).ConfigureAwait(false).GetAwaiter().GetResult();
            
            var auth2 = new AuthRepository(canvas_config, service, new JsonConverter(), sess);

            var client = new Client(canvas_config, auth2);

            var courses = client.CoursesManager.GetAll();

            var asdf = 1;
        }
    }
}