using System;
using System.Collections.Generic;

namespace BibliotecaAPI.Entities;

public partial class TipoMaterial
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool Estado { get; set; }

 
}
