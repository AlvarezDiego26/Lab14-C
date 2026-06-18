using Biblioteca.Mvc.Dtos;

namespace Biblioteca.Mvc.Repositories;

public interface ILibroRepository
{
    Task<IReadOnlyList<LibroDto>> ListarAsync(CancellationToken cancellationToken = default);
}
