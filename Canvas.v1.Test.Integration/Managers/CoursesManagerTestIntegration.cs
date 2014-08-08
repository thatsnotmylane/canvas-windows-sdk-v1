using System.Threading.Tasks;
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
    }
}
