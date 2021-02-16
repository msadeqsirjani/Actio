using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Actio.Common.Mongo
{
    public interface IDatabaseInitializer
    {
        Task InitializeAsync();
    }

    public class MongoInitializer : IDatabaseInitializer
    {
        private bool _initialized;
        private readonly IMongoDatabase _database;
        private readonly bool _seed;

        public MongoInitializer(IMongoDatabase database, IOptions<MongoOptions> options)
        {
            _database = database;
            _seed = options.Value.Seed;
        }

        public Task InitializeAsync()
        {
            if (_initialized) return Task.CompletedTask;

            RegisterConventions();

            _initialized = true;

            return Task.CompletedTask;
        }

        private static void RegisterConventions()
        {
            ConventionRegistry.Register("ActioConventions", new MongoConventions(), x => true);
        }


        private class MongoConventions : IConventionPack
        {
            public IEnumerable<IConvention> Conventions => new List<IConvention>
            {
                 new IgnoreExtraElementsConvention(true),
                 new EnumRepresentationConvention(BsonType.String),
                 new CamelCaseElementNameConvention()
            };
        }
    }
}
