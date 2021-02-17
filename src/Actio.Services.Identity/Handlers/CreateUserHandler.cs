using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.Exceptions;
using Microsoft.Extensions.Logging;
using RawRabbit;
using System;
using System.Threading.Tasks;

namespace Actio.Services.Identity.Handlers
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly IBusClient _bus;
        private readonly ILogger<CreateUserHandler> _logger;

        public CreateUserHandler(IBusClient bus, ILogger<CreateUserHandler> logger)
        {
            _bus = bus;
            _logger = logger;
        }

        public async Task HandleAsync(CreateUser command)
        {
            try
            {
                await _bus.PublishAsync(new UserCreated(command.Email, command.Name));

                _logger.LogInformation($"User: '{command.Email}' was created with name: '{command.Name}'.");
            }
            catch (ActioException ex)
            {
                await _bus.PublishAsync(new CreateUserRejected(command.Email, ex.Message, ex.Code));

                _logger.LogError(ex, ex.Message);
            }
            catch (Exception ex)
            {
                await _bus.PublishAsync(new CreateUserRejected(command.Email, ex.Message, "error"));

                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
