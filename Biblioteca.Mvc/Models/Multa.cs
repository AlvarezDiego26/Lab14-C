using System;
using System.Collections.Generic;

namespace Biblioteca.Mvc.Models;

public partial class Multa
{
    public Guid Id { get; set; }

    public Guid PrestamoId { get; set; }

    public decimal Monto { get; set; }

    public string Motivo { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public DateOnly? FechaPago { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Prestamo Prestamo { get; set; } = null!;
}
