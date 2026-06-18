using Biblioteca.Mvc.Dtos;
using Biblioteca.Mvc.Repositories;

namespace Biblioteca.Mvc.Services;

public sealed class LibroService(ILibroRepository libroRepository) : ILibroService
{
    public Task<IReadOnlyList<LibroDto>> ListarAsync(CancellationToken cancellationToken = default)
    {
        return libroRepository.ListarAsync(cancellationToken);
    }
}
