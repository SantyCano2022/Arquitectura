using System;
using System.Collections.Generic;

namespace BibliotecaAPI.Entities;

public partial class Material
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string NroIdentificacion { get; set; } = null!;

    public DateOnly FechaRegistro { get; set; }

    public int CantidadRegistrada { get; set; }

    public int CantidadActual { get; set; }

    public int TipoMaterialId { get; set; }

    public bool Estado { get; set; }
    public TipoMaterial TipoMaterial { get; set; } = null!;
}
