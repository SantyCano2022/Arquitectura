namespace BibliotecaAPI.Dtos
{
    public class ObtenerMaterialesDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public string NroIdentificacion { get; set; } = null!;
        public DateOnly FechaRegistro { get; set; }
        public int CantidadRegistrada { get; set; }
        public int CantidadActual { get; set; }
        public int TipoMaterialId { get; set; }
        public string NombreTipoMaterial { get; set; } = null!;
        public bool Estado { get; set; }
    }
}
