using System;
using Canvas.v1.Auth;
using Canvas.v1.Config;
using Canvas.v1.Converter;
using Canvas.v1.Managers;
using Canvas.v1.Models;
using Canvas.v1.Plugin;
using Canvas.v1.Request;
using Canvas.v1.Services;

namespace Canvas.v1
{
    /// <summary>
    /// The central entrypoint for all SDK interaction. The BoxClient houses all of the API endpoints and are represented 
    /// as resource managers for each distinct endpoint
    /// </summary>
    public class BoxClient
    {
        protected readonly IBoxService _service;
        protected readonly IBoxConverter _converter;
        protected readonly IRequestHandler _handler;

        /// <summary>
        /// Instantiates a BoxClient with the provided config object
        /// </summary>
        /// <param name="canvasConfig">The config object to be used</param>
        public BoxClient(ICanvasConfig canvasConfig) : this(canvasConfig, null) { }

        /// <summary>
        /// Instantiates a BoxClient with the provided config object and auth session
        /// </summary>
        /// <param name="canvasConfig">The config object to be used</param>
        /// <param name="authSession">A fully authenticated auth session</param>
        public BoxClient(ICanvasConfig canvasConfig, OAuthSession authSession)
        {
            Config = canvasConfig;
            
            _handler = new HttpRequestHandler();
            _converter = new BoxJsonConverter();
            _service = new BoxService(_handler);
            Auth = new AuthRepository(Config, _service, _converter, authSession);

            InitManagers();
        }

        /// <summary>
        /// Initializes a new BoxClient with the provided config, converter, service and auth objects.
        /// </summary>
        /// <param name="canvasConfig">The config object to use</param>
        /// <param name="boxConverter">The box converter object to use</param>
        /// <param name="boxService">The box service to use</param>
        /// <param name="auth">The auth repository object to use</param>
        public BoxClient(ICanvasConfig canvasConfig, IBoxConverter boxConverter, IRequestHandler requestHandler, IBoxService boxService, IAuthRepository auth)
        {
            Config = canvasConfig;

            _handler = requestHandler;
            _converter = boxConverter;
            _service = boxService;
            Auth = auth;

            InitManagers();
        }

        private void InitManagers()
        {
            // Init Resource Managers
            CoursesManager = new CoursesManager(Config, _service, _converter, Auth);

            // Init Resource Plugins Manager
            ResourcePlugins = new BoxResourcePlugins();
        }

        /// <summary>
        /// Adds additional resource managers/endpoints to the BoxClient.
        /// This is meant to allow for the inclusion of beta APIs or unofficial endpoints
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public BoxClient AddResourcePlugin<T>() where T : ResourceManager
        {
            ResourcePlugins.Register<T>(() => (T)Activator.CreateInstance(typeof(T), Config, _service, _converter, Auth));
            return this;
        }

        /// <summary>
        /// The configuration parameters used by the Box Service
        /// </summary>
        public ICanvasConfig Config { get; private set; }

        /// <summary>
        /// The manager that represents the courses endpoint
        /// </summary>
        public CoursesManager CoursesManager { get; private set; }

        /// <summary>
        /// The Auth repository that holds the auth session
        /// </summary>
        public IAuthRepository Auth { get; private set; }

        /// <summary>
        /// Allows resource managers to be registered and retrieved as plugins
        /// </summary>
        public BoxResourcePlugins ResourcePlugins { get; private set; }

    }
}
