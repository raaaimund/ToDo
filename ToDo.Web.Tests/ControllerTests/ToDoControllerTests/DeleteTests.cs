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
    public class DeleteTests : BaseToDoControllerTests
    {
        private static readonly ToDoItem FirstItem = new ToDoItem() { Id = Guid.NewGuid(), Name = "First Item" };
        private static readonly ToDoItem SecondItem = new ToDoItem() { Id = Guid.NewGuid(), Name = "Second Item" };

        public DeleteTests() : base(new List<ToDoItem>() { FirstItem, SecondItem })
        {
        }

        [Fact]
        public async Task DeleteGetWithInvalidIdShouldReturnNotFound()
        {
            var result = await ControllerUnderTest.Delete(Guid.Empty);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteGetShouldCallGetAsyncOnce()
        {
            await ControllerUnderTest.Delete(Guid.Empty);

            MockService.Verify(mock => mock.GetAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task DeleteGetViewModelShouldBeOfTypeDeleteViewModel()
        {
            MockService.Setup(svc => svc.GetAsync(FirstItem.Id)).ReturnsAsync(FirstItem);

            var result = await ControllerUnderTest.Delete(FirstItem.Id);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<DeleteViewModel>(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task DeleteGetViewModelShouldHaveCorrectProperties()
        {
            MockService.Setup(svc => svc.GetAsync(FirstItem.Id)).ReturnsAsync(FirstItem);

            var result = await ControllerUnderTest.Delete(FirstItem.Id);

            var viewResult = Assert.IsType<ViewResult>(result);
            var viewModel = Assert.IsAssignableFrom<DeleteViewModel>(viewResult.ViewData.Model);
            Assert.Equal(FirstItem.Id, viewModel.Id);
            Assert.Equal(FirstItem.Name, viewModel.Name);
        }

        [Fact]
        public async Task DeletePostShouldReturnRedirectToActionIndex()
        {
            var model = new DeleteViewModel();

            var result = await ControllerUnderTest.Delete(model);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(ToDoController.Index), redirectResult.ActionName);
        }

        [Fact]
        public async Task DeletePostShouldCallDeleteItemAsyncOnceIfModelIsValid()
        {
            var model = new DeleteViewModel() { Name = nameof(DeletePostShouldCallDeleteItemAsyncOnceIfModelIsValid) };

            await ControllerUnderTest.Delete(model);

            MockService.Verify(mock => mock.DeleteItemAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task DeletePostShouldCallDeleteItemAsyncWithCorrectParameter()
        {
            var item = new ToDoItem() { Id = FirstItem.Id, Name = nameof(DeletePostShouldCallDeleteItemAsyncWithCorrectParameter) };
            var model = new DeleteViewModel() { Id = item.Id, Name = item.Name };

            await ControllerUnderTest.Delete(model);

            MockService.Verify(mock => mock.DeleteItemAsync(It.Is<Guid>(id => id.Equals(item.Id))), Times.Once);
        }
    }
}
