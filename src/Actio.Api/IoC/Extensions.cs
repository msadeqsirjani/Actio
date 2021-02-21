using Actio.Api.Handlers;
using Actio.Common.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Actio.Api.IoC
{
    public static class Extensions
    {
        public static IServiceCollection AddActivityCreatedService(this IServiceCollection services) =>
            services.AddScoped<IEventHandler<ActivityCreated>, ActivityCreatedHandler>();

        public static IServiceCollection AddUserCreatedService(this IServiceCollection services) =>
            services.AddScoped<IEventHandler<UserCreated>, UserCreatedHandler>();
    }
}
