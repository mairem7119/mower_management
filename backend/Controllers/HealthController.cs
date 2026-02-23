using Microsoft.AspNetCore.Mvc;

namespace MowerManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { message = "Mower Management API", version = "1.0" });
    }
}
