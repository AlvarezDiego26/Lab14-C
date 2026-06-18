using Biblioteca.Mvc.Dtos;
using Biblioteca.Mvc.Repositories;

namespace Biblioteca.Mvc.Services;

public sealed class RolService(IRolRepository rolRepository) : IRolService
{
    public Task<IReadOnlyList<RolDto>> ListarAsync(CancellationToken cancellationToken = default)
    {
        return rolRepository.ListarAsync(cancellationToken);
    }
}
