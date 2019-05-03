using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Dto;
using ToDo.Web.Controllers;
using ToDo.Web.Models.ToDo;
using Xunit;

namespace ToDo.Web.Tests.ControllerTests.ToDoControllerTests
{
    public class CreateTests : BaseToDoControllerTests
    {
        private static readonly ToDoItem FirstItem = new ToDoItem() { Id = Guid.NewGuid(), Name = "First Item" };
        private static readonly ToDoItem SecondItem = new ToDoItem() { Id = Guid.NewGuid(), Name = "Second Item" };

        public CreateTests() : base(new List<ToDoItem>() { FirstItem, SecondItem })
        {
        }

        [Fact]
        public void CreateGetShouldHaveNoViewModel()
        {
            var result = Controller.Create();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task CreatePostShouldReturnCreateViewModelIfModelIsInvalid()
        {
            var model = new CreateViewModel();

            Controller.ModelState.AddModelError("error", "testerror");
            var result = await Controller.Create(model);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<CreateViewModel>(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task CreatePostShouldReturnRedirectToActionIndexIfModelIsValid()
        {
            var model = new CreateViewModel();

            var result = await Controller.Create(model);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(ToDoController.Index), redirectResult.ActionName);
        }

        [Fact]
        public async Task CreatePostShouldCallAddItemAsyncOnceIfModelIsValid()
        {
            var model = new CreateViewModel() { Name = nameof(CreatePostShouldCallAddItemAsyncOnceIfModelIsValid) };

            var result = await Controller.Create(model);

            MockService.Verify(mock => mock.AddItemAsync(It.IsAny<ToDoItem>()), Times.Once);
        }

        [Fact]
        public async Task CreatePostShouldCallAddItemAsyncWithCorrectParameterIfModelIsValid()
        {
            var item = new ToDoItem() { Name = nameof(CreatePostShouldCallAddItemAsyncWithCorrectParameterIfModelIsValid) };
            var model = new CreateViewModel() { Name = item.Name };

            var result = await Controller.Create(model);

            MockService.Verify(mock => mock.AddItemAsync(It.Is<ToDoItem>(i => i.Name.Equals(item.Name))), Times.Once);
        }
    }
}
