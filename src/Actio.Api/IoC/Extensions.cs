using Actio.Api.Handlers;
using Actio.Common.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Actio.Api.IoC
{
    public static class Extensions
    {
        public static IServiceCollection RegisterActivityCreatedService(this IServiceCollection services)
        {
            services.AddScoped<IEventHandler<ActivityCreated>, ActivityCreatedHandler>();

            return services;
        }
    }
}
