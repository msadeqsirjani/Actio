using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Actio.Common.Mongo
{
    public static class Extensions
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<MongoOptions>(configuration.GetSection("Mongo"));
            var options = services.BuildServiceProvider().GetService<IOptions<MongoOptions>>();
            services.AddSingleton(sp => new MongoClient(options?.Value.ConnectionString));
            services.AddScoped(sp => sp.GetService<MongoClient>()?.GetDatabase(options?.Value.Database));
            services.AddScoped<IDatabaseInitializer, MongoInitializer>();
            services.AddScoped<IDatabaseSeeder, MongoSeeder>();

            return services;
        }
    }
}