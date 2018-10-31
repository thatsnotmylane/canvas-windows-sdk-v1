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
    public partial class _Default : Page
    {
        public static Client _Client;
        public static OAuth2Session _Session;
        public static CanvasConfig _Config;
        public static AuthRepository _AuthRepository;


        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox1.Text = "Loading...";
            Button1.Visible = false;
            if (InitializeOauth())
            {
                TextBox1.Text = "Oauth Initialization Success";
                Button2_Click(sender, e);
            }
            else
            {
                TextBox1.Text = "Oauth Initialization Failed, Perform New Oauth";
                Button1.Visible = true;
            }




        }

        public bool InitializeOauth()
        {
            var tokens = System.IO.File.ReadAllLines(@Server.MapPath("~/state.txt"));

            var refresh_token = "";
            var token_type = "";
            var access_token = "";
            if (tokens != null && tokens.Count() == 3)
            {
                string domain = ConfigurationManager.AppSettings["Domain"];
                string oauth_path = ConfigurationManager.AppSettings["OauthPath"];
                string client_id = ConfigurationManager.AppSettings["ClientID"];
                var client_secret = ConfigurationManager.AppSettings["ClientSecret"];
                var redirect_uri_str = ConfigurationManager.AppSettings["RedirectUri"];
                var redirect_uri = (Uri)null;
                if (string.IsNullOrEmpty(redirect_uri_str) == false)
                    redirect_uri = new Uri(redirect_uri_str);



                access_token = tokens[0];
                refresh_token = tokens[1];
                token_type = tokens[2];
                _Config = new CanvasConfig(domain, client_id, client_secret, redirect_uri);
                _Session = new OAuth2Session(access_token, refresh_token, 1, token_type);
                _AuthRepository = new AuthRepository(_Config, new RequestService(new HttpRequestHandler()), new JsonConverter(), _Session);
                _Client = new Client(_Config, _AuthRepository);
                return true;
            }
            return false;
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

            var oauth2session = new OAuth2Session("", refresh_token, 0, token_type);

            var client = new Client(canvas_config, new AuthRepository(canvas_config, new RequestService(new HttpRequestHandler()), new JsonConverter(), oauth2session));

            var self_account = await client.AccountsManager.GetSelf().ConfigureAwait(false);

            var courses = await client.CoursesManager.GetAll().ConfigureAwait(false);
            return;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string uri = ConfigurationManager.AppSettings["RedirectUri"];
            string domain = ConfigurationManager.AppSettings["Domain"];
            string oauth_path = ConfigurationManager.AppSettings["OauthPath"];
            string client_id = ConfigurationManager.AppSettings["ClientID"];
            string url = String.Format("https://{3}{2}?client_id={0}&response_type=code&redirect_uri={1}", client_id, uri, oauth_path, domain);
            Response.Redirect(url);
        }

        protected async void Button2_Click(object sender, EventArgs e)
        {
            var sessionCourse = Session["course"];
            if (sessionCourse != null)
            {
                lblMessage.Text = sessionCourse.ToString();
            }
            else
            {
                lblMessage.Text = "NOT FOUND!";
            }
            var courses = await _Client.CoursesManager.GetAll().ConfigureAwait(false);
            foreach (var course in courses)
            {
                var course_button = new Button()
                {
                    Text = "Add Students To: " + course.Name,
                    ID = course.Id.ToString(),
                };
                course_button.Click += new EventHandler(Clicky);
                Panel1.Controls.Add(course_button);
            }
        }

        protected async void Clicky(object sender, EventArgs e)
        {
            TextBox1.Text = "Clicky! Clicky!";
            Button button = sender as Button;
            var course_id = 0;
            int.TryParse(button.ID, out course_id);

            var course_Info = await _Client.CoursesManager.Get(course_id).ConfigureAwait(false);

            lblDisplay.Text = course_Info.ToString();
            TextBox2.Visible = false;
            this.Session["course"] = button.Text.ToString();

            var sessionCourse = Session["course"];
            if (sessionCourse != null)
            {
                lblMessage.Text = sessionCourse.ToString();
            }
            else
            {
                lblMessage.Text = "NOT FOUND!";
            }
        }
    }
}