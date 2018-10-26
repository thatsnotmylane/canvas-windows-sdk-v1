using System;
using System.Collections.Specialized;
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
using System.IO;

namespace Andrew.Web.Canvas.Test
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string html = string.Empty;
            string uri = "https://localhost:44376/oauth_complete.aspx";
            string url = String.Format("https://instantadmin.instructure.com/login/oauth2/auth?client_id={0}&response_type=code&redirect_uri={1}", "131850000000000003", uri);

            Response.Redirect(url);

            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //request.AutomaticDecompression = DecompressionMethods.GZip;

            //using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            //using (Stream stream = response.GetResponseStream())
            //using (StreamReader reader = new StreamReader(stream))
            //{
            //    html = reader.ReadToEnd();
            //}


            //var request_factory = new OAuth2.Infrastructure.RequestFactory();
            //request_factory.CreateRequest();
            //request_factory.CreateClient();



            //var config = new OAuth2.Configuration.RuntimeClientConfiguration();
            //config.ClientId = "131850000000000003";
            //config.ClientSecret = "2i5KAQ1y3RcBtkL3HoaxX2RTiQAlMBf7AIaX7EydyUjoLzs92kfcFztRhE3mU2mm";
            //config.RedirectUri = "https://instantadmin.instructure.com/accounts/1/developer_keys";





            //var oauthin = new OAuth2.Client.Impl.GoogleClient(request_factory, config);
            //var token = oauthin.GetToken(new NameValueCollection());

            //var asdf = 1;

        }
    }
}