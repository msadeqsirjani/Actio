using Microsoft.AspNetCore.Mvc;

namespace Actio.Services.Activities.Controllers
{
    public class HomeController : DefaultController
    {
        public IActionResult Get() => Content("Hello from Actio.Service.Activities Api");
    }
}
