using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Canvas.v1.Managers;
using Canvas.v1.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Canvas.v1.Test.Managers
{
    [TestClass]
    public class AccountsManagerTests : ResourceManagerTest
    {
        private readonly AccountsManager _accountsManager;

        public AccountsManagerTests()
        {
            _accountsManager = new AccountsManager(_config.Object, _service, _converter, _authRepository);
        }

        [TestMethod]
        public async Task GetAllAccounts()
        {
            const string content = @"[{""id"":1234,""name"":""Indiana University"",""parent_account_id"":null,""root_account_id"":null,""workflow_state"":""active"",""default_storage_quota_mb"":500,""default_user_storage_quota_mb"":100,""default_group_storage_quota_mb"":300,""default_time_zone"":""America/New_York""}]";

            ArrangeSuccessfulResponse<IEnumerable<Account>>(content);

            IEnumerable<Account> accounts = await _accountsManager.GetAll();

            Assert.AreEqual(accounts.Count(), 1);
            Account account = accounts.Single();
            Assert.AreEqual(account.Id, "1234");
            Assert.AreEqual(account.Name, "Indiana University");
            Assert.AreEqual(account.ParentAccountId, null);
            Assert.AreEqual(account.RootAccountId, null);
            Assert.AreEqual(account.WorkflowState, AccountWorkflowState.Active);
            Assert.AreEqual(account.DefaultStorageQuotaMB, 500);
            Assert.AreEqual(account.DefaultUserStorageQuotaMB, 100);
            Assert.AreEqual(account.DefaultGroupStorageQuotaMB, 300);
            Assert.AreEqual(account.DefaultTimeZone, "America/New_York");
        }
    }
}