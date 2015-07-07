using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Canvas.v1.Exceptions;
using Canvas.v1.Models;
using Canvas.v1.Models.Analytics;
using Canvas.v1.Models.Request;
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
        public async Task GetCourse()
        {
            var course = await _client.CoursesManager.Get(CourseId);
            Console.Out.WriteLine(course);
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

        [TestMethod]
        public async Task GetStudentSummaries()
        {
            var actual = await _client.AnalyticsManager.GetStudentSummaries("943314");
            Console.Out.WriteLine(actual);
        }

        [TestMethod]
        public async Task GetStudentAssignments()
        {
            var actual = await _client.AnalyticsManager.GetStudentAssignments("943314", "4517974");
            Console.Out.WriteLine(actual);
        }

        [TestMethod]
        public async Task GetCourseAssignments()
        {
            var actual = await _client.AnalyticsManager.GetCourseAssignments("943314");
            Console.Out.WriteLine(actual);
        }

        [TestMethod]
        public async Task GetCourseParticipations()
        {
            var actual = await _client.AnalyticsManager.GetCourseParticipations("943314");
            Console.Out.WriteLine(actual);
        }

        [TestMethod]
        public async Task GetStudentParticipations()
        {
            var actual = await _client.AnalyticsManager.GetStudentParticipations("943314", "4517974");
            Console.Out.WriteLine(actual);
        }


        [TestMethod]
        public async Task ConcurrencyTest()
        {
            List<Task> tasks = new List<Task>();
            tasks.Add(_client.CoursesManager.GetUsers(CourseId, itemsPerPage: 100, enrollmentType: UserEnrollmentType.Student, include: UserInclude.Email));
            tasks.Add(_client.CoursesManager.GetUsers(CourseId, itemsPerPage: 100, enrollmentType: UserEnrollmentType.Student, include: UserInclude.Enrollments));
            tasks.Add(_client.CoursesManager.GetUsers(CourseId, itemsPerPage: 100, enrollmentType: UserEnrollmentType.Student, include: UserInclude.Email | UserInclude.Enrollments));
            await Task.WhenAll(tasks);
        }
    }
}
