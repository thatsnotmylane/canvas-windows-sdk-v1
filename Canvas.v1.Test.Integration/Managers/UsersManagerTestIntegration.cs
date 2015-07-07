using System;
using System.Threading.Tasks;
using Canvas.v1.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Canvas.v1.Test.Integration.Managers
{
    [TestClass]
    public class UsersManagerTestIntegration : ResourceManagerTestIntegration
    {
        [TestMethod]
        public async Task GetMyProfile()
        {
            var user = await _client.UsersManager.Get();
            AssertProperties(user);
        }

        [TestMethod]
        public async Task GetProfileById()
        {
            var user = await _client.UsersManager.Get(5650232);
            AssertProperties(user);
        }

        [TestMethod]
        public async Task GetProfileBySisLoginId()
        {
            var user = await _client.UsersManager.GetBySisLoginId("jhoerr");
            AssertProperties(user);
        }

        private static void AssertProperties(User user)
        {
            Assert.AreEqual(user.Name, "John Hoerr");
            Assert.AreEqual(user.LoginId, "jhoerr");
            Console.Out.WriteLine(user.Name);
        }
    }
}