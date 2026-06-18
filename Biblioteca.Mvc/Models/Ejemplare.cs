using System;
using System.Collections.Generic;

namespace Biblioteca.Mvc.Models;

public partial class Ejemplare
{
    public Guid Id { get; set; }

    public Guid LibroId { get; set; }

    public string Codigo { get; set; } = null!;

    public string? Ubicacion { get; set; }

    public string Estado { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Libro Libro { get; set; } = null!;

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
