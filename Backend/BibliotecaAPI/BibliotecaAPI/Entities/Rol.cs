using System;
using System.Collections.Generic;

namespace BibliotecaAPI.Entities;

public partial class Rol
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool Estado { get; set; }

}
