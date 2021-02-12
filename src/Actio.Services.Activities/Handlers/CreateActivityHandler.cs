using Actio.Common.Commands;
using Actio.Common.Events;
using Microsoft.Extensions.Logging;
using RawRabbit;
using System.Threading.Tasks;

namespace Actio.Services.Activities.Handlers
{
    public class CreateActivityHandler : ICommandHandler<CreateActivity>
    {
        private readonly IBusClient _bus;
        private readonly ILogger<CreateActivityHandler> _logger;

        public CreateActivityHandler(IBusClient bus, ILogger<CreateActivityHandler> logger)
        {
            _bus = bus;
            _logger = logger;
        }

        public async Task HandleAsync(CreateActivity command)
        {
            _logger.LogInformation($"Create activity: {command.Name}");

            await _bus.PublishAsync(new ActivityCreated(command.Id, command.UserId, command.Category, command.Name,
                command.Description, command.CreatedAt));
        }
    }
}
