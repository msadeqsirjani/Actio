using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Actio.Common.Mongo
{
    public static class Extensions
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration) =>
            services.Configure<MongoOptions>(configuration.GetSection("Mongo"))
                .AddSingleton(sp =>
                {
                    var options = sp.GetService<IOptions<MongoOptions>>();

                    return new MongoClient(options?.Value.ConnectionString);
                })
                .AddScoped(sp =>
                {
                    var options = sp.GetService<IOptions<MongoOptions>>();
                    var client = sp.GetService<MongoClient>();

                    return client?.GetDatabase(options?.Value.Database);
                })
                .AddScoped<IDatabaseInitializer, MongoInitializer>()
                .AddScoped<IDatabaseSeeder, MongoSeeder>();
    }
}