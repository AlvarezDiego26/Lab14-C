namespace Biblioteca.Mvc.Dtos;

public sealed record LibroDto(
    Guid Id,
    string Titulo,
    string Isbn,
    int? AnioPublicacion,
    int? NumeroPaginas,
    string Estado);
