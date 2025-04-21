using System;
using System.Collections.Generic;

namespace BibliotecaAPI.Entities;

public partial class Prestamo
{
    public int Id { get; set; }

    public int CantidadPrestada { get; set; }

    public int UsuarioId { get; set; }

    public int MaterialId { get; set; }

    public DateOnly FechaPrestamo { get; set; }

    public bool Estado { get; set; }

    public Material Material { get; set; } = null!;

    public Usuario Usuario { get; set; } = null!;
}
