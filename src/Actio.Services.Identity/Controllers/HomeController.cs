using Microsoft.AspNetCore.Mvc;

namespace Actio.Services.Identity.Controllers
{
    public class HomeController : DefaultController
    {
        public IActionResult Get() => Content("Hello from Actio.Service.Identity Api");
    }
}
