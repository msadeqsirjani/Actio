using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Common.Mongo
{
    public interface IDatabaseSeeder
    {
        Task SeedAsync();
    }

    public class MongoSeeder : IDatabaseSeeder
    {
        private readonly IMongoDatabase _database;

        public MongoSeeder(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task SeedAsync()
        {
            var collectionCursor = await _database.ListCollectionsAsync();
            var collections = await collectionCursor.ToListAsync();

            if (collections.Any()) return;

            await CustomSeed();
        }

        protected virtual Task CustomSeed() => Task.CompletedTask;
    }
}
