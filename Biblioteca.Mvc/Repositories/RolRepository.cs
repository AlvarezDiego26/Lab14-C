using Biblioteca.Mvc.Data;
using Biblioteca.Mvc.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Mvc.Repositories;

public sealed class RolRepository(BibliotecaDbContext context) : IRolRepository
{
    public async Task<IReadOnlyList<RolDto>> ListarAsync(CancellationToken cancellationToken = default)
    {
        return await context.Roles
            .AsNoTracking()
            .OrderBy(rol => rol.Nombre)
            .Select(rol => new RolDto(
                rol.Id,
                rol.Nombre,
                rol.Descripcion))
            .ToListAsync(cancellationToken);
    }
}
