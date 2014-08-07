using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Canvas.v1.Auth;
using Canvas.v1.Config;
using Canvas.v1.Exceptions;
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
    public class AuthRepositoryTest : ResourceManagerTest
    {

        [TestMethod]
        [ExpectedException(typeof(CanvasException))]
        public async Task AuthenticateLive_InvalidAuthCode_Exception()
        {
            // Arrange
            IRequestHandler handler = new HttpRequestHandler();
            IRequestService service = new RequestService(handler);
            ICanvasConfig config = new CanvasConfig(null, null, null);

            IAuthRepository authRepository = new AuthRepository(config, service, _converter);

            // Act
            OAuthSession response = await authRepository.AuthenticateAsync("fakeAuthorizationCode");
        }

        [TestMethod]
        public async Task Authenticate_ValidResponse_ValidSession()
        {
            // Arrange
            _handler.Setup(h => h.ExecuteAsync<OAuthSession>(It.IsAny<IApiRequest>()))
                .Returns(Task<IApiResponse<OAuthSession>>.Factory.StartNew(() => new ApiResponse<OAuthSession>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = "{\"access_token\": \"T9cE5asGnuyYCCqIZFoWjFHvNbvVqHjl\",\"expires_in\": 3600,\"token_type\": \"bearer\",\"refresh_token\": \"J7rxTiWOHMoSC1isKZKBZWizoRXjkQzig5C6jFgCVJ9bUnsUfGMinKBDLZWP9BgR\"}"
                }));

            // Act
            OAuthSession session = await _authRepository.AuthenticateAsync("sampleauthorizationcode");

            // Assert
            Assert.AreEqual(session.AccessToken, "T9cE5asGnuyYCCqIZFoWjFHvNbvVqHjl");
            Assert.AreEqual(session.ExpiresIn, 3600);
            Assert.AreEqual(session.RefreshToken, "J7rxTiWOHMoSC1isKZKBZWizoRXjkQzig5C6jFgCVJ9bUnsUfGMinKBDLZWP9BgR");
            Assert.AreEqual(session.TokenType, "bearer");
        }

        [TestMethod]
        [ExpectedException(typeof(CanvasException))]
        public async Task Authenticate_ErrorResponse_Exception()
        {
            // Arrange
            _handler.Setup(h => h.ExecuteAsync<OAuthSession>(It.IsAny<IApiRequest>()))
                .Returns(Task<IApiResponse<OAuthSession>>.Factory.StartNew(() => new ApiResponse<OAuthSession>()
                {
                    Status = ResponseStatus.Error,
                    ContentString = "{\"error\": \"invalid_grant\",\"error_description\": \"Invalid user credentials\"}"
                }));

            // Act
            OAuthSession session = await _authRepository.AuthenticateAsync("fakeauthorizationcode");
        }

        [TestMethod]
        public async Task RefreshSession_ValidResponse_ValidSession()
        {
            // Arrange
            _handler.Setup(h => h.ExecuteAsync<OAuthSession>(It.IsAny<IApiRequest>()))
                .Returns(Task<IApiResponse<OAuthSession>>.Factory.StartNew(() => new ApiResponse<OAuthSession>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = "{\"access_token\": \"T9cE5asGnuyYCCqIZFoWjFHvNbvVqHjl\",\"expires_in\": 3600,\"token_type\": \"bearer\",\"refresh_token\": \"J7rxTiWOHMoSC1isKZKBZWizoRXjkQzig5C6jFgCVJ9bUnsUfGMinKBDLZWP9BgR\"}"
                }));

            // Act
            OAuthSession session = await _authRepository.RefreshAccessTokenAsync("fakeaccesstoken");

            // Assert
            Assert.AreEqual(session.AccessToken, "T9cE5asGnuyYCCqIZFoWjFHvNbvVqHjl");
            Assert.AreEqual(session.ExpiresIn, 3600);
            Assert.AreEqual(session.RefreshToken, "J7rxTiWOHMoSC1isKZKBZWizoRXjkQzig5C6jFgCVJ9bUnsUfGMinKBDLZWP9BgR");
            Assert.AreEqual(session.TokenType, "bearer");
        }


        [TestMethod]
        public async Task RefreshSession_MultipleThreadsSameAccessToken_SameSession()
        {

            /*** Arrange ***/
            int numTasks = 1000;

            int count = 0; 

            // Increments the access token each time a call is made to the API
            _handler.Setup(h => h.ExecuteAsync<OAuthSession>(It.IsAny<IApiRequest>()))
                .Returns(() => Task.FromResult<IApiResponse<OAuthSession>>(new ApiResponse<OAuthSession>() 
                {
                    Status = ResponseStatus.Success,
                    ContentString = "{\"access_token\": \""+ count + "\",\"expires_in\": 3600,\"token_type\": \"bearer\",\"refresh_token\": \"J7rxTiWOHMoSC1isKZKBZWizoRXjkQzig5C6jFgCVJ9bUnsUfGMinKBDLZWP9BgR\"}"
                })).Callback(() => System.Threading.Interlocked.Increment(ref count));

            /*** Act ***/
            List<Task<OAuthSession>> tasks = new List<Task<OAuthSession>>();

            for (int i = 0; i < numTasks; i++)
                tasks.Add(_authRepository.RefreshAccessTokenAsync("fakeAccessToken")); // Refresh with the same access token each time

            await Task.WhenAll(tasks);

            /*** Assert ***/
            var exceptions = tasks.Where(t => t.Status == TaskStatus.Faulted).Select(t => t.Exception);
            Assert.AreEqual(exceptions.Count(), 0);
            var completions = tasks.Where(t => t.Status == TaskStatus.RanToCompletion).Select(t => t.Result);
            Assert.AreEqual(completions.Count(), numTasks);
            var results = tasks.Where(t => t.Result.AccessToken == "0").Select(t => t.Result);
            Assert.AreEqual(results.Count(), numTasks);
        }
    }
}
