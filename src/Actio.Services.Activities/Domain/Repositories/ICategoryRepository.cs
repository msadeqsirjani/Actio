using Actio.Services.Activities.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Actio.Services.Activities.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> GetAsync(string name);
        Task<IEnumerable<Category>> BrowseAsync();
        Task AddAsync(Category category);
    }
}
