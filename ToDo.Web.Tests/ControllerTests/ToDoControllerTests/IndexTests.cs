using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Dto;
using Xunit;

namespace ToDo.Web.Tests.ControllerTests.ToDoControllerTests
{
    public class IndexTests : BaseToDoControllerTests
    {
        private static readonly ToDoItem FirstItem = new ToDoItem() { Id = Guid.NewGuid(), Name = "First Item" };
        private static readonly ToDoItem SecondItem = new ToDoItem() { Id = Guid.NewGuid(), Name = "Second Item" };

        public IndexTests() : base(new List<ToDoItem>() { FirstItem, SecondItem })
        {
        }

        [Fact]
        public async Task IndexGetViewModelShouldBeOfTypeIEnumerableToDoItem()
        {
            var result = await ControllerUnderTest.Index();

            var viewResult = Assert.IsType<ViewResult>(result);

            Assert.IsAssignableFrom<IEnumerable<ToDoItem>>(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task IndexGetShouldReturnListOfToDoItems()
        {
            var result = await ControllerUnderTest.Index();

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<IEnumerable<ToDoItem>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }
    }
}
