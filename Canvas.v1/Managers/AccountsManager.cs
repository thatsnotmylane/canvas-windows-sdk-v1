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
using Newtonsoft.Json;

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
            return await GetReponseAsync<IEnumerable<Account>>(request).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve information on a single account
        /// </summary>
        /// <returns></returns>
        public async Task<Account> Get(long id)
        {
            id.ThrowIfUnassigned("accountId");

            var request = new ApiRequest(_config.AccountsEndpointUri, id.ToString());
            return await GetReponseAsync<Account>(request).ConfigureAwait(false);
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
        /// <param name="include">Optional Course properties to include. Use bitwise 'OR' operator to specify multiple items, e.g. CourseInclude.Term | CourseInclude.Sections</param>
        /// <returns></returns>
        /// <remarks>The API parameters 'completed' and 'published' are not included here. Use the 'state' enum flags instead.</remarks>
        public async Task<IEnumerable<Course>> GetCourses(long accountId, int page = 1, int itemsPerPage = 10, CourseWorkflowState? state = null, bool? withEnrollments = null, bool? hideEnrollmentlessCourses = null, IEnumerable<long> byTeachers = null, IEnumerable<long> bySubaccounts = null, string enrollmentTermId = null, string searchTerm = null, CourseInclude? include = null)
        {
            accountId.ThrowIfUnassigned("accountId");
            searchTerm.ThrowIfShorterThanLength(3, "searchTerm");

            var request = new PagedApiRequest(_config.AccountsEndpointUri, accountId + "/courses", page, itemsPerPage)
                .Param("with_enrollments", withEnrollments)
                .Param("hide_enrollmentless_courses", hideEnrollmentlessCourses)
                .Param("state", state)
                .Param("by_teachers", byTeachers)
                .Param("by_subaccounts", bySubaccounts)
                .Param("enrollment_term_id", enrollmentTermId)
                .Param("search_term", searchTerm)
                .Param("include", include);

            return await GetReponseAsync<IEnumerable<Course>>(request).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve the list of users associated with this account.
        /// </summary>
        /// <param name="accountId">The ID of the account to query.</param>
        /// <param name="searchTerm">Optional. The partial name or full ID of the users to match and return in the results list. Must be at least 3 characters.</param>
        /// <param name="page">The results page to fetch</param>
        /// <param name="itemsPerPage">The number of results per page to fetch</param>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetUsers(long accountId, int page = 1, int itemsPerPage = 10, string searchTerm = null)
        {
            accountId.ThrowIfUnassigned("accountId");
            searchTerm.ThrowIfShorterThanLength(3, "searchTerm");

            ApiRequest request = new PagedApiRequest(_config.AccountsEndpointUri, accountId + "/users", page, itemsPerPage)
                .Param("search_term", searchTerm);

            return await GetReponseAsync<IEnumerable<User>>(request).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve enrollment terms for this account
        /// </summary>
        /// <param name="accountId">The ID of the account to query</param>
        /// <param name="workflowState">(Optional) The workflow state(s) of the enrollment terms to fetch. Defaults to 'Active'</param>
        /// <returns></returns>
        public async Task<IEnumerable<EnrollmentTerm>> GetEnrollmentTerms(long accountId, EnrollmentTermWorkflowState? workflowState = EnrollmentTermWorkflowState.Active)
        {
            accountId.ThrowIfUnassigned("accountId");
            
            var request = new ApiRequest(_config.AccountsEndpointUri, accountId+"/terms")
                .Param("workflow_state", workflowState);

            var collection = await GetReponseAsync<EnrollmentTermCollection>(request).ConfigureAwait(false);
            return collection.EnrollmentTerms;
        }

        internal class EnrollmentTermCollection
        {
            [JsonProperty(PropertyName = "enrollment_terms")]
            public IEnumerable<EnrollmentTerm> EnrollmentTerms { get; set; }
        }

        /// <summary>
        /// Create an enrollment term
        /// </summary>
        /// <param name="accountId">The ID of the account</param>
        /// <param name="term">The term to create</param>
        /// <returns></returns>
        public async Task<EnrollmentTerm> CreateEnrollmentTerm(long accountId, EnrollmentTermRequest term)
        {
            accountId.ThrowIfUnassigned("accountId");
            term.ThrowIfNull("term");

            var request = new ApiRequest(_config.AccountsEndpointUri, accountId + "/terms")
                .Method(RequestMethod.Post);
            request = PopulateTerm(request, term);

            return await GetReponseAsync<EnrollmentTerm>(request).ConfigureAwait(false);
        }

        /// <summary>
        /// Update an enrollment term
        /// </summary>
        /// <param name="accountId">The ID of the account</param>
        /// <param name="term">The term to update</param>
        /// <returns></returns>
        public async Task<EnrollmentTerm> UpdateEnrollmentTerm(long accountId, EnrollmentTerm term)
        {
            accountId.ThrowIfUnassigned("accountId");
            term.ThrowIfNull("term");

            var request = new ApiRequest(_config.AccountsEndpointUri, accountId + "/terms/" + term.Id)
                .Method(RequestMethod.Put);
            request = PopulateTerm(request, term);

            return await GetReponseAsync<EnrollmentTerm>(request).ConfigureAwait(false);
        }

        /// <summary>
        /// Delete an enrollment term
        /// </summary>
        /// <param name="accountId">The ID of the account</param>
        /// <param name="enrollmentTermId">The ID of the enrollment term</param>
        /// <returns></returns>
        public async Task<EnrollmentTerm> DeleteEnrollmentTerm(long accountId, long enrollmentTermId)
        {
            accountId.ThrowIfUnassigned("accountId");
            accountId.ThrowIfUnassigned("enrollmentTermId");

            var request = new ApiRequest(_config.AccountsEndpointUri, accountId + "/terms/" + enrollmentTermId)
                .Method(RequestMethod.Delete);

            return await GetReponseAsync<EnrollmentTerm>(request).ConfigureAwait(false);
        }

        private static ApiRequest PopulateTerm(ApiRequest request, EnrollmentTermRequest term)
        {
            return request
                .Param("enrollment_term[name]", term.Name)
                .Param("enrollment_term[start_at]", term.StartAt)
                .Param("enrollment_term[end_at]", term.EndAt)
                .Param("enrollment_term[sis_term_id]", term.SisTermId);
        }
    }
}