using System.Threading.Tasks;
using Canvas.v1.Auth;
using Canvas.v1.Config;
using Canvas.v1.Converter;
using Canvas.v1.Models;
using Canvas.v1.Services;
using Canvas.v1.Wrappers;
using Canvas.v1.Wrappers.Contracts;

namespace Canvas.v1.Managers
{
    public class UsersManager : ResourceManager
    {
        public UsersManager(ICanvasConfig config, IRequestService service, IJsonConverter converter, IAuthRepository auth)
            : base(config, service, converter, auth)
        {
        }

        /// <summary>
        /// Get a user profile for the indicated user.
        /// </summary>
        /// <param name="id">The ID of the user. Defaults to the user represented by the current OAuth2 token.</param>
        /// <returns></returns>
        public async Task<UserProfile> Get(string id = "self")
        {
            var request = new ApiRequest(_config.UsersEndpointUri, id + "/profile");

            IApiResponse<UserProfile> response = await ToResponseAsync<UserProfile>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }
    }
}