using System;
using System.Linq;
using System.Threading.Tasks;
using Canvas.v1.Managers;
using Canvas.v1.Models;
using Canvas.v1.Models.Request;
using Canvas.v1.Wrappers;
using Canvas.v1.Wrappers.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Canvas.v1.Test
{
    [TestClass]
    public class CourseManagerTests : ResourceManagerTest
    {
        protected CoursesManager _coursesManager;

        public CourseManagerTests()
        {
            _coursesManager = new CoursesManager(_config.Object, _service, _converter, _authRepository);
        }

        [TestMethod]
        public async Task GetFolderItems_ValidResponse_ValidFolder()
        {
//            _handler.Setup(h => h.ExecuteAsync<BoxCollection<Item>>(It.IsAny<IApiRequest>()))
//                .Returns(() => Task.FromResult<IApiResponse<BoxCollection<Item>>>(new ApiResponse<BoxCollection<Item>>()
//                    {
//                        Status = ResponseStatus.Success,
//                        ContentString = "{\"total_count\":24,\"entries\":[{\"type\":\"folder\",\"id\":\"192429928\",\"sequence_id\":\"1\",\"etag\":\"1\",\"name\":\"Stephen Curry Three Pointers\"},{\"type\":\"file\",\"id\":\"818853862\",\"sequence_id\":\"0\",\"etag\":\"0\",\"name\":\"Warriors.jpg\"}],\"offset\":0,\"limit\":2,\"order\":[{\"by\":\"type\",\"direction\":\"ASC\"},{\"by\":\"name\",\"direction\":\"ASC\"}]}"
//                    }));
//
//            BoxCollection<Item> items = await _coursesManager.CreateAsync("0", 2);
//
//            Assert.AreEqual(items.TotalCount, 24);
//            Assert.AreEqual(items.Entries.Count, 2);
//            Assert.AreEqual(items.Entries[0].Id, "192429928");
        }
    }
}
