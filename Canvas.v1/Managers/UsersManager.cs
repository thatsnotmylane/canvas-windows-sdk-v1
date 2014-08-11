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
    public class UsersManager : ResourceManager
    {
        public UsersManager(ICanvasConfig config, IRequestService service, IJsonConverter converter, IAuthRepository auth)
            : base(config, service, converter, auth)
        {
        }

        /// <summary>
        /// Get a user profile for the indicated user ID.
        /// </summary>
        /// <param name="id">The ID of the user. Defaults to the user represented by the current OAuth2 token.</param>
        /// <returns></returns>
        public async Task<UserProfile> Get(long? id = null)
        {
            if (id.HasValue) id.Value.ThrowIfUnassigned("id");

            var userId = id.HasValue ? id.Value.ToString() : "self";
            var request = new ApiRequest(_config.UsersEndpointUri, userId + "/profile");
            return await GetReponseAsync<UserProfile>(request);
        }

        /// <summary>
        /// Get a user profile for the indicated SIS Login ID. This method only applies for institutions using SIS Import to load data into Canvas.
        /// </summary>
        /// <param name="sisLoginId">The SIS Login ID of the user</param>
        /// <returns></returns>
        public async Task<UserProfile> GetBySisLoginId(string sisLoginId)
        {
            sisLoginId.ThrowIfNullOrWhiteSpace("sisLoginId");

            var request = new ApiRequest(_config.UsersEndpointUri, string.Format("sis_login_id:{0}/profile",sisLoginId));
            return await GetReponseAsync<UserProfile>(request);
        }

    }
}