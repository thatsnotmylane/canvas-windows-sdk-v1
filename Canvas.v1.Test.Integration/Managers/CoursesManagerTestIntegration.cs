using System;
using System.Threading.Tasks;
using Canvas.v1.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Canvas.v1.Test.Integration.Managers
{
    [TestClass]
    public class CoursesManagerTestIntegration : ResourceManagerTestIntegration
    {
        [TestMethod]
        public async Task GetAllAccountsManageableByUser()
        {
            var courses = await _client.CoursesManager.GetAll();
        }

        [TestMethod]
        public async Task GetCourses()
        {
            var users = await _client.CoursesManager.GetUsers(1234, perPage: 100, enrollmentType: UserEnrollmentType.Student, include: UserInclude.Email);
            Console.Out.WriteLine(string.Join("\n", users));
        }

    }
}
