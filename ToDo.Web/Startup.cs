using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using ToDo.Infrastructure.Extensions;
using ToDo.Application.Extensions;
using FluentValidation.AspNetCore;
using ToDo.Application.Common.Interfaces;
using ToDo.Web.Filters;

namespace ToDo.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDatabaseServices(services);
            ConfigureDefaultServices(services);
        }

        protected void ConfigureDefaultServices(IServiceCollection services)
        {
            services.AddInfrastructure();
            services.AddApplication();
            services.Configure<CookiePolicyOptions>(options => { options.CheckConsentNeeded = context => true; });
            services.AddControllersWithViews(conf => conf.Filters.Add<ModelStateValidationFilter>())
                .AddNewtonsoftJson()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<IToDoDbContext>());
            services.AddRazorPages();
        }

        // We have to override this message in our TestStartup, because we want to inject our own database providers
        protected virtual void ConfigureDatabaseServices(IServiceCollection services)
        {
            services.AddPersistence(Configuration);
        }

        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
        }
    }
}