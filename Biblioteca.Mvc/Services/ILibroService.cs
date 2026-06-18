using Biblioteca.Mvc.Dtos;

namespace Biblioteca.Mvc.Services;

public interface ILibroService
{
    Task<IReadOnlyList<LibroDto>> ListarAsync(CancellationToken cancellationToken = default);
}
