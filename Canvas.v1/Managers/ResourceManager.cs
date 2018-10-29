using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Canvas.v1.Auth;
using Canvas.v1.Config;
using Canvas.v1.Converter;
using Canvas.v1.Extensions;
using Canvas.v1.Models;
using Canvas.v1.Services;
using Canvas.v1.Wrappers;
using Canvas.v1.Wrappers.Contracts;

namespace Canvas.v1.Managers
{
    /// <summary>
    /// The base class for all of the Box resource managers
    /// </summary>
    public abstract class ResourceManager
    {
        protected const string ParamFields = "fields";

        protected ICanvasConfig _config;
        protected IRequestService _service;
        protected IJsonConverter _converter;
        protected IAuthRepository _auth;

        /// <summary>
        /// Instantiates the base class for the Box resource managers
        /// </summary>
        /// <param name="config"></param>
        /// <param name="service"></param>
        /// <param name="converter"></param>
        /// <param name="auth"></param>
        public ResourceManager(ICanvasConfig config, IRequestService service, IJsonConverter converter, IAuthRepository auth)
        {
            _config = config;
            _service = service;
            _converter = converter;
            _auth = auth;
        }

        protected IApiRequest AddDefaultHeaders(IApiRequest request)
        {
            request
                .Header(Constants.RequestParameters.UserAgent, _config.UserAgent);
            //                .Header(Constants.RequestParameters.AcceptEncoding, "gzip, deflate");

            return request;
        }

        protected async Task<IApiResponse<T>> ToResponseAsync<T>(IApiRequest request, bool queueRequest = false)
            where T : class
        {
            AddDefaultHeaders(request);
            AddAuthorization(request);
            var response = await ExecuteRequest<T>(request, queueRequest).ConfigureAwait(false);

            return response.ParseResults(_converter);
        }

        private async Task<IApiResponse<T>> ExecuteRequest<T>(IApiRequest request, bool queueRequest)
            where T : class
        {
            var response = queueRequest ?
                await _service.EnqueueAsync<T>(request).ConfigureAwait(false) :
                await _service.ToResponseAsync<T>(request).ConfigureAwait(false);

            if (response.Status == ResponseStatus.Unauthorized)
            {
                // Refresh the access token if the status is "Unauthorized" (HTTP Status Code 401: Unauthorized)
                // This will only be attempted once as refresh tokens are single use
                response = await RetryExpiredTokenRequest<T>(request).ConfigureAwait(false);
            }

            return response;
        }

        /// <summary>
        /// Retry the request once if the first try was due to an expired token
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        protected async Task<IApiResponse<T>> RetryExpiredTokenRequest<T>(IApiRequest request)
            where T : class
        {
            OAuth2Session newSession = await _auth.RefreshAccessTokenAsync(request.Authorization).ConfigureAwait(false);
            AddDefaultHeaders(request);
            AddAuthorization(request, newSession.AccessToken);
            return await _service.ToResponseAsync<T>(request).ConfigureAwait(false);
        }

        protected void AddAuthorization(IApiRequest request, string accessToken = null)
        {
            var auth = accessToken ?? _auth.Session.AccessToken;

            string authString = string.Format(CultureInfo.InvariantCulture, Constants.V2AuthString, auth);

            request.Authorization = auth;
            request.Header(Constants.AuthHeaderKey, authString);
        }


        protected async Task<T> GetReponseAsync<T>(ApiRequest request) where T : class
        {
            var response = await ToResponseAsync<T>(request).ConfigureAwait(false);
            return response.ResponseObject;
        }
    }
}
