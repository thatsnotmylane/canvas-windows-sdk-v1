using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Canvas.v1.Auth;
using Canvas.v1.Config;
using Canvas.v1.Converter;
using Canvas.v1.Extensions;
using Canvas.v1.Models;
using Canvas.v1.Models.Request;
using Canvas.v1.Services;
using Canvas.v1.Wrappers;
using Canvas.v1.Wrappers.Contracts;

namespace Canvas.v1.Managers
{
    public class CoursesManager : ResourceManager
    {
        public CoursesManager(ICanvasConfig config, IRequestService service, IJsonConverter converter, IAuthRepository auth)
            : base(config, service, converter, auth) { }

        /// <summary>
        /// Retrieves the files and/or folders contained in the provided folder id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        [Obsolete("This endpoint is not officially supported by the API and is not guaranteed to be available in the next version. Please use GetFolderItemsAsync")]
        public async Task<Course> GetItemsAsync(string id, int limit, int offset = 0, List<string> fields = null)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.CoursesEndpointUri, id)
                .Param("limit", limit.ToString())
                .Param("offset", offset.ToString())
                .Param(ParamFields, fields);

            IBoxResponse<Course> response = await ToResponseAsync<Course>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Used to create a new empty folder. The new folder will be created inside of the specified parent folder
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public async Task<Course> CreateAsync(CourseRequest courseRequest, List<string> fields = null)
        {
            courseRequest.ThrowIfNull("courseRequest")
                .AccountId.ThrowIfUnassigned("courseRequest.AccountId");
            courseRequest.Course.ThrowIfNull("courseRequest.Course");

            BoxRequest request = new BoxRequest(_config.CoursesEndpointUri)
                .Method(RequestMethod.Post)
                .Param(ParamFields, fields)
                .Payload(_converter.Serialize<CourseRequest>(courseRequest));

            IBoxResponse<Course> response = await ToResponseAsync<Course>(request).ConfigureAwait(false);


            return response.ResponseObject;
        }
    }
}
