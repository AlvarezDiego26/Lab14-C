using System;
using System.Collections.Generic;

namespace Biblioteca.Mvc.Models;

public partial class Editoriale
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Pais { get; set; }

    public string? SitioWeb { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
