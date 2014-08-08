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
            Console.Out.WriteLine(string.Join(", ", accounts.Select(a=>a.Name)));
        }
    }
}