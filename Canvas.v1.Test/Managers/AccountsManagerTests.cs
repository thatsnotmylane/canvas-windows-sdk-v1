using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Canvas.v1.Managers;
using Canvas.v1.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Canvas.v1.Test.Managers
{
    [TestClass]
    public class AccountsManagerTests : ResourceManagerTest
    {
        private readonly AccountsManager _accountsManager;

        public AccountsManagerTests()
        {
            _accountsManager = new AccountsManager(_config.Object, _service, _converter, _authRepository);
        }

        [TestMethod]
        public async Task GetAllAccounts()
        {
            const string content = @"[{""id"":1234,""name"":""Indiana University"",""parent_account_id"":null,""root_account_id"":null,""workflow_state"":""active"",""default_storage_quota_mb"":500,""default_user_storage_quota_mb"":100,""default_group_storage_quota_mb"":300,""default_time_zone"":""America/New_York""}]";

            ArrangeSuccessfulResponse<IEnumerable<Account>>(content);

            var accounts = await _accountsManager.GetAll();

            Assert.AreEqual(accounts.Count(), 1);
            Account account = accounts.Single();

            Assert.AreEqual(account.Id, "1234");
            Assert.AreEqual(account.Name, "Indiana University");
            Assert.AreEqual(account.ParentAccountId, null);
            Assert.AreEqual(account.RootAccountId, null);
            Assert.AreEqual(account.WorkflowState, AccountWorkflowState.Active);
            Assert.AreEqual(account.DefaultStorageQuotaMB, 500);
            Assert.AreEqual(account.DefaultUserStorageQuotaMB, 100);
            Assert.AreEqual(account.DefaultGroupStorageQuotaMB, 300);
            Assert.AreEqual(account.DefaultTimeZone, "America/New_York");
        }

        [TestMethod]
        public async Task GetAllCourses()
        {
            const string content = @"[{""account_id"":1234,""root_account_id"":8765,""course_code"":""VID DEMO 101"",""default_view"":""feed"",""id"":5678,""name"":""Video Demo"",""start_at"":""2014-03-27T00:00:00Z"",""end_at"":""2014-04-27T00:00:00Z"",""public_syllabus"":true,""storage_quota_mb"":500,""apply_assignment_group_weights"":true,""calendar"":{""ics"":""https://iu.test.instructure.com/feeds/calendars/course_abcd.ics""},""sis_course_id"":7890,""integration_id"":4321,""hide_final_grades"":true,""workflow_state"":""available""}]";

            ArrangeSuccessfulResponse<IEnumerable<Course>>(content);

            var courses = await _accountsManager.GetCourses("id");

            Assert.AreEqual(courses.Count(), 1);
            var course = courses.Single();

            Assert.AreEqual(course.Id, "5678");
            Assert.AreEqual(course.AccountId, "1234");
            Assert.AreEqual(course.RootAccountId, "8765");
            Assert.AreEqual(course.IntegrationId, "4321");
            Assert.AreEqual(course.SisCourseId, "7890");
            Assert.AreEqual(course.Name, "Video Demo");
            Assert.AreEqual(course.CourseCode, "VID DEMO 101");
            Assert.AreEqual(course.WorkflowState, CourseWorkflowState.Available);
            Assert.AreEqual(course.ApplyAssignmentGroupWeights, true);
            Assert.AreEqual(course.HideFinalGrades, true);
            Assert.AreEqual(course.PublicSyllabus, true);
            Assert.AreEqual(course.StorageQuotaMB, 500);
            Assert.AreEqual(course.StartAt, new DateTime(2014, 3, 27));
            Assert.AreEqual(course.EndAt, new DateTime(2014, 4, 27));
            Assert.IsNotNull(course.Calendar);
            Assert.AreEqual(course.Calendar.Ics, "https://iu.test.instructure.com/feeds/calendars/course_abcd.ics");
        }
    }
}