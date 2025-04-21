namespace BibliotecaAPI.Dtos
{
    public class ConsultarUsuarioDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Cedula { get; set; } = null!;
        public string NombreRol { get; set; } = null!;

    }
}
