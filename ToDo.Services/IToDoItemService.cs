using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Dto;

namespace ToDo.Services
{
    public interface IToDoItemService
    {
        Task<ToDoItem> GetAsync(Guid id);
        Task<IEnumerable<ToDoItem>> GetItemsAsync();
        Task AddItemAsync(ToDoItem item);
        Task UpdateItemAsync(ToDoItem item);
        Task DeleteItemAsync(Guid id);
    }
}
