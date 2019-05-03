using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using ToDo.Dto;
using Xunit;

namespace ToDo.Data.Tests.ConfigurationTests
{
    public class ToDoItemConfigurationTests : TestWithSqlite
    {
        [Fact]
        public void TableShouldGetCreated()
        {
            Assert.False(DbContext.ToDoItem.Any());
        }

        [Fact]
        public void NameIsRequired()
        {
            var newItem = new ToDoItem();
            DbContext.ToDoItem.Add(newItem);

            Assert.Throws<DbUpdateException>(() => DbContext.SaveChanges());
        }

        [Fact]
        public void AddedItemShouldGetGeneratedId()
        {
            var newItem = new ToDoItem() { Name = "Testitem" };
            DbContext.ToDoItem.Add(newItem);
            DbContext.SaveChanges();

            Assert.NotEqual(Guid.Empty, newItem.Id);
        }

        [Fact]
        public void AddedItemShouldGetPersisted()
        {
            var newItem = new ToDoItem() { Name = "Testitem" };
            DbContext.ToDoItem.Add(newItem);
            DbContext.SaveChanges();

            Assert.Equal(newItem, DbContext.ToDoItem.Find(newItem.Id));
            Assert.Equal(1, DbContext.ToDoItem.Count());
        }
    }
}
