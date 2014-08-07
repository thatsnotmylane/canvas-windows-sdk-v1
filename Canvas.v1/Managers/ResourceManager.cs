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
        protected IBoxService _service;
        protected IBoxConverter _converter;
        protected IAuthRepository _auth;

        /// <summary>
        /// Instantiates the base class for the Box resource managers
        /// </summary>
        /// <param name="config"></param>
        /// <param name="service"></param>
        /// <param name="converter"></param>
        /// <param name="auth"></param>
        public ResourceManager(ICanvasConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth)
        {
            _config = config;
            _service = service;
            _converter = converter; 
            _auth = auth;
        }

        protected IBoxRequest AddDefaultHeaders(IBoxRequest request)
        {
            request
                .Header(Constants.RequestParameters.UserAgent, _config.UserAgent)
                .Header(Constants.RequestParameters.AcceptEncoding, _config.AcceptEncoding.ToString());

            return request;
        }

        protected async Task<IBoxResponse<T>> ToResponseAsync<T>(IBoxRequest request, bool queueRequest = false)
            where T : class
        {
            AddDefaultHeaders(request);
            AddAuthorization(request);
            var response = await ExecuteRequest<T>(request, queueRequest).ConfigureAwait(false);

            return response.ParseResults(_converter);
        }

        private async Task<IBoxResponse<T>> ExecuteRequest<T>(IBoxRequest request, bool queueRequest)
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
        protected async Task<IBoxResponse<T>> RetryExpiredTokenRequest<T>(IBoxRequest request)
            where T : class
        {
            OAuthSession newSession = await _auth.RefreshAccessTokenAsync(request.Authorization).ConfigureAwait(false);
            AddDefaultHeaders(request);
            AddAuthorization(request, newSession.AccessToken);
            return await _service.ToResponseAsync<T>(request).ConfigureAwait(false);
        }

        protected void AddAuthorization(IBoxRequest request, string accessToken = null)
        {
            var auth = accessToken ?? _auth.Session.AccessToken;

            string authString = _auth.Session.AuthVersion == AuthVersion.V1 ? 
                string.Format(CultureInfo.InvariantCulture, Constants.V1AuthString, _config.ClientId, auth) : 
                string.Format(CultureInfo.InvariantCulture, Constants.V2AuthString, auth);

            StringBuilder sb = new StringBuilder(authString);

            // Appending device_id is required for accounts that have device pinning enabled on V1 auth
            if (_auth.Session.AuthVersion == AuthVersion.V1)
            { 
                sb.Append(string.IsNullOrWhiteSpace(_config.DeviceId) ? 
                    string.Empty : 
                    string.Format("&device_id={0}", _config.DeviceId));
                sb.Append(string.IsNullOrWhiteSpace(_config.DeviceName) ? 
                    string.Empty : 
                    string.Format("&device_name={0}", _config.DeviceName));
            }

            request.Authorization = auth;
            request.Header(Constants.AuthHeaderKey, sb.ToString());
        }


    }
}
