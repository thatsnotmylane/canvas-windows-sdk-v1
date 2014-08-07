using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Canvas.v1.Auth.EventArgs;
using Canvas.v1.Config;
using Canvas.v1.Converter;
using Canvas.v1.Exceptions;
using Canvas.v1.Extensions;
using Canvas.v1.Models;
using Canvas.v1.Services;
using Canvas.v1.Wrappers;
using Canvas.v1.Wrappers.Contracts;
using Nito.AsyncEx;

namespace Canvas.v1.Auth
{
    /// <summary>
    /// Default auth repository implementation that will manage the life cycle of the authentication tokens. 
    /// This class can be extended to provide your own token management implementation by overriding the virtual methods
    /// </summary>
    public class AuthRepository : IAuthRepository
    {
        protected ICanvasConfig _config;
        protected IRequestService _service;
        protected IJsonConverter _converter;

        private List<string> _expiredTokens = new List<string>();
        private readonly AsyncLock _mutex = new AsyncLock();

        /// <summary>
        /// Fires when the authenticaiton session is invalidated
        /// </summary>
        public event EventHandler SessionInvalidated;

        /// <summary>
        /// Fires when a new set of auth token and refresh token pair has been fetched
        /// </summary>
        public event EventHandler<SessionAuthenticatedEventArgs> SessionAuthenticated;

        /// <summary>
        /// Instantiates a new AuthRepository
        /// </summary>
        /// <param name="canvasConfig">The Canvas app configuration that should be used</param>
        /// <param name="requestService">The service that will be used to make the requests</param>
        /// <param name="converter">How requests/responses will be serialized/deserialized respectively</param>
        public AuthRepository(ICanvasConfig canvasConfig, IRequestService requestService, IJsonConverter converter) : this(canvasConfig, requestService, converter, null) { }

        /// <summary>
        /// Instantiates a new AuthRepository
        /// </summary>
        /// <param name="canvasConfig">The Canvas app configuration that should be used</param>
        /// <param name="requestService">The service that will be used to make the requests</param>
        /// <param name="converter">How requests/responses will be serialized/deserialized respectively</param>
        /// <param name="session">The current authenticated session</param>
        public AuthRepository(ICanvasConfig canvasConfig, IRequestService requestService, IJsonConverter converter, OAuth2Session session)
        {
            _config = canvasConfig;
            _service = requestService;
            _converter = converter;
            Session = session;
        }

        /// <summary>
        /// The current user's OAuth2 credentials
        /// </summary>
        public OAuth2Session Session { get; protected set; }

        #region Overrideable Methods

        /// <summary>
        /// Authenticates the session by exchanging the provided auth code for a Access/Refresh token pair
        /// </summary>
        /// <param name="authCode"></param>
        /// <returns></returns>
        public virtual async Task<OAuth2Session> AuthenticateAsync(string authCode)
        {
            OAuth2Session session = await ExchangeAuthCode(authCode);

            using (await _mutex.LockAsync().ConfigureAwait(false))
            {
                Session = session;

                OnSessionAuthenticated(session);
            }

            return session;
        }

        /// <summary>
        /// Refreshes the session by exchanging the access token for a new Access/Refresh token pair. In general,
        /// this method should not need to be called explicitly, as an automatic refresh is invoked when the SDK 
        /// detects that the tokens have expired. 
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public virtual async Task<OAuth2Session> RefreshAccessTokenAsync(string accessToken)
        {
            OAuth2Session session;
            using (await _mutex.LockAsync().ConfigureAwait(false))
            {
                if (_expiredTokens.Contains(accessToken))
                {
                    session = Session;
                }
                else
                {
                    // Add the expired token to the list so subsequent calls will get new acces token. Add
                    // token to the list before making the network call. This way, if refresh fails, subsequent calls
                    // with the same refresh token will not attempt te call. 
                    _expiredTokens.Add(accessToken);

                    session = await ExchangeRefreshToken(Session.RefreshToken).ConfigureAwait(false);
                    Session = session;

                    OnSessionAuthenticated(session);
                }
            }

            return session;
        }

        #endregion


        /// <summary>
        /// Performs the authentication request using the provided auth code
        /// </summary>
        /// <param name="authCode"></param>
        /// <returns></returns>
        protected async Task<OAuth2Session> ExchangeAuthCode(string authCode)
        {
            if (string.IsNullOrWhiteSpace(authCode))
                throw new ArgumentException("Auth code cannot be null or empty", "authCode");

            ApiRequest apiRequest = new ApiRequest(_config.CanvasApiHostUri, Constants.AuthTokenEndpointString)
                .Method(RequestMethod.Post)
                .Payload(Constants.RequestParameters.GrantType, Constants.RequestParameters.AuthorizationCode)
                .Payload(Constants.RequestParameters.Code, authCode)
                .Payload(Constants.RequestParameters.ClientId, _config.ClientId)
                .Payload(Constants.RequestParameters.ClientSecret, _config.ClientSecret);

            IApiResponse<OAuth2Session> apiResponse = await _service.ToResponseAsync<OAuth2Session>(apiRequest).ConfigureAwait(false);
            apiResponse.ParseResults(_converter);

            return apiResponse.ResponseObject;
        }

        /// <summary>
        /// Performs the refresh request using the provided refresh token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        protected async Task<OAuth2Session> ExchangeRefreshToken(string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
                throw new ArgumentException("Refresh token cannot be null or empty", "refreshToken");

            ApiRequest apiRequest = new ApiRequest(_config.CanvasApiHostUri, Constants.AuthTokenEndpointString)
                .Method(RequestMethod.Post)
                .Payload(Constants.RequestParameters.GrantType, Constants.RequestParameters.RefreshToken)
                .Payload(Constants.RequestParameters.RefreshToken, refreshToken)
                .Payload(Constants.RequestParameters.ClientId, _config.ClientId)
                .Payload(Constants.RequestParameters.ClientSecret, _config.ClientSecret);

            IApiResponse<OAuth2Session> apiResponse = await _service.ToResponseAsync<OAuth2Session>(apiRequest).ConfigureAwait(false);
            if (apiResponse.Status == ResponseStatus.Success)
            {
                // Parse and return the new session
                apiResponse.ParseResults(_converter);
                return apiResponse.ResponseObject;
            }

            // The session has been invalidated, notify subscribers
            OnSessionInvalidated();

            // As well as the caller
            throw new SessionInvalidatedException()
            {
                StatusCode = apiResponse.StatusCode,
            };
        }

        /// <summary>
        /// Allows sub classes to invoke the SessionInvalidated event
        /// </summary>
        protected void OnSessionInvalidated()
        {
            var handler = SessionInvalidated;
            if (handler != null)
            {
                handler(this, new System.EventArgs());
            }
        }

        /// <summary>
        /// Allows sub classes to invoke the SessionAuthenticated event
        /// </summary>
        /// <param name="e"></param>
        protected void OnSessionAuthenticated(OAuth2Session session)
        {
            var handler = SessionAuthenticated;
            if (handler != null)
            {
                handler(this, new SessionAuthenticatedEventArgs(session));
            }
        }

    }
}
