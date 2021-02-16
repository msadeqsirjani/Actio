using Actio.Common.Commands;
using Actio.Common.Mongo;
using Actio.Services.Activities.Domain.Repositories;
using Actio.Services.Activities.Handlers;
using Actio.Services.Activities.Repositories;
using Actio.Services.Activities.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Actio.Services.Activities.IoC
{
    public static class Extensions
    {
        public static IServiceCollection RegisterCreateActivityService(this IServiceCollection services) =>
            services.AddScoped<ICommandHandler<CreateActivity>, CreateActivityHandler>();

        public static IServiceCollection RegisterCategoryRepository(this IServiceCollection services) =>
            services.AddScoped<ICategoryRepository, CategoryRepository>();

        public static IServiceCollection RegisterActivityRepository(this IServiceCollection services) =>
            services.AddScoped<IActivityRepository, ActivityRepository>();

        public static IServiceCollection RegisterCustomMongoSeeder(this IServiceCollection services) =>
            services.AddScoped<IDatabaseSeeder, CustomMongoSeeder>();

        public static IServiceCollection RegisterActivityService(this IServiceCollection services) =>
            services.AddScoped<IActivityService, ActivityService>();
    }
}
