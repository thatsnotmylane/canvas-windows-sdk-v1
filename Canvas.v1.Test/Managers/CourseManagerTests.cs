using Canvas.v1.Managers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Canvas.v1.Test.Managers
{
    [TestClass]
    public class CourseManagerTests : ResourceManagerTest
    {
        protected CoursesManager _coursesManager;

        public CourseManagerTests()
        {
            _coursesManager = new CoursesManager(_config.Object, _service, _converter, _authRepository);
        }
    }
}
