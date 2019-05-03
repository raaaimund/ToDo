using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDo.Web.IntegrationTests.Helpers;

namespace ToDo.Web.IntegrationTests
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        protected override void ConfigureDatabaseServices(IServiceCollection services)
        {
            // Database providers are injected in WebApplicationFactoryWithPROVIDER.cs classes
            services.AddTransient<TestDataSeeder>();
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.Configure(app, env);
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var seeder = serviceScope.ServiceProvider.GetService<TestDataSeeder>();
            seeder.SeedToDoItems();
        }
    }
}