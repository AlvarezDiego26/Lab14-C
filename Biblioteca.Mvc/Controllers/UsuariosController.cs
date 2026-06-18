using Biblioteca.Mvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Mvc.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class UsuariosController(IUsuarioService usuarioService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var usuarios = await usuarioService.ListarAsync(cancellationToken);
        return Ok(usuarios);
    }
}
