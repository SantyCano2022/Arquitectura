namespace BibliotecaAPI.Dtos
{
    public class CrearMaterialDto
    {
        public string Titulo { get; set; } = null!;
        public string NroIdentificacion { get; set; } = null!;
        public int CantidadRegistrada { get; set; }
        public int CantidadActual { get; set; }
        public int TipoMaterialId { get; set; }
    }
}
