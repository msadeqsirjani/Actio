using Actio.Common.Events;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Actio.Api.Handlers
{
    public class ActivityCreatedHandler : IEventHandler<ActivityCreated>
    {
        private readonly ILogger<ActivityCreatedHandler> _logger;

        public ActivityCreatedHandler(ILogger<ActivityCreatedHandler> logger)
        {
            _logger = logger;
        }

        public Task HandleAsync(ActivityCreated @event)
        {
            _logger.LogInformation($"Activity creating: {@event.Category} {@event.Name}");

            return Task.CompletedTask;
        }
    }
}