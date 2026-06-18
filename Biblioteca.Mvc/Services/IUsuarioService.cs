using Biblioteca.Mvc.Dtos;

namespace Biblioteca.Mvc.Services;

public interface IUsuarioService
{
    Task<IReadOnlyList<UsuarioDto>> ListarAsync(CancellationToken cancellationToken = default);
}
