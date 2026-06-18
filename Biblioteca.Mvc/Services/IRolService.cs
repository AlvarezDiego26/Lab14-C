using Biblioteca.Mvc.Dtos;

namespace Biblioteca.Mvc.Services;

public interface IRolService
{
    Task<IReadOnlyList<RolDto>> ListarAsync(CancellationToken cancellationToken = default);
}
