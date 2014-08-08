using System.Collections.Generic;
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
    public class AccountsManager : ResourceManager
    {
        public AccountsManager(ICanvasConfig config, IRequestService service, IJsonConverter converter, IAuthRepository auth) : base(config, service, converter, auth)
        {
        }

        /// <summary>
        /// List accounts that the current user can view or manage. Typically, students and even teachers will get an empty list in response, only account admins can view the accounts that they are in.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Account>> GetAll()
        {
            var request = new ApiRequest(_config.AccountsEndpointUri);

            var response = await ToResponseAsync<IEnumerable<Account>>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Retrieve information on a single account
        /// </summary>
        /// <returns></returns>
        public async Task<Account> Get(string id)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            var request = new ApiRequest(_config.AccountsEndpointUri, id);

            var response = await ToResponseAsync<Account>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Retrieve the list of courses in this account
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Course>> GetCourses(string id)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            var request = new ApiRequest(_config.AccountsEndpointUri, id + "/courses");

            var response = await ToResponseAsync<IEnumerable<Course>>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

    }
}