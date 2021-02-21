using Actio.Common.Events;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Actio.Api.Handlers
{
    public class UserAuthenticatedHandler : IEventHandler<UserAuthenticated>
    {
        private readonly ILogger<UserAuthenticatedHandler> _logger;

        public UserAuthenticatedHandler(ILogger<UserAuthenticatedHandler> logger)
        {
            _logger = logger;
        }

        public Task HandleAsync(UserAuthenticated @event)
        {
            _logger.LogInformation($"User autheniticated: {@event.Email}");

            return Task.CompletedTask;
        }
    }
}