using Microsoft.AspNetCore.Mvc;

namespace Study.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PingController : ControllerBase
{
    [HttpGet]
    public ActionResult<string> Get()
    {
        return "pong";
    }
}
