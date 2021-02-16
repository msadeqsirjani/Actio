using Actio.Services.Activities.Domain.Models;
using Actio.Services.Activities.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Actio.Services.Activities.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly IMongoCollection<Activity> _collection;

        public ActivityRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<Activity>("Activities");
        }

        public async Task<IEnumerable<Activity>> BrowseAsync() => await _collection.AsQueryable().ToListAsync();

        public async Task<Activity> GetAsync(Guid id) => await _collection.AsQueryable().SingleOrDefaultAsync(x => x.Id == id);

        public async Task AddAsync(Activity activity) => await _collection.InsertOneAsync(activity);
    }
}