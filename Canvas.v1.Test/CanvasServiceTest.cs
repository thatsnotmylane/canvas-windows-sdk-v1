using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Canvas.v1.Auth;
using Canvas.v1.Config;
using Canvas.v1.Converter;
using Canvas.v1.Models;
using Canvas.v1.Request;
using Canvas.v1.Services;
using Canvas.v1.Wrappers;
using Canvas.v1.Wrappers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Canvas.v1.Test
{
    [TestClass]
    public class CanvasServiceTest
    {

        IJsonConverter _converter;
        Mock<IRequestHandler> _handler;
        IRequestService _service;
        Mock<ICanvasConfig> _boxConfig;
        IAuthRepository _authRepository;

        public CanvasServiceTest()
        {
            // Initial Setup
            _converter = new JsonConverter();
            _handler = new Mock<IRequestHandler>();
            _service = new RequestService(_handler.Object);
            _boxConfig = new Mock<ICanvasConfig>();

            OAuthSession session = new OAuthSession("fakeAccessToken", "fakeRefreshToken", 3600, "bearer");

            _authRepository = new AuthRepository(_boxConfig.Object, _service, _converter, session);
        }

        [TestMethod]
        public async Task QueueTask_MultipleThreads_OrderedResponse()
        {
            /*** Arrange ***/
            int numTasks = 1000;

            int count = 0;

            // Increments the access token each time a call is made to the API
            _handler.Setup(h => h.ExecuteAsync<OAuthSession>(It.IsAny<IApiRequest>()))
                .Returns(() => Task.FromResult<IApiResponse<OAuthSession>>(new ApiResponse<OAuthSession>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = "{\"access_token\": \"" + count + "\",\"expires_in\": 3600,\"token_type\": \"bearer\",\"refresh_token\": \"J7rxTiWOHMoSC1isKZKBZWizoRXjkQzig5C6jFgCVJ9bUnsUfGMinKBDLZWP9BgR\"}"
                })).Callback(() => System.Threading.Interlocked.Increment(ref count));

            /*** Act ***/
            IApiRequest request = new ApiRequest(new Uri("http://box.com"), "folders");

            List<Task<IApiResponse<OAuthSession>>> tasks = new List<Task<IApiResponse<OAuthSession>>>();
            for (int i = 0; i < numTasks; i++)
                tasks.Add(_service.EnqueueAsync<OAuthSession>(request));

            await Task.WhenAll(tasks);

            /*** Assert ***/
            for (int i = 0; i < numTasks; i++)
            {
                OAuthSession session = _converter.Parse<OAuthSession>(tasks[i].Result.ContentString);
                Assert.AreEqual(session.AccessToken, i.ToString());
            }
        }

    }
}
