using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDo.Infrastructure.Persistence;
using ToDo.Application.Common.Interfaces;
using ToDo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ToDo.Infrastructure.Identity;

namespace ToDo.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDefaultIdentity<User>()
                .AddUserStore<ToDoUserStore>();
            return services;
        }

        public static IServiceCollection AddPersistence(
            this IServiceCollection services,
            IConfiguration configuration) =>
            services.AddDbContext<ToDoDbContext>(options =>
                options.UseSqlite(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ToDoDbContext).Assembly.FullName)))
                .AddScoped<IToDoDbContext, ToDoDbContext>();
    }
}
