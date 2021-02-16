using Actio.Services.Activities.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Actio.Services.Activities.Domain.Repositories
{
    public interface IActivityRepository
    {
        Task<IEnumerable<Activity>> BrowseAsync();
        Task<Activity> GetAsync(Guid id);
        Task AddAsync(Activity activity);
    }
}