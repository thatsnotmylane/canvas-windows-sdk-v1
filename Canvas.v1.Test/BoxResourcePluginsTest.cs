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
            BoxClient client = new BoxClient(_config.Object);

            // Act
            client.AddResourcePlugin<CoursesManager>();

            // Assert
            var dm = client.ResourcePlugins.Get<CoursesManager>();
        }

    }
}
