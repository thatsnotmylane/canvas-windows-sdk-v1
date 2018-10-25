using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Andrew.Canvas.Test
{
    class Program
    {
        //public static string DOMAIN = "canvas.instructure.com";
        public static string DOMAIN = "instantadmin.instructure.com";
        public static Uri REDIRECT = new Uri("https://instantadmin.instructure.com/accounts/1/developer_keys");
        public static JsonConverter CONVERTER = new JsonConverter();
        public static HttpRequestHandler HANDLER = new HttpRequestHandler();
        public static RequestService service;
        public static CanvasConfig config;
        public static AuthRepository auth;
        public static OAuth2Session session;
        public static Client client;
        public static string CLIENT_ID = "131850000000000003";
        public static string CLIENT_SECRET = "2i5KAQ1y3RcBtkL3HoaxX2RTiQAlMBf7AIaX7EydyUjoLzs92kfcFztRhE3mU2mm";

        static void Main(string[] args)
        {
            Initialize();
            DoSomeOauth2().Wait();
            //DoTheCourses().Wait();
            //GetUsers().Wait();
            return;
        }

        public static void Initialize()
        {
            service = new RequestService(HANDLER);
            config = new CanvasConfig(DOMAIN, "", "", REDIRECT);
            //auth = new AuthRepository(config, service, CONVERTER, new OAuth2Session("7~DHwAbT82Vj4LujkiK5f0sHxvIjuAJMC5lg23G54R0qARwckBzBOttvI3WjDxBQa9", "", -1, "Bearer"));
            auth = new AuthRepository(config, service, CONVERTER, new OAuth2Session("13185~Et9gDdkpi4iYyFs3QlmoE9OKV7UaHgZ5w2VSLq6ZDG8zmKhX9jAsEho78DCsia7S", "", -1, "Bearer"));
            client = new Client(config, auth);
        }

        public async static Task DoSomeOauth2()
        {
            service = new RequestService(HANDLER);
            config = new CanvasConfig(DOMAIN, CLIENT_ID, CLIENT_SECRET, REDIRECT);

            var request = new ApiRequest(new Uri("https://instantadmin.instructure.com/login/oauth2/auth"))
                .Param("client_id", CLIENT_ID)
                .Param("response_type", "code")
                .Param("redirect_uri", REDIRECT.ToString())
                ;

            var basic = await HANDLER.ExecuteAsync<OAuth2Session>(request).ConfigureAwait(false);
            
            //session = new OAuth2Session(access_token, refresh_token, expires_in, token_type);
            auth = new AuthRepository(config, service, CONVERTER);
            
            client = new Client(config, auth);

            var courses = await client.CoursesManager.GetAll();

            foreach(var course in courses)
            {
                Console.WriteLine(course.ToString());
            }


            return;
        }

        public async static Task DoTheCourses()
        {
            //var self_account = await client.AccountsManager.GetSelf();
            //var all_courses = await client.CoursesManager.GetAll().ConfigureAwait(false);

            //foreach (var course in all_courses)
            //{
            //    Console.WriteLine(String.Format("Name: {0} | ID: {0}", course.Name, course.Id));
            //    var lookup_course = await client.CoursesManager.Get(course.Id);
            //    if (lookup_course != null)
            //        Console.WriteLine(String.Format("Lookup Success: Name: {0}, ID: {1} ", lookup_course.Name, lookup_course.Id));
            //}


            //var course_request = new CourseRequest();
            //course_request.Course = new Course()
            //{
            //    Name = "New Course From Test App",
            //    CourseCode = "TAC",
            //    PublicSyllabus = true,

            //};
            //course_request.AccountId = (int)self_account.Id;

            var new_course_request = new NewCourseRequest()
            {
                Name = "Test Open Enroll API 1",
                CourseCode = "API",
                Licence = "private",
                OpenEnrollment = true,
                SelfEnrollment = true,
                IsPublic = true,
                EnrollMe = true,
            };

            //var new_course = await client.AccountsManager.CreateNewCourse(new_course_request);
            //var course_id = new_course.Id;
            var course_id = 27;
            var course_for_secret_code = await client.CoursesManager.Get(course_id, true);

            var secret_code = course_for_secret_code.SelfEnrollmentCode;
            //var new_user = await client.UsersManager.CreateNewUser("thatsnotmylane+11@gmail.com", "Andrew Test1", "Test1, Andrew");
            var new_user = await client.UsersManager.CreateNewUser("thatsnotmylane+12", "Andrew NoEmail", "NoEmail, Andrew");

            var new_enrollment = await client.CoursesManager.EnrollUser(course_id, new_user.Id, EnrollmentType.StudentEnrollment);

            return;
        }

        public async static Task GetUsers()
        {
            var all_users = await client.AccountsManager.GetAll();
            foreach (var user in all_users)
            {
                Console.WriteLine(user.Id);
            }
        }
    }
}
