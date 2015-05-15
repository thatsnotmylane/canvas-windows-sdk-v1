using System;
using System.Net;
using System.Threading.Tasks;
using Canvas.v1.Exceptions;
using Canvas.v1.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Canvas.v1.Test.Integration.Managers
{
    [TestClass]
    public class CoursesManagerTestIntegration : ResourceManagerTestIntegration
    {
        [TestMethod]
        public async Task GetAllCoursesManageableByUser()
        {
            var courses = await _client.CoursesManager.GetAll();
            Console.Out.WriteLine(string.Join("\n", courses));
        }

        [TestMethod]
        public async Task GetStudentsInCourse()
        {
            var users = await _client.CoursesManager.GetUsers(CourseId, itemsPerPage: 100, enrollmentType: UserEnrollmentType.Student, include: UserInclude.Email|UserInclude.Enrollments);
            Console.Out.WriteLine(string.Join("\n", users));
        }

        [TestMethod]
        public async Task GetStudentsInCourse_UnknownCourse()
        {
            try
            {
                await _client.CoursesManager.GetUsers(123412341234);
                Assert.Fail("Should have thrown a 'not found' CanvasException");
            }
            catch (CanvasException e)
            {
                Assert.AreEqual(HttpStatusCode.NotFound, e.StatusCode);
                Assert.AreEqual("The specified resource does not exist.", e.Message);
            }
        }
    }
}
