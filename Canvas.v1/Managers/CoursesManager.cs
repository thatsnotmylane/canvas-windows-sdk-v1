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
            return await GetReponseAsync<Course>(request);
        }

        /// <summary>
        /// Create a new course for the specified account.
        /// </summary>
        /// <param name="courseRequest">The details of the course to create</param>
        /// <returns></returns>
        public async Task<Course> CreateAsync(CourseRequest courseRequest)
        {
            courseRequest.ThrowIfNull("courseRequest")
                .AccountId.ThrowIfUnassigned("courseRequest.AccountId");
            courseRequest.Course.ThrowIfNull("courseRequest.Course");

            ApiRequest request = new ApiRequest(_config.CoursesEndpointUri)
                .Method(RequestMethod.Post)
                .Payload(_converter.Serialize(courseRequest));

            return await GetReponseAsync<Course>(request);
        }
    }
}
