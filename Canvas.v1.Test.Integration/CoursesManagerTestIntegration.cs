using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Canvas.v1.Config;
using Canvas.v1.Models;
using Canvas.v1.Models.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Canvas.v1.Test.Integration
{
    [TestClass]
    public class CoursesManagerTestIntegration : ResourceManagerTestIntegration
    {
        [TestMethod]
        public async Task GetFolder_LiveSession_ValidResponse()
        {
            await AssertFolderContents(_client);
        }

        [TestMethod]
        public async Task GetFolder_LiveSession_ValidResponse_GzipCompression()
        {
            var boxConfig = new CanvasConfig(ClientId, ClientSecret, RedirectUri){AcceptEncoding = CompressionType.gzip};
            var boxClient = new BoxClient(boxConfig, _auth);
            await AssertFolderContents(boxClient);
        }

        [TestMethod]
        public async Task GetFolder_LiveSession_ValidResponse_DeflateCompression()
        {
            var boxConfig = new CanvasConfig(ClientId, ClientSecret, RedirectUri) { AcceptEncoding = CompressionType.deflate };
            var boxClient = new BoxClient(boxConfig, _auth);
            await AssertFolderContents(boxClient);
        }

        private static async Task AssertFolderContents(BoxClient boxClient)
{
            const int totalCount = 11;
            const int numFiles = 9;
            const int numFolders = 2;

            BoxCollection<BoxItem> c = await boxClient.CoursesManager.GetFolderItemsAsync("0", 50, 0, new List<string>() { 
                BoxItem.FieldName, 
                BoxItem.FieldSize, 
                BoxFolder.FieldItemCollection
             });

            Assert.AreEqual(totalCount, c.TotalCount, "Incorrect total count");
            Assert.AreEqual(totalCount, c.Entries.Count, "Incorrect number if items returned");

            Assert.AreEqual(numFolders, c.Entries.Count(item => item is BoxFolder), "Wrong number of Folders");
            Assert.AreEqual(numFiles, c.Entries.Count(item => item is BoxFile), "Wrong number of Files");
        }

        [TestMethod]
        public async Task FolderGetTrashItems_LiveSession_ValidResponse()
        {
            var results = await _client.CoursesManager.GetTrashItemsAsync(10);
            Assert.IsNotNull(results);
        }

        [TestMethod]
        public async Task FolderWorkflow_LiveSession_ValidResponse()
        {
            string testName = GetUniqueName();

            // Test Create Folder
            BoxFolderRequest folderReq = new BoxFolderRequest() {
                Name = testName,
                Parent = new BoxRequestEntity() { Id = "0" }
            };

            BoxFolder f = await _client.CoursesManager.CreateAsync(folderReq);

            Assert.IsNotNull(f, "Folder was not created");
            Assert.AreEqual(testName, f.Name, "Folder with incorrect name was created");

            // Test Get Information
            BoxFolder fi = await _client.CoursesManager.GetInformationAsync(f.Id);

            Assert.AreEqual(f.Id, fi.Id, "Folder Ids are not identical");
            Assert.AreEqual(testName, fi.Name, "folder name is incorrect");

            // Test Create Shared Link
            BoxSharedLinkRequest sharedLinkReq = new BoxSharedLinkRequest()
            {
                Access = BoxSharedLinkAccessType.open
            };

            BoxFolder fsl = await _client.CoursesManager.CreateSharedLinkAsync(f.Id, sharedLinkReq);

            Assert.AreEqual(BoxSharedLinkAccessType.open, fsl.SharedLink.Access, "Shared link Access is not correct");

            // Test Update Folder Information
            string newTestname = GetUniqueName();
            BoxFolderRequest updateReq = new BoxFolderRequest()
            {
                Id = f.Id,
                Name = newTestname,
                SyncState = BoxSyncStateType.not_synced,
                FolderUploadEmail = new BoxEmailRequest { Access = "open" }
            };

            BoxFolder uf = await _client.CoursesManager.UpdateInformationAsync(updateReq);

            Assert.AreEqual(newTestname, uf.Name, "New folder name is not correct");

            // Test Copy Folder
            string copyTestName = GetUniqueName();
            BoxFolderRequest copyReq = new BoxFolderRequest()
            {
                Id = f.Id,
                Parent = new BoxRequestEntity() { Id = "0" },
                Name = copyTestName
            };

            BoxFolder f2 = await _client.CoursesManager.CopyAsync(copyReq);

            Assert.AreEqual(copyTestName, f2.Name, "Copied file does not have correct name");

            //Clean up - Delete Test Folders
            await _client.CoursesManager.DeleteAsync(f.Id, true);
            await _client.CoursesManager.DeleteAsync(f2.Id, true);
        }
    }
}
