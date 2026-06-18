namespace Biblioteca.Mvc.Dtos;

public sealed record UsuarioDto(
    Guid Id,
    string Nombres,
    string Apellidos,
    string Dni,
    string Email,
    string Estado,
    string Rol);
