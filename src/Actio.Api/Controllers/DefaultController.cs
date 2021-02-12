using Microsoft.AspNetCore.Mvc;

namespace Actio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class DefaultController : ControllerBase
    {
    }
}
