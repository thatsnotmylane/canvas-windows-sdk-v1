using System;
using System.Threading.Tasks;
using Canvas.v1.Auth;
using Canvas.v1.Config;
using Canvas.v1.Converter;
using Canvas.v1.Models;
using Canvas.v1.Request;
using Canvas.v1.Services;
using Canvas.v1.Wrappers;
using Canvas.v1.Wrappers.Contracts;
using Moq;

namespace Canvas.v1.Test.Managers
{
    public abstract class ResourceManagerTest 
    {

        protected IJsonConverter _converter;
        protected Mock<IRequestHandler> _handler;
        protected IRequestService _service;
        protected Mock<ICanvasConfig> _config;
        protected AuthRepository _authRepository;

        protected Uri _baseUri = new Uri(Constants.ApiUriString);

        public ResourceManagerTest()
        {
            // Initial Setup
            _converter = new JsonConverter();
            _handler = new Mock<IRequestHandler>();
            _service = new RequestService(_handler.Object);
            _config = new Mock<ICanvasConfig>();

            _authRepository = new AuthRepository(_config.Object, _service, _converter, new OAuth2Session("fakeAccessToken", "fakeRefreshToken", 3600, "bearer"));
        }

        protected void ArrangeSuccessfulResponse<T>(string contentString) where T: class 
        {
            _handler.Setup(h => h.ExecuteAsync<T>(It.IsAny<IApiRequest>()))
                .Returns(() => Task.FromResult<IApiResponse<T>>(new ApiResponse<T>
                {
                    Status = ResponseStatus.Success,
                    ContentString = contentString
                }));
        }
    }
}
