using System.Linq;
using System.Threading.Tasks;
using Canvas.v1.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Canvas.v1.Test.Integration
{
    [TestClass]
    public class BoxSearchManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        [TestMethod]
        public async Task SearchKeyword_LiveSession_ValidResponse()
        {
            const string keyword = "IMG";
            const int numResults = 13;
            const int numFiles = 12;
            const int numFolders = 1;

            BoxCollection<BoxItem> results = await _client.SearchManager.SearchAsync(keyword, 200);

            Assert.IsNotNull(results, "Search results are null");
            Assert.AreEqual(numResults, results.Entries.Count, "Incorrect number of search results");
            Assert.IsTrue(results.Entries.Count(item => item is BoxFolder) == numFolders, "Incorrect number of folders in search results");
            Assert.IsTrue(results.Entries.Count(item => item is BoxFile) == numFiles, "Incorrect number of files in search results");
        }

        [TestMethod]
        public async Task SearchKeyword_LiveSession_EmptyResult()
        {
            const string keyword = "NonExistentKeyWord";

            BoxCollection<BoxItem> results = await _client.SearchManager.SearchAsync(keyword, 200);

            Assert.IsNotNull(results, "Search results are null");
            Assert.AreEqual(0, results.Entries.Count, "Incorrect number of search results");
        }
    }
}
