using Biblioteca.Mvc.Data;
using Biblioteca.Mvc.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Mvc.Repositories;

public sealed class UsuarioRepository(BibliotecaDbContext context) : IUsuarioRepository
{
    public async Task<IReadOnlyList<UsuarioDto>> ListarAsync(CancellationToken cancellationToken = default)
    {
        return await context.Usuarios
            .AsNoTracking()
            .OrderBy(usuario => usuario.Apellidos)
            .ThenBy(usuario => usuario.Nombres)
            .Select(usuario => new UsuarioDto(
                usuario.Id,
                usuario.Nombres,
                usuario.Apellidos,
                usuario.Dni,
                usuario.Email,
                usuario.Estado,
                usuario.Rol.Nombre))
            .ToListAsync(cancellationToken);
    }
}
