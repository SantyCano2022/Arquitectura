using System;
using System.Collections.Generic;

namespace BibliotecaAPI.Entities;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Cedula { get; set; } = null!;

    public int RolId { get; set; }

    public bool Estado { get; set; }
    public Rol Rol { get; set; } = null!;
}
