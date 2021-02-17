using Actio.Services.Identity.Domain.Models;
using Actio.Services.Identity.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Threading.Tasks;

namespace Actio.Services.Identity.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _collection;

        public UserRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<User>("Users");
        }

        public async Task<User> GetAsync(Guid id) =>
            await _collection.AsQueryable().SingleOrDefaultAsync(x => x.Id == id);

        public async Task<User> GetAsync(string email) =>
            await _collection.AsQueryable().SingleOrDefaultAsync(x => x.Email == email.ToLowerInvariant());

        public async Task AddAsync(User user) => await _collection.InsertOneAsync(user);
    }
}
