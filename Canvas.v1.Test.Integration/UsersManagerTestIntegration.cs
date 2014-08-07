using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Canvas.v1.Test.Integration
{
    [TestClass]
    public class UsersManagerTestIntegration : ResourceManagerTestIntegration
    {
        [TestMethod]
        public async Task GetMyProfile()
        {
            var user = await _client.UsersManager.Get();
            Console.Out.WriteLine(user.Name);
        }

        [TestMethod]
        public async Task GetProfileById()
        {
            var user = await _client.UsersManager.Get("10000005650232");
            Console.Out.WriteLine(user.Name);
        }
    }
}