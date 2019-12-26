using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using ToDo.Application.Common.Behaviours;

namespace ToDo.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
            => services
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
    }
}
