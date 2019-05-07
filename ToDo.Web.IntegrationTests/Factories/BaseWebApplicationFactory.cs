using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ToDo.Web.IntegrationTests.Factories
{
    // About WebApplicationFactory
    // https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-2.2#basic-tests-with-the-default-webapplicationfactory
    // https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.testing.webapplicationfactory-1?view=aspnetcore-2.2&viewFallbackFrom=aspnetcore-3.0
    public abstract class BaseWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        protected override IWebHostBuilder CreateWebHostBuilder() =>
            WebHost.CreateDefaultBuilder().UseStartup<TStartup>();
    }
}