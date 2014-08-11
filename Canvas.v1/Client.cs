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
    /// The central entrypoint for all SDK interaction. The Client houses all of the API endpoints and are represented 
    /// as resource managers for each distinct endpoint
    /// </summary>
    public class Client
    {
        private readonly IRequestService _service;
        private readonly IJsonConverter _converter;
        private readonly ICanvasConfig _config;
        
        private Lazy<CoursesManager> _coursesManager;
        private Lazy<AccountsManager> _accountsManager;
        private Lazy<UsersManager> _usersManager;

        /// <summary>
        /// Instantiates a Client with the provided config object and auth session
        /// </summary>
        /// <param name="config">The config object to be used</param>
        /// <param name="authSession">A fully authenticated auth session</param>
        public Client(ICanvasConfig config, OAuth2Session authSession = null)
            : this(config, new AuthRepository(config, new RequestService(new HttpRequestHandler()), new JsonConverter(), authSession))
        {
            _config = config;
            _converter = new JsonConverter();
            _service = new RequestService(new HttpRequestHandler());
            Auth = new AuthRepository(config, new RequestService(new HttpRequestHandler()), new JsonConverter(), authSession);

            InitManagers();
        }

        /// <summary>
        /// Initializes a new Client with the provided config, converter, service and auth objects.
        /// </summary>
        /// <param name="config">The config object to use</param>
        /// <param name="auth">The auth repository object to use</param>
        public Client(ICanvasConfig config, IAuthRepository auth)
        {
            _config = config;
            _converter = new JsonConverter();
            _service = new RequestService(new HttpRequestHandler());
            Auth = auth;

            InitManagers();
        }

        private void InitManagers()
        {
            // Init Resource Managers
            _coursesManager = new Lazy<CoursesManager>(() => new CoursesManager(_config, _service, _converter, Auth));
            _accountsManager = new Lazy<AccountsManager>(() => new AccountsManager(_config, _service, _converter, Auth));
            _usersManager = new Lazy<UsersManager>(() => new UsersManager(_config, _service, _converter, Auth));
            
            ResourcePlugins = new ResourcePlugins();
        }

        /// <summary>
        /// Adds additional resource managers/endpoints to the Client.
        /// This is meant to allow for the inclusion of beta APIs or unofficial endpoints
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Client AddResourcePlugin<T>() where T : ResourceManager
        {
            ResourcePlugins.Register<T>(() => (T)Activator.CreateInstance(typeof(T), _config, _service, _converter, Auth));
            return this;
        }

        /// <summary>
        /// The manager that represents the /courses endpoint
        /// </summary>
        public CoursesManager CoursesManager
        {
            get { return _coursesManager.Value; }
        }

        /// <summary>
        /// The manager that represents the /accounts endpoint
        /// </summary>
        public AccountsManager AccountsManager
        {
            get { return _accountsManager.Value; }
        }

        /// <summary>
        /// The manager that represents the /users endpoint
        /// </summary>
        public UsersManager UsersManager
        {
            get { return _usersManager.Value; }
        }


        /// <summary>
        /// The Auth repository that holds the auth session
        /// </summary>
        public IAuthRepository Auth { get; private set; }

        /// <summary>
        /// Allows resource managers to be registered and retrieved as plugins
        /// </summary>
        public IResourcePlugins ResourcePlugins { get; private set; }

    }
}
