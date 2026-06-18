using Biblioteca.Mvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Mvc.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class RolesController(IRolService rolService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var roles = await rolService.ListarAsync(cancellationToken);
        return Ok(roles);
    }
}
