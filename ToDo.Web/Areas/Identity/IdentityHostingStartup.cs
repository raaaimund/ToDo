using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(ToDo.Web.Areas.Identity.IdentityHostingStartup))]

namespace ToDo.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => { });
        }
    }
}