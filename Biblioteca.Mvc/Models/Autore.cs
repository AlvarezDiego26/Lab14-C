using System;
using System.Collections.Generic;

namespace Biblioteca.Mvc.Models;

public partial class Autore
{
    public Guid Id { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string? Nacionalidad { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
