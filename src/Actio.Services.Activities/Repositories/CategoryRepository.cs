using Actio.Services.Activities.Domain.Models;
using Actio.Services.Activities.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Actio.Services.Activities.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoCollection<Category> _collection;

        public CategoryRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<Category>("Categories");
        }

        public async Task<Category> GetAsync(string name) => await _collection.AsQueryable().SingleOrDefaultAsync(x => x.Name == name.ToLowerInvariant());

        public async Task<IEnumerable<Category>> BrowseAsync() => await _collection.AsQueryable().ToListAsync();

        public async Task AddAsync(Category category) => await _collection.InsertOneAsync(category);
    }
}
