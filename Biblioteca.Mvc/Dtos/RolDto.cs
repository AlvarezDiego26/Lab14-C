namespace Biblioteca.Mvc.Dtos;

public sealed record RolDto(
    Guid Id,
    string Nombre,
    string? Descripcion);
