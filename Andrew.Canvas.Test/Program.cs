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

namespace Andrew.Canvas.Test
{
    class Program
    {
        public static string DOMAIN = "canvas.instructure.com";
        public static Uri REDIRECT = new Uri("https://canvas");
        public static JsonConverter CONVERTER = new JsonConverter();
        public static HttpRequestHandler HANDLER = new HttpRequestHandler();
        public static RequestService service;
        public static CanvasConfig config;
        public static AuthRepository auth;
        public static Client client;

        static void Main(string[] args)
        {
            Initialize();
            DoTheCourses().Wait();
            //GetUsers().Wait();
            return;
        }

        public static void Initialize()
        {
            service = new RequestService(HANDLER);
            config = new CanvasConfig(DOMAIN, "", "", REDIRECT);
            auth = new AuthRepository(config, service, CONVERTER, new OAuth2Session("7~DHwAbT82Vj4LujkiK5f0sHxvIjuAJMC5lg23G54R0qARwckBzBOttvI3WjDxBQa9", "", -1, "Bearer"));
            client = new Client(config, auth);
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

            var new_course = await client.AccountsManager.CreateNewCourse(new_course_request);
            var course_id = new_course.Id;
            var course_for_secret_code = await client.CoursesManager.Get(course_id, true);

            var secret_code = course_for_secret_code.SelfEnrollmentCode;
            

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
