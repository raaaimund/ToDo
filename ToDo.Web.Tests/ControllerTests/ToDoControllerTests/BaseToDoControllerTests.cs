using Moq;
using System.Collections.Generic;
using ToDo.Dto;
using ToDo.Services;
using ToDo.Web.Controllers;

namespace ToDo.Web.Tests.ControllerTests.ToDoControllerTests
{
    public abstract class BaseToDoControllerTests
    {
        protected readonly List<ToDoItem> Items;
        protected readonly Mock<IToDoItemService> MockService;
        protected readonly ToDoController ControllerUnderTest;

        protected BaseToDoControllerTests(List<ToDoItem> items)
        {
            Items = items;
            MockService = new Mock<IToDoItemService>();
            MockService.Setup(svc => svc.GetItemsAsync())
                .ReturnsAsync(Items);
            ControllerUnderTest = new ToDoController(MockService.Object);
        }
    }
}