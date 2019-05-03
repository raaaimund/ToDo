using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ToDo.Dto;
using ToDo.Services;
using ToDo.Web.Models.ToDo;

namespace ToDo.Web.Controllers
{
    public class ToDoController : Controller
    {
        private readonly IToDoItemService _toDoItemService;

        public ToDoController(IToDoItemService toDoItemService)
        {
            this._toDoItemService = toDoItemService ?? throw new ArgumentNullException(nameof(toDoItemService));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ToDoItem>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Index()
        {
            var items = await _toDoItemService.GetItemsAsync();
            return View(items);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ProducesResponseType(typeof(CreateViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            await _toDoItemService.AddItemAsync(new ToDoItem() {Name = model.Name});
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UpdateViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(Guid id)
        {
            var item = await _toDoItemService.GetAsync(id);

            if (item == null)
                return NotFound();

            var model = new UpdateViewModel() {Id = item.Id, Name = item.Name};
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ProducesResponseType(typeof(UpdateViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(UpdateViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            await _toDoItemService.UpdateItemAsync(new ToDoItem() {Id = model.Id, Name = model.Name});
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(DeleteViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await _toDoItemService.GetAsync(id);

            if (item == null)
                return NotFound();

            var model = new DeleteViewModel() {Id = item.Id, Name = item.Name};
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ProducesResponseType(StatusCodes.Status302Found)]
        public async Task<IActionResult> Delete(DeleteViewModel model)
        {
            await _toDoItemService.DeleteItemAsync(model.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}