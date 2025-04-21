namespace BibliotecaAPI.Dtos
{
    public class CrearUsuarioDto
    {
        public string Nombre { get; set; } = null!;
        public string Cedula { get; set; } = null!;
        public int RolId { get; set; }
    }
}
