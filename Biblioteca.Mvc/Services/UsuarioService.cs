using Biblioteca.Mvc.Dtos;
using Biblioteca.Mvc.Repositories;

namespace Biblioteca.Mvc.Services;

public sealed class UsuarioService(IUsuarioRepository usuarioRepository) : IUsuarioService
{
    public Task<IReadOnlyList<UsuarioDto>> ListarAsync(CancellationToken cancellationToken = default)
    {
        return usuarioRepository.ListarAsync(cancellationToken);
    }
}
