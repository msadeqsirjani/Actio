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

            var provider = services.BuildServiceProvider();

            var options = provider.GetService<IOptions<MongoOptions>>();

            services.AddSingleton<MongoClient>(sp => new MongoClient(options?.Value.ConnectionSting))
                .AddScoped(sp =>
                {
                    var client = provider.GetService<MongoClient>();

                    return client?.GetDatabase(options?.Value.Database);
                })
                .AddScoped<IDatabaseInitializer, MongoInitializer>();

            return services;
        }
    }
}