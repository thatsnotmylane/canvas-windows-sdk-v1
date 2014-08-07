using System.Collections.Generic;
using System.Threading.Tasks;
using Canvas.v1.Auth;
using Canvas.v1.Config;
using Canvas.v1.Converter;
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
        public async Task<IEnumerable<string>> GetAll()
        {
            ApiRequest request = new ApiRequest(_config.AccountsEndpointUri);

            IApiResponse<IEnumerable<string>> response = await ToResponseAsync<IEnumerable<string>>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }
    }
}