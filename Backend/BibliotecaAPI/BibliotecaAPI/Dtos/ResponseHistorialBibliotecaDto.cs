namespace BibliotecaAPI.Dtos
{
    public class ResponseHistorialBibliotecaDto
    {
        public int PrestamoId { get; set; }

        public int CantidadPrestada { set; get; }

        public DateOnly FechaPrestamo { get; set; }

        public bool Estado { get; set; }

        public DateOnly? FechaDevolucion { get; set; }

        public string NombreUsuario { get; set; } = null!;

        public string CedulaUsuario { get; set; } = null!;

        public string RolUsuario { get; set; } = null!;

        public string TituloMaterial { get; set; } = null!;

        public string NroIdentificacionMaterial { get; set; } = null!;

        public string TipoMaterial { get; set; } = null!;
    }
}
