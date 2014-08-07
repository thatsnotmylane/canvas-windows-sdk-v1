using System;
using Canvas.v1.Config;
using Canvas.v1.Converter;
using Canvas.v1.Models;
using Canvas.v1.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Canvas.v1.Test.Integration
{
    [TestClass]
    public abstract class ResourceManagerTestIntegration
    {
        public const string ClientId = "YOUR_CLIENT_ID";
        public const string ClientSecret = "YOUR_CLIENT_SECRET";

        public Uri RedirectUri = new Uri("http://boxsdk");

        protected OAuth2Session _auth;
        protected Client _client;
        protected ICanvasConfig _config;
        protected IRequestHandler _handler;
        protected IJsonConverter _parser;

        public ResourceManagerTestIntegration()
        {
            _auth = new OAuth2Session("YOUR_ACCESS_TOKEN", "YOUR_REFRESH_TOKEN", 3600, "bearer");

            _handler = new HttpRequestHandler();
            _parser = new JsonConverter();
            _config = new CanvasConfig(ClientId, ClientSecret, RedirectUri);
            _client = new Client(_config, _auth);
        }

        protected string GetUniqueName()
        {
            return string.Format("test{0}", Guid.NewGuid().ToString());
        }

        #region Test Properties

        //private string _testFolderId = "0";
        //public string TestFolderId
        //{
        //    get { return _testFolderId; }
        //    set { _testFolderId = value; }
        //}

        //private string _testFileId = "7869094982";
        //public string TestFileId
        //{
        //    get { return _testFileId; }
        //    set { _testFileId = value; }
        //}


        #endregion
    }
}
