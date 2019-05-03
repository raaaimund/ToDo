using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
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
            this._toDoItemService = toDoItemService;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _toDoItemService.GetItemsAsync();
            return View(items);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            await _toDoItemService.AddItemAsync(new ToDoItem() { Name = model.Name });
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var item = await _toDoItemService.GetAsync(id);

            if (item == null)
                return NotFound();

            var model = new UpdateViewModel() { Id = item.Id, Name = item.Name };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            await _toDoItemService.UpdateItemAsync(new ToDoItem() { Id = model.Id, Name = model.Name });
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await _toDoItemService.GetAsync(id);

            if (item == null)
                return NotFound();

            var model = new DeleteViewModel() { Id = item.Id, Name = item.Name };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteViewModel model)
        {
            await _toDoItemService.DeleteItemAsync(model.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}