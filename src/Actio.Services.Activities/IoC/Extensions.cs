using Actio.Common.Commands;
using Actio.Services.Activities.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace Actio.Services.Activities.IoC
{
    public static class Extensions
    {
        public static IServiceCollection RegisterCreateActivityService(this IServiceCollection services)
        {
            services.AddScoped<ICommandHandler<CreateActivity>, CreateActivityHandler>();

            return services;
        }
    }
}
