using Biblioteca.Mvc.Dtos;

namespace Biblioteca.Mvc.Repositories;

public interface IRolRepository
{
    Task<IReadOnlyList<RolDto>> ListarAsync(CancellationToken cancellationToken = default);
}
