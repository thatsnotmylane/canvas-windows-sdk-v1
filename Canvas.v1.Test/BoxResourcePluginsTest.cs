using System;
using Canvas.v1.Managers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Canvas.v1.Test
{
    [TestClass]
    public class BoxResourcePluginsTest : BoxResourceManagerTest
    {
    
        [TestMethod]
        public void InitializePlugins_ValidResource_ValidPlugins()
        {
            // Arrange
            BoxClient client = new BoxClient(_config.Object);

            // Act
            client
                .AddResourcePlugin<BoxFilesManager>()
                .AddResourcePlugin<BoxFoldersManager>()
                .AddResourcePlugin<BoxCommentsManager>()
                .AddResourcePlugin<BoxGroupsManager>();

            var fm = client.ResourcePlugins.Get<BoxFilesManager>();
            var dm = client.ResourcePlugins.Get<BoxFoldersManager>();
            var cm = client.ResourcePlugins.Get<BoxCommentsManager>();
            var gm = client.ResourcePlugins.Get<BoxGroupsManager>();

            // Assert
            Assert.IsNotNull(fm);
            Assert.IsNotNull(dm);
            Assert.IsNotNull(cm);
            Assert.IsNotNull(gm);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InitializePlugins_UnregisteredResource_InvalidOperationException()
        {
            // Arrange
            BoxClient client = new BoxClient(_config.Object);

            // Act
            client.AddResourcePlugin<BoxFilesManager>();

            // Assert
            var dm = client.ResourcePlugins.Get<BoxFoldersManager>();
        }

    }
}
