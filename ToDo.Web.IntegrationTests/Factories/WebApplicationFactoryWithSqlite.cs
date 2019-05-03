using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToDo.Data;

namespace ToDo.Web.IntegrationTests.Factories
{
    public class WebApplicationFactoryWithSqlite : BaseWebApplicationFactory<TestStartup>
    {
        private readonly string _connectionString = $"DataSource={Guid.NewGuid()}.db";

        protected override void ConfigureWebHost(IWebHostBuilder builder) =>
            builder.ConfigureServices(services =>
            {
                services
                    .AddEntityFrameworkSqlite()
                    .AddDbContext<ToDoDbContext>(options =>
                    {
                        options.UseSqlite(_connectionString);
                        options.UseInternalServiceProvider(services.BuildServiceProvider());
                    });
            });
    }
}