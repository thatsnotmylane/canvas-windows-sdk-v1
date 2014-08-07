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
    /// <summary>
    /// The manager that represents all of the Search endpoints
    /// </summary>
    public class BoxSearchManager : BoxResourceManager
    {
        public BoxSearchManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth)
            : base(config, service, converter, auth) { }


        /// <summary>
        /// Returns a collection of search results that match the keyword, if there are are no matching search results
        /// an empty collection will be returned
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public async Task<BoxCollection<BoxItem>> SearchAsync(string keyword, int limit, int offset = 0, List<string> fields = null)
        {
            keyword.ThrowIfNullOrWhiteSpace("keyword");

            BoxRequest request = new BoxRequest(_config.SearchEndpointUri)
                .Param("query", keyword)
                .Param("limit", limit.ToString())
                .Param("offset", offset.ToString())
                .Param(ParamFields, fields);

            IBoxResponse<BoxCollection<BoxItem>> response = await ToResponseAsync<BoxCollection<BoxItem>>(request).ConfigureAwait(false);
                    
            return response.ResponseObject;
        }

    }
}
