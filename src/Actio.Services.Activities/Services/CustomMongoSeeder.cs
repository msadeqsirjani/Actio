using Actio.Common.Mongo;
using Actio.Services.Activities.Domain.Models;
using Actio.Services.Activities.Domain.Repositories;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Services.Activities.Services
{
    public class CustomMongoSeeder : MongoSeeder
    {
        private readonly ICategoryRepository _repository;

        public CustomMongoSeeder(IMongoDatabase database, ICategoryRepository repository) : base(database)
        {
            _repository = repository;
        }

        protected override Task CustomSeed()
        {
            var categories = new List<string>
            {
                "Work",
                "Sport",
                "Hobby"
            };

            return Task.WhenAll(categories.Select(x => _repository.AddAsync(new Category(x))));
        }
    }
}
