using System;
using ToDo.Data;
using ToDo.Dto;

namespace ToDo.Web.IntegrationTests.Helpers
{
    public class TestDataSeeder
    {
        // Ids are parsed from constants and not generated with Guid.NewGuid() because some test runners use different application domains
        // https://docs.microsoft.com/en-us/dotnet/framework/app-domains/application-domains
        public const string FirstItemId = "312658D1-8146-42E3-B57B-360427182811";
        public const string SecondItemId = "64C7E3F5-74F9-4540-9B12-BC7AFBCC7CE6";

        public static readonly ToDoItem FirstItem = new ToDoItem() {Id = Guid.Parse(FirstItemId), Name = "Item 1"};
        public static readonly ToDoItem SecondItem = new ToDoItem() {Id = Guid.Parse(SecondItemId), Name = "Item 2"};

        private readonly ToDoDbContext _context;

        public TestDataSeeder(ToDoDbContext context)
        {
            _context = context;

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        public void SeedToDoItems()
        {
            _context.ToDoItem.Add(FirstItem);
            _context.ToDoItem.Add(SecondItem);
            _context.SaveChanges();
        }
    }
}