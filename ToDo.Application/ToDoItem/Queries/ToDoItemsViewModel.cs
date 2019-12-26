using System.Collections.Generic;

namespace ToDo.Application.ToDoItem.Queries
{
    public class ToDoItemsViewModel
    {
        public IList<ToDoItemViewModel> ToDoItems { get; set; } = new List<ToDoItemViewModel>();
    }
}
