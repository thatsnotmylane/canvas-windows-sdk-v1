using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Canvas.v1.Test.Integration.Managers
{
    [TestClass]
    public class AccountsManagerTestIntegration : ResourceManagerTestIntegration
    {

        [TestMethod]
        public async Task GetAllAccountsManageableByUser()
        {
            var accounts = await _client.AccountsManager.GetAll();
            Console.Out.WriteLine(string.Join("\n", accounts));
        }

        [TestMethod]
        public async Task GetAccountById()
        {
            var account = await _client.AccountsManager.Get(AccountId);
            Console.Out.WriteLine(account);
        }

        [TestMethod]
        public async Task GetCoursesById()
        {
            var courses = await _client.AccountsManager.GetCourses(AccountId);
            Console.Out.WriteLine(string.Join("\n", courses));
        }
    }
}