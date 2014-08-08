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
            ICanvasConfig config = new CanvasConfig("domain", null, null, null);

            IAuthRepository authRepository = new AuthRepository(config, service, _converter);

            // Act
            OAuth2Session response = await authRepository.AuthenticateAsync("fakeAuthorizationCode");
        }

        [TestMethod]
        public async Task Authenticate_ValidResponse_ValidSession()
        {
            // Arrange
            _handler.Setup(h => h.ExecuteAsync<OAuth2Session>(It.IsAny<IApiRequest>()))
                .Returns(Task<IApiResponse<OAuth2Session>>.Factory.StartNew(() => new ApiResponse<OAuth2Session>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = "{\"access_token\": \"T9cE5asGnuyYCCqIZFoWjFHvNbvVqHjl\",\"expires_in\": 3600,\"token_type\": \"bearer\",\"refresh_token\": \"J7rxTiWOHMoSC1isKZKBZWizoRXjkQzig5C6jFgCVJ9bUnsUfGMinKBDLZWP9BgR\"}"
                }));

            // Act
            OAuth2Session session = await _authRepository.AuthenticateAsync("sampleauthorizationcode");

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
            _handler.Setup(h => h.ExecuteAsync<OAuth2Session>(It.IsAny<IApiRequest>()))
                .Returns(Task<IApiResponse<OAuth2Session>>.Factory.StartNew(() => new ApiResponse<OAuth2Session>()
                {
                    Status = ResponseStatus.Error,
                    ContentString = "{\"error\": \"invalid_grant\",\"error_description\": \"Invalid user credentials\"}"
                }));

            // Act
            OAuth2Session session = await _authRepository.AuthenticateAsync("fakeauthorizationcode");
        }

        [TestMethod]
        public async Task RefreshSession_ValidResponse_ValidSession()
        {
            // Arrange
            _handler.Setup(h => h.ExecuteAsync<OAuth2Session>(It.IsAny<IApiRequest>()))
                .Returns(Task<IApiResponse<OAuth2Session>>.Factory.StartNew(() => new ApiResponse<OAuth2Session>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = "{\"access_token\": \"T9cE5asGnuyYCCqIZFoWjFHvNbvVqHjl\",\"expires_in\": 3600,\"token_type\": \"bearer\",\"refresh_token\": \"J7rxTiWOHMoSC1isKZKBZWizoRXjkQzig5C6jFgCVJ9bUnsUfGMinKBDLZWP9BgR\"}"
                }));

            // Act
            OAuth2Session session = await _authRepository.RefreshAccessTokenAsync("fakeaccesstoken");

            // Assert
            Assert.AreEqual(session.AccessToken, "T9cE5asGnuyYCCqIZFoWjFHvNbvVqHjl");
            Assert.AreEqual(session.ExpiresIn, 3600);
            Assert.AreEqual(session.RefreshToken, "J7rxTiWOHMoSC1isKZKBZWizoRXjkQzig5C6jFgCVJ9bUnsUfGMinKBDLZWP9BgR");
            Assert.AreEqual(session.TokenType, "bearer");
        }
    }
}
