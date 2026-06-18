using System;
using System.Collections.Generic;

namespace Biblioteca.Mvc.Models;

public partial class Prestamo
{
    public Guid Id { get; set; }

    public Guid UsuarioId { get; set; }

    public Guid EjemplarId { get; set; }

    public DateOnly FechaPrestamo { get; set; }

    public DateOnly FechaVencimiento { get; set; }

    public DateOnly? FechaDevolucion { get; set; }

    public string Estado { get; set; } = null!;

    public string? Observacion { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Ejemplare Ejemplar { get; set; } = null!;

    public virtual Multa? Multa { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
