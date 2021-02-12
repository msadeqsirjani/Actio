using Actio.Common.Commands;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;
using System.Threading.Tasks;

namespace Actio.Api.Controllers
{
    public class UserController : DefaultController
    {
        private readonly IBusClient _bus;

        public UserController(IBusClient bus)
        {
            _bus = bus;
        }

        [HttpPost(Name = "Register")]
        public async Task<IActionResult> Post(CreateUser command)
        {
            await _bus.PublishAsync(command);

            return Accepted();
        }
    }
}
