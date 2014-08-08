using System;
using Canvas.v1.Extensions;
using Canvas.v1.Models;
using Canvas.v1.Wrappers;
using Canvas.v1.Wrappers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Canvas.v1.Test.Wrappers
{
    [TestClass]
    public class RequestTest
    {
        [TestMethod]
        public void ValidParameters_ValidRequest()
        {
            Uri baseUri = new Uri("http://example.com");
            IApiRequest request = new ApiRequest(baseUri, "api");
            request.Parameters.Add("test", "test2");

            Assert.AreEqual(request.Method, RequestMethod.Get);
            Assert.AreEqual(baseUri, request.Host);
            Assert.IsNotNull(request.Parameters);
        }

        [TestMethod]
        public void ValidParameters_Array()
        {
            Uri baseUri = new Uri("http://example.com");
            IApiRequest request = new ApiRequest(baseUri, "api");
            request.Param("test", new[] {"value1", "value2", "value3"});

            var param = request.Parameters["test[]"];
            Assert.IsNotNull(param);
            Assert.AreEqual(param, "value1&test[]=value2&test[]=value3");
        }

        [TestMethod]
        public void ValidParameters_SingleElementArray()
        {
            Uri baseUri = new Uri("http://example.com");
            IApiRequest request = new ApiRequest(baseUri, "api");
            request.Param("test", new[] { "value1" });

            var param = request.Parameters["test"];
            Assert.IsNotNull(param);
            Assert.AreEqual(param, "value1");
        }

        [TestMethod]
        public void ValidParameters_SingleValue()
        {
            Uri baseUri = new Uri("http://example.com");
            IApiRequest request = new ApiRequest(baseUri, "api");
            request.Param("test", "value1");

            var param = request.Parameters["test"];
            Assert.IsNotNull(param);
            Assert.AreEqual(param, "value1");
        }


        [TestMethod]
        public void EnumFlagsToString()
        {
            var states = CourseWorkflowState.Available | CourseWorkflowState.Completed | CourseWorkflowState.Deleted;
            var tostring = states.ToString().ToLower();
            Assert.AreEqual("available, completed, deleted", tostring);
        }
    }
}
