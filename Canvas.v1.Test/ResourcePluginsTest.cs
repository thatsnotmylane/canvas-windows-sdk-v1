using System;
using Canvas.v1.Managers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Canvas.v1.Test
{
    [TestClass]
    public class ResourcePluginsTest : ResourceManagerTest
    {
    
        [TestMethod]
        public void InitializePlugins_ValidResource_ValidPlugins()
        {
            // Arrange
            Client client = new Client(_config.Object);

            // Act
            client
                .AddResourcePlugin<CoursesManager>();

            var dm = client.ResourcePlugins.Get<CoursesManager>();

            // Assert
            Assert.IsNotNull(dm);
        }

        [TestMethod, Ignore]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InitializePlugins_UnregisteredResource_InvalidOperationException()
        {
            // Won't work until there are additional manager types.

            // Arrange
            Client client = new Client(_config.Object);

            // Act
            client.AddResourcePlugin<CoursesManager>();

            // Assert
            var dm = client.ResourcePlugins.Get<CoursesManager>();
        }

    }
}
