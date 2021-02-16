using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.Exceptions;
using Actio.Services.Activities.Services;
using Microsoft.Extensions.Logging;
using RawRabbit;
using System;
using System.Threading.Tasks;

namespace Actio.Services.Activities.Handlers
{
    public class CreateActivityHandler : ICommandHandler<CreateActivity>
    {
        private readonly IBusClient _bus;
        private readonly IActivityService _activityService;
        private readonly ILogger<CreateActivityHandler> _logger;

        public CreateActivityHandler(IBusClient bus, IActivityService activityService, ILogger<CreateActivityHandler> logger)
        {
            _bus = bus;
            _activityService = activityService;
            _logger = logger;
        }

        public async Task HandleAsync(CreateActivity command)
        {
            _logger.LogInformation($"Creating activity: {command.Category} {command.Name}");

            try
            {
                await _activityService.AddAsync(command.Id, command.Name, command.Category, command.Description,
                    command.UserId, command.CreatedAt);

                await _bus.PublishAsync(new ActivityCreated(command.Id, command.UserId, command.Category, command.Name,
                    command.Description, command.CreatedAt));
            }
            catch (ActioException ex)
            {
                await _bus.PublishAsync(new CreateActivityRejected(command.Id, ex.Message, ex.Code));

                _logger.LogError(ex.Message);
            }
            catch (Exception ex)
            {
                await _bus.PublishAsync(new CreateActivityRejected(command.Id, ex.Message, "error"));

                _logger.LogError(ex.Message);
            }

        }
    }
}
