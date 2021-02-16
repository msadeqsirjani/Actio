using System;
using System.Threading.Tasks;

namespace Actio.Services.Activities.Services
{
    public interface IActivityService
    {
        Task AddAsync(Guid id, string name, string category, string description, Guid userId, DateTime createdAt);
    }
}
