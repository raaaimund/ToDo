using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ToDo.Data;
using ToDo.Dto;

namespace ToDo.Services
{
    public class ToDoItemService : IToDoItemService
    {
        private readonly ToDoDbContext _dbContext;
        private readonly ILogger<ToDoItemService> _logger;

        public ToDoItemService(ToDoDbContext dbContext)
        {
            this._dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public ToDoItemService(ToDoDbContext dbContext, ILogger<ToDoItemService> logger)
        {
            this._dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ToDoItem> GetAsync(Guid id)
        {
            return await _dbContext.ToDoItem.FindAsync(id);
        }

        public async Task<IEnumerable<ToDoItem>> GetItemsAsync()
        {
            return await _dbContext.ToDoItem.AsNoTracking().Select(s => s).ToListAsync();
        }

        public async Task AddItemAsync(ToDoItem item)
        {
            await _dbContext.ToDoItem.AddAsync(item);
            await _dbContext.SaveChangesAsync();
            _logger?.LogInformation($"{item.Name} created.");
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var item = await _dbContext.ToDoItem.FindAsync(id);
            _dbContext.ToDoItem.Remove(item);
            await _dbContext.SaveChangesAsync();
            _logger?.LogInformation($"Item {item.Name} deleted.");
        }

        public async Task UpdateItemAsync(ToDoItem item)
        {
            var itemInDb = await _dbContext.ToDoItem.FindAsync(item.Id);
            itemInDb.Name = item.Name;
            await _dbContext.SaveChangesAsync();
            _logger?.LogInformation($"{item.Name} updated.");
        }
    }
}