using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using ToDo.Data;
using ToDo.Dto;
using ToDo.Web.Extensions;

namespace ToDo.Web
{
    public class Program
    {
        public static void Main(string[] args) =>
            CreateHostBuilder(args)
            .Build()
            .MigrateDbContext<ToDoDbContext>(serviceProvider =>
            {
                var context = serviceProvider.GetRequiredService<ToDoDbContext>();
                if(!context.ToDoItem.Any())
                {
                    context.ToDoItem.Add(new ToDoItem() { Id = Guid.NewGuid(), Name = "Item 1" });
                    context.ToDoItem.Add(new ToDoItem() { Id = Guid.NewGuid(), Name = "Item 2" });
                    context.SaveChanges();
                }
            })
            .Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(configure =>
                {
                    configure.UseStartup<Startup>();
                });
    }
}
