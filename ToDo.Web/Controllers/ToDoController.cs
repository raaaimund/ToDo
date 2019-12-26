using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ToDo.Application.ToDoItem.Commands.CreateToDoItem;
using ToDo.Application.ToDoItem.Commands.DeleteToDoItem;
using ToDo.Application.ToDoItem.Commands.UpdateToDoItem;
using ToDo.Application.ToDoItem.Queries;

namespace ToDo.Web.Controllers
{
    public class ToDoController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Index() => 
            View(await Mediator.Send(new GetToDoItemsQuery()));

        [HttpGet]
        public IActionResult Create() => 
            View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateToDoItemCommand model)
        {
            await Mediator.Send(model);
            return RedirectToAction(nameof(Index));
        }
            
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var model = await Mediator.Send(new GetToDoItemQuery() { Id = id });
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateToDoItemCommand model)
        {
            await Mediator.Send(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var model = await Mediator.Send(new GetToDoItemQuery() { Id = id });
            ViewData["Name"] = model.Name;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteToDoItemCommand model)
        {
            await Mediator.Send(model);
            return RedirectToAction(nameof(Index));
        }
    }
}