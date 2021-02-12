using Actio.Common.Commands;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;
using System;
using System.Threading.Tasks;

namespace Actio.Api.Controllers
{
    public class ActivityController : DefaultController
    {
        private readonly IBusClient _bus;

        public ActivityController(IBusClient bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateActivity command)
        {
            command.Id = Guid.NewGuid();
            command.CreatedAt = DateTime.UtcNow;
            await _bus.PublishAsync(command);

            return Accepted(new Uri($"activity/{command.Id}"));
        }
    }
}
