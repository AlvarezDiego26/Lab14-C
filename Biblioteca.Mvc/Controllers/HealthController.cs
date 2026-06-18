using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Mvc.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            status = "OK",
            service = "Biblioteca.Mvc"
        });
    }
}
