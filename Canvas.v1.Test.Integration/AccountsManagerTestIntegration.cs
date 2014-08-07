using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Canvas.v1.Test.Integration
{
    [TestClass]
    public class AccountsManagerTestIntegration : ResourceManagerTestIntegration
    {
        [TestMethod]
        public async Task GetAllAccountsManageableByUser()
        {
            var accounts = await _client.AccountsManager.GetAll();
        }
    }
}