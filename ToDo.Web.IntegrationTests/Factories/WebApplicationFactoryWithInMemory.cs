using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using ToDo.Data;

namespace ToDo.Web.IntegrationTests.Factories
{
    public class WebApplicationFactoryWithInMemory : BaseWebApplicationFactory<TestStartup>
    {
        private readonly InMemoryDatabaseRoot _databaseRoot = new InMemoryDatabaseRoot();
        private readonly string _connectionString = Guid.NewGuid().ToString();

        protected override void ConfigureWebHost(IWebHostBuilder builder) =>
            builder.ConfigureServices(services =>
            {
                services
                    .AddEntityFrameworkInMemoryDatabase()
                    .AddDbContext<ToDoDbContext>(options =>
                    {
                        options.UseInMemoryDatabase(_connectionString, _databaseRoot);
                        options.UseInternalServiceProvider(services.BuildServiceProvider());
                    });
            });
    }
}