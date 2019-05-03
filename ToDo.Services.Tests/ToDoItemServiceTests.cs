using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Dto;
using Xunit;

namespace ToDo.Services.Tests
{
    public class ToDoItemServiceTests : TestWithSqlite
    {
        private readonly ToDoItem _firstItem = new ToDoItem() {Id = Guid.NewGuid(), Name = "First Item"};
        private readonly ToDoItem _secondItem = new ToDoItem() {Id = Guid.NewGuid(), Name = "Second Item"};
        private readonly ToDoItem[] _items;

        public ToDoItemServiceTests()
        {
            _items = new[] {_firstItem, _secondItem};
            Context.ToDoItem.AddRange(_items);
            Context.SaveChanges();
        }

        [Fact]
        public async Task GetAsyncShouldReturnCorrectItem()
        {
            var service = new ToDoItemService(Context);

            var actualItem = await service.GetAsync(_firstItem.Id);

            Assert.Equal(_firstItem.Id, actualItem.Id);
        }

        [Fact]
        public async Task GetItemsAsyncShouldReturnAllItems()
        {
            var service = new ToDoItemService(Context);

            var itemsFromService = await service.GetItemsAsync();

            Assert.Contains(itemsFromService, a => a.Id.Equals(_firstItem.Id));
            Assert.Contains(itemsFromService, a => a.Id.Equals(_secondItem.Id));
            Assert.Equal(_items.Count(), itemsFromService.Count());
        }

        [Fact]
        public async Task AddItemAsyncShouldAddItem()
        {
            var service = new ToDoItemService(Context);
            var thirdItem = new ToDoItem() {Name = "Third Item"};

            await service.AddItemAsync(thirdItem);

            var itemsFromService = await service.GetItemsAsync();
            Assert.Contains(itemsFromService, a => a.Id.Equals(thirdItem.Id));
            Assert.Equal(3, itemsFromService.Count());
        }

        [Fact]
        public async Task AddItemAsyncWithPropertyNameIsNullShouldThrowException()
        {
            var service = new ToDoItemService(Context);
            var thirdItem = new ToDoItem();

            await Assert.ThrowsAsync<DbUpdateException>(() => service.AddItemAsync(thirdItem));
        }

        [Theory]
        [InlineData("testname")]
        [InlineData("TESTNAME")]
        [InlineData("Ein.Test")]
        [InlineData("$%)/)(&")]
        public async Task UpdateItemAsyncShouldChangeName(string expectedName)
        {
            var service = new ToDoItemService(Context);
            var itemToUpdate = await service.GetAsync(_firstItem.Id);
            itemToUpdate.Name = expectedName;

            await service.UpdateItemAsync(_firstItem);

            var updatedItem = await service.GetAsync(_firstItem.Id);
            Assert.Equal(expectedName, updatedItem.Name);
        }

        [Fact]
        public async Task UpdateItemAsyncWithPropertyNameIsNullShouldThrowException()
        {
            var service = new ToDoItemService(Context);
            var itemToUpdate = await service.GetAsync(_firstItem.Id);

            itemToUpdate.Name = null;

            await Assert.ThrowsAsync<DbUpdateException>(() => service.UpdateItemAsync(itemToUpdate));
        }

        [Fact]
        public async Task DeleteItemAsyncShouldDeleteItem()
        {
            var service = new ToDoItemService(Context);

            await service.DeleteItemAsync(_firstItem.Id);

            var itemsFromService = await service.GetItemsAsync();
            Assert.DoesNotContain(itemsFromService, item => item.Id.Equals(_firstItem.Id));
        }
    }
}