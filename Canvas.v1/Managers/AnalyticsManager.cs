using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Canvas.v1.Auth;
using Canvas.v1.Config;
using Canvas.v1.Converter;
using Canvas.v1.Extensions;
using Canvas.v1.Models.Analytics;
using Canvas.v1.Services;
using Canvas.v1.Wrappers;

namespace Canvas.v1.Managers
{
    public class AnalyticsManager : ResourceManager
    {
        public AnalyticsManager(ICanvasConfig config, IRequestService service, IJsonConverter converter, IAuthRepository auth) : base(config, service, converter, auth)
        {
        }

        /// <summary>
        /// Returns a list of assignments for the course sorted by due date. For each assignment returns basic assignment information, the grade breakdown, and a breakdown of on-time/late status of homework submissions.
        /// </summary>
        /// <param name="courseId">The ID of the course.</param>
        /// <returns></returns>
        public async Task<IEnumerable<CourseAssignment>> GetCourseAssignments(string courseId)
        {
            courseId.ThrowIfNull("courseId");

            var request = new ApiRequest(_config.CoursesEndpointUri, courseId + "/analytics/assignments");
            return await GetReponseAsync<IEnumerable<CourseAssignment>>(request).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns page view hits and participation numbers grouped by day through the entire history of the course.
        /// </summary>
        /// <param name="courseId">The ID of the course.</param>
        /// <returns></returns>
        public async Task<IEnumerable<CourseParticipation>> GetCourseParticipations(string courseId)
        {
            courseId.ThrowIfNull("courseId");

            var request = new ApiRequest(_config.CoursesEndpointUri, courseId + "/analytics/activity");
            return await GetReponseAsync<IEnumerable<CourseParticipation>>(request).ConfigureAwait(false);
        }


        /// <summary>
        /// Returns a summary of per-user access information for all students in a course. This includes total page views, total participations, and a breakdown of on-time/late status for all homework submissions in the course. The data is returned as a list in lexical order on the student name.
        /// Each student's summary also includes the maximum number of page views and participations by any student in the course, which may be useful for some visualizations (since determining maximums client side can be tricky with pagination).
        /// </summary>
        /// <param name="courseId">The ID of the course.</param>
        /// <returns></returns>
        public async Task<IEnumerable<StudentSummary>> GetStudentSummaries(string courseId)
        {
            courseId.ThrowIfNull("courseId");

            var request = new ApiRequest(_config.CoursesEndpointUri, courseId + "/analytics/student_summaries");
            return await GetReponseAsync<IEnumerable<StudentSummary>>(request).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns a list of assignments for the course sorted by due date. For each assignment returns basic assignment information, the grade breakdown (including the student's actual grade), and the basic submission information for the student's submission if it exists.
        /// </summary>
        /// <param name="courseId">The ID of the course.</param>
        /// <param name="studentId">The ID of the student.</param>
        /// <returns></returns>
        public async Task<IEnumerable<StudentAssignment>> GetStudentAssignments(string courseId, string studentId)
        {
            courseId.ThrowIfNull("courseId");
            studentId.ThrowIfNull("studentId");

            var request = new ApiRequest(_config.CoursesEndpointUri, courseId + "/analytics/users/" + studentId + "/assignments");
            return await GetReponseAsync<IEnumerable<StudentAssignment>>(request).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns page view hits and participation numbers grouped by day through the entire history of the course.
        /// </summary>
        /// <param name="courseId">The ID of the course.</param>
        /// <param name="studentId">The ID of the student.</param>
        /// <returns></returns>
        public async Task<StudentParticipation> GetStudentParticipations(string courseId, string studentId)
        {
            courseId.ThrowIfNull("courseId");
            studentId.ThrowIfNull("studentId");

            var request = new ApiRequest(_config.CoursesEndpointUri, courseId + "/analytics/users/" + studentId + "/activity");
            return await GetReponseAsync<StudentParticipation>(request).ConfigureAwait(false);
        }

    }
}
