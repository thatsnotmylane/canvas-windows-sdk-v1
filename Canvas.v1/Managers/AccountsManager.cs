using System;
using System.Collections.Generic;
using System.Linq;
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
            return await GetReponseAsync<IEnumerable<Account>>(request);
        }

        /// <summary>
        /// Retrieve information on a single account
        /// </summary>
        /// <returns></returns>
        public async Task<Account> Get(string id)
        {
            id.ThrowIfNullOrWhiteSpace("accountId");

            var request = new ApiRequest(_config.AccountsEndpointUri, id);
            return await GetReponseAsync<Account>(request);
        }

        /// <summary>
        /// Retrieve the list of courses associated with this account
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="state">Optional. If set, only return courses that are in the given state(s). By default, all states but "deleted" are returned.</param>
        /// <param name="withEnrollments">Optional. If true, include only courses with at least one enrollment. If false, include only courses with no enrollments. If not present, do not filter on course enrollment status.</param>
        /// <param name="hideEnrollmentlessCourses">Optional. If present, only return courses that have at least one enrollment. Equivalent to 'with_enrollments=true'; retained for compatibility.</param>
        /// <param name="byTeachers">Optional. List of User IDs of teachers; if supplied, include only courses taught by one of the referenced users.</param>
        /// <param name="bySubaccounts">Optional. List of Account IDs; if supplied, include only courses associated with one of the referenced subaccounts.</param>
        /// <param name="enrollmentTermId">Optional. If set, only includes courses from the specified term.</param>
        /// <param name="searchTerm">Optional. The partial course name, code, or full ID to match and return in the results list. Must be at least 3 characters.</param>
        /// <param name="page">The results page to fetch</param>
        /// <param name="itemsPerPage">The number of results per page to fetch</param>
        /// <returns></returns>
        /// <remarks>The API parameters 'completed' and 'published' are not included here. Use the 'state' enum flags instead.</remarks>
        public async Task<IEnumerable<Course>> GetCourses(string accountId, int page = 1, int itemsPerPage = 10, CourseWorkflowState? state = null, bool? withEnrollments = null, bool? hideEnrollmentlessCourses = null, IEnumerable<long> byTeachers = null, IEnumerable<long> bySubaccounts = null, string enrollmentTermId = null, string searchTerm = null)
        {
            accountId.ThrowIfNullOrWhiteSpace("accountId");
            searchTerm.ThrowIfShorterThanLength(3, "searchTerm");

            var request = new PagedApiRequest(_config.AccountsEndpointUri, accountId + "/courses", page, itemsPerPage)
                .Param("with_enrollments", withEnrollments)
                .Param("hide_enrollmentless_courses", hideEnrollmentlessCourses)
                .Param("state", state)
                .Param("by_teachers", byTeachers)
                .Param("by_subaccounts", bySubaccounts)
                .Param("enrollment_term_id", enrollmentTermId)
                .Param("search_term", searchTerm);

            return await GetReponseAsync<IEnumerable<Course>>(request);
        }

        /// <summary>
        /// Retrieve the list of users associated with this account.
        /// </summary>
        /// <param name="accountId">The ID of the account to query.</param>
        /// <param name="searchTerm">Optional. The partial name or full ID of the users to match and return in the results list. Must be at least 3 characters.</param>
        /// <param name="page">The results page to fetch</param>
        /// <param name="itemsPerPage">The number of results per page to fetch</param>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetUsers(string accountId, int page = 1, int itemsPerPage = 10, string searchTerm = null)
        {
            accountId.ThrowIfNullOrWhiteSpace("accountId");
            searchTerm.ThrowIfShorterThanLength(3, "searchTerm");

            ApiRequest request = new PagedApiRequest(_config.AccountsEndpointUri, accountId + "/users", page, itemsPerPage)
                .Param("search_term", searchTerm);
                
            return await GetReponseAsync<IEnumerable<User>>(request);
        }
    }
}