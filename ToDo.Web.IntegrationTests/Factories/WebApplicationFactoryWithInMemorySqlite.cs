using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToDo.Data;

namespace ToDo.Web.IntegrationTests.Factories
{
    public class WebApplicationFactoryWithInMemorySqlite : BaseWebApplicationFactory<TestStartup>
    {
        private readonly string _connectionString = "DataSource=:memory:";
        private readonly SqliteConnection _connection;

        public WebApplicationFactoryWithInMemorySqlite()
        {
            _connection = new SqliteConnection(_connectionString);
            _connection.Open();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder) =>
            builder.ConfigureServices(services =>
            {
                services
                    .AddEntityFrameworkSqlite()
                    .AddDbContext<ToDoDbContext>(options =>
                    {
                        options.UseSqlite(_connection);
                        options.UseInternalServiceProvider(services.BuildServiceProvider());
                    });
            });

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _connection.Close();
        }
    }
}