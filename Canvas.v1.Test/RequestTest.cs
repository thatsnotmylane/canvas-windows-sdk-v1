using System;
using Canvas.v1.Wrappers;
using Canvas.v1.Wrappers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Canvas.v1.Test
{
    [TestClass]
    public class RequestTest
    {
        [TestMethod]
        public void ValidParameters_ValidRequest()
        {
            Uri baseUri = new Uri("http://api.box.com/v2");
            IBoxRequest request = new BoxRequest(baseUri, "auth/oauth2");
            request.Parameters.Add("test", "test2");

            Assert.AreEqual(request.Method, RequestMethod.Get);
            Assert.AreEqual(baseUri, request.Host);
            Assert.IsNotNull(request.Parameters);
        }
    }
}
