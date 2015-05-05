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
        public async Task<IEnumerable<Course>> GetAll()
        {
            ApiRequest request = new ApiRequest(_config.CoursesEndpointUri);
            return await GetReponseAsync<IEnumerable<Course>>(request).ConfigureAwait(false);
        }

        /// <summary>
        /// Fetches the list of users in this course, and optionally the user's enrollments in the course.
        /// </summary>
        /// <param name="courseId">The ID of the course to fetch</param>
        /// <param name="page">Optional. The results page to fetch</param>
        /// <param name="itemsPerPage">Optional. The number of results per page to fetch</param>
        /// <param name="enrollmentType">Optional. When set, only return users where the user is enrolled as this type. This argument is ignored if enrollmentRole is given.</param>
        /// <param name="enrollmentRole">Optional. When set, only return users enrolled with the specified course-level role.</param>
        /// <param name="searchTerm">Optional. The partial name or full ID of the users to match and return in the results list.</param>
        /// <param name="include">Optional. Additional information to be returned for each user.</param>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetUsers(long courseId, int page = 1, int itemsPerPage = 10, string searchTerm = null, UserEnrollmentType? enrollmentType = null, UserEnrollmentType? enrollmentRole = null, UserInclude? include = null)
        {
            courseId.ThrowIfUnassigned("courseId");
            searchTerm.ThrowIfShorterThanLength(3, "searchTerm");

            ApiRequest request = new PagedApiRequest(_config.CoursesEndpointUri, courseId + "/users", page, itemsPerPage)
                .Param("search_term", searchTerm)
                .Param("enrollment_type", enrollmentType)
                .Param("enrollment_role", enrollmentRole)
                .Param("include", include);

            return await GetReponseAsync<IEnumerable<User>>(request).ConfigureAwait(false);
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

            return await GetReponseAsync<Course>(request).ConfigureAwait(false);
        }
    }
}
