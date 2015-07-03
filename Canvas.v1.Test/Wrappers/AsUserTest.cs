using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Canvas.v1.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Canvas.v1.Test.Wrappers
{
    [TestClass]
    public class AsUserTest
    {
        private readonly Uri _uriWithoutQuery = new Uri("http://example.com");
        private readonly Uri _uriWithQuery = new Uri("http://example.com?foo=bar");

        [TestMethod]
        public void AsCanvasUser()
        {
            var asUser = new AsUser(AsUserType.Canvas, "1234");
            var actual = asUser.AppendQuery(_uriWithoutQuery);
            Assert.AreEqual("http://example.com/?as_user_id=1234", actual.AbsoluteUri);
        }

        [TestMethod]
        public void AsCanvasUser_Append()
        {
            var asUser = new AsUser(AsUserType.Canvas, "1234");
            var actual = asUser.AppendQuery(_uriWithQuery);
            Assert.AreEqual("http://example.com/?foo=bar&as_user_id=1234", actual.AbsoluteUri);
        }

        [TestMethod]
        public void AsSisUser()
        {
            var asUser = new AsUser(AsUserType.SIS, "joe");
            var actual = asUser.AppendQuery(_uriWithoutQuery);
            Assert.AreEqual("http://example.com/?as_user_id=sis_user_id%3Ajoe", actual.AbsoluteUri);
        }

        [TestMethod]
        public void AsSisUser_Append()
        {
            var asUser = new AsUser(AsUserType.SIS, "joe");
            var actual = asUser.AppendQuery(_uriWithQuery);
            Assert.AreEqual("http://example.com/?foo=bar&as_user_id=sis_user_id%3Ajoe", actual.AbsoluteUri);
        }
    }
}
