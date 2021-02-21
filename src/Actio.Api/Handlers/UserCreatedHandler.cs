using Actio.Common.Events;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Actio.Api.Handlers
{
    public class UserCreatedHandler : IEventHandler<UserCreated>
    {
        private readonly ILogger<UserCreatedHandler> _logger;

        public UserCreatedHandler(ILogger<UserCreatedHandler> logger)
        {
            _logger = logger;
        }

        public Task HandleAsync(UserCreated @event)
        {
            _logger.LogInformation($"User published: {@event.Email} {@event.Name}");

            return Task.CompletedTask;
        }
    }
}