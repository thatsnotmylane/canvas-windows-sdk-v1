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
        /// Returns the list of active courses for the current user.
        /// </summary>
        public async Task<Course> GetAll()
        {
            ApiRequest request = new ApiRequest(_config.CoursesEndpointUri);

            IApiResponse<Course> response = await ToResponseAsync<Course>(request).ConfigureAwait(false);

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

            ApiRequest request = new ApiRequest(_config.CoursesEndpointUri)
                .Method(RequestMethod.Post)
                .Param(ParamFields, fields)
                .Payload(_converter.Serialize<CourseRequest>(courseRequest));

            IApiResponse<Course> response = await ToResponseAsync<Course>(request).ConfigureAwait(false);


            return response.ResponseObject;
        }
    }
}
