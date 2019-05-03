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
    public class UpdateTests : BaseToDoControllerTests
    {
        private static readonly ToDoItem FirstItem = new ToDoItem() { Id = Guid.NewGuid(), Name = "First Item" };
        private static readonly ToDoItem SecondItem = new ToDoItem() { Id = Guid.NewGuid(), Name = "Second Item" };

        public UpdateTests() : base(new List<ToDoItem>() { FirstItem, SecondItem })
        {
        }

        [Fact]
        public async Task UpdateGetWithInvalidIdShouldReturnNotFound()
        {
            var result = await ControllerUnderTest.Update(Guid.Empty);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateGetShouldCallGetAsyncOnce()
        {
            var result = await ControllerUnderTest.Update(Guid.Empty);

            MockService.Verify(mock => mock.GetAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task UpdateGetViewModelShouldBeOfTypeUpdateViewModel()
        {
            MockService.Setup(svc => svc.GetAsync(FirstItem.Id)).ReturnsAsync(FirstItem);

            var result = await ControllerUnderTest.Update(FirstItem.Id);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<UpdateViewModel>(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task UpdateGetViewModelShouldHaveCorrectProperties()
        {
            MockService.Setup(svc => svc.GetAsync(FirstItem.Id)).ReturnsAsync(FirstItem);

            var result = await ControllerUnderTest.Update(FirstItem.Id);

            var viewResult = Assert.IsType<ViewResult>(result);
            var viewModel = Assert.IsAssignableFrom<UpdateViewModel>(viewResult.ViewData.Model);
            Assert.Equal(FirstItem.Id, viewModel.Id);
            Assert.Equal(FirstItem.Name, viewModel.Name);
        }

        [Fact]
        public async Task UpdatePostShouldReturnUpdateViewModelIfModelIsInvalid()
        {
            var model = new UpdateViewModel();

            ControllerUnderTest.ModelState.AddModelError("error", "testerror");
            var result = await ControllerUnderTest.Update(model);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<UpdateViewModel>(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task UpdatePostShouldReturnRedirectToActionIndexIfModelIsValid()
        {
            var model = new UpdateViewModel();

            var result = await ControllerUnderTest.Update(model);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(ToDoController.Index), redirectResult.ActionName);
        }

        [Fact]
        public async Task UpdatePostShouldCallUpdateItemAsyncOnceIfModelIsValid()
        {
            var model = new UpdateViewModel() { Name = nameof(UpdatePostShouldCallUpdateItemAsyncOnceIfModelIsValid) };

            await ControllerUnderTest.Update(model);

            MockService.Verify(mock => mock.UpdateItemAsync(It.IsAny<ToDoItem>()), Times.Once);
        }

        [Fact]
        public async Task UpdatePostShouldCallUpdateItemAsyncWithCorrectParameterIfModelIsValid()
        {
            var item = new ToDoItem() { Id = FirstItem.Id, Name = nameof(UpdatePostShouldCallUpdateItemAsyncWithCorrectParameterIfModelIsValid) };
            var model = new UpdateViewModel() { Id = item.Id, Name = item.Name };

            await ControllerUnderTest.Update(model);

            MockService.Verify(mock => mock.UpdateItemAsync(It.Is<ToDoItem>(i => i.Name.Equals(item.Name) && i.Id.Equals(item.Id))), Times.Once);
        }
    }
}
