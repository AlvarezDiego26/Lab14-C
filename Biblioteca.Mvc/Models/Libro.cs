using System;
using System.Collections.Generic;

namespace Biblioteca.Mvc.Models;

public partial class Libro
{
    public Guid Id { get; set; }

    public Guid? EditorialId { get; set; }

    public string Titulo { get; set; } = null!;

    public string Isbn { get; set; } = null!;

    public int? AnioPublicacion { get; set; }

    public int? NumeroPaginas { get; set; }

    public string? Descripcion { get; set; }

    public string Estado { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Editoriale? Editorial { get; set; }

    public virtual ICollection<Ejemplare> Ejemplares { get; set; } = new List<Ejemplare>();

    public virtual ICollection<Autore> Autors { get; set; } = new List<Autore>();

    public virtual ICollection<Categoria> Categoria { get; set; } = new List<Categoria>();
}
