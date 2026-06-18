using Biblioteca.Mvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Mvc.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class LibrosController(ILibroService libroService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var libros = await libroService.ListarAsync(cancellationToken);
        return Ok(libros);
    }
}
