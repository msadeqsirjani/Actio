using Actio.Common.Commands;
using Actio.Services.Identity.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Actio.Services.Identity.Controllers
{
    public class AccountController : DefaultController
    {
        private readonly IUserService _user;

        public AccountController(IUserService user)
        {
            _user = user;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(AuthenticateUser command)
        {
            return Ok(await _user.LoginAsync(command.Email, command.Password));
        }
    }
}
