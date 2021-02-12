using Microsoft.AspNetCore.Mvc;

namespace Actio.Api.Controllers
{
    public class HomeController : DefaultController
    {
        public IActionResult Get() => Content("Hello from Actio Api");
    }
}
