using Biblioteca.Mvc.Dtos;

namespace Biblioteca.Mvc.Repositories;

public interface IUsuarioRepository
{
    Task<IReadOnlyList<UsuarioDto>> ListarAsync(CancellationToken cancellationToken = default);
}
