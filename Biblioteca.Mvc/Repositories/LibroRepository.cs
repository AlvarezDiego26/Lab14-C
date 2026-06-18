using Biblioteca.Mvc.Data;
using Biblioteca.Mvc.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Mvc.Repositories;

public sealed class LibroRepository(BibliotecaDbContext context) : ILibroRepository
{
    public async Task<IReadOnlyList<LibroDto>> ListarAsync(CancellationToken cancellationToken = default)
    {
        return await context.Libros
            .AsNoTracking()
            .OrderBy(libro => libro.Titulo)
            .Select(libro => new LibroDto(
                libro.Id,
                libro.Titulo,
                libro.Isbn,
                libro.AnioPublicacion,
                libro.NumeroPaginas,
                libro.Estado))
            .ToListAsync(cancellationToken);
    }
}
