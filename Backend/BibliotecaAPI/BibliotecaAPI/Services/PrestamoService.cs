using BibliotecaAPI.Dtos;
using BibliotecaAPI.Entities;
using BibliotecaAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Services
{
    public class PrestamoService
    {
        private readonly IApplicationContext _context;

        public PrestamoService(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<string> AddPrestamoAsync(CrearPrestamoDto dto)
        {
            var material = await _context.Material.FindAsync(dto.MaterialId);
            if (material == null)
            {
                return "El material no existe.";
            }

            if (material.CantidadActual < dto.CantidadPrestada)
            {
                return $"Solo hay {material.CantidadActual} unidad(es) disponible(s) del material.";
            }

            var usuario = await _context.Usuario.Include(u => u.Rol)
                                                .FirstOrDefaultAsync(u => u.Id == dto.UsuarioId);
            if (usuario == null)
            {
                return "El usuario no existe.";
            }

            int prestamosActivos = await _context.Prestamo
                .Where(p => p.UsuarioId == dto.UsuarioId && p.Estado == true)
                .SumAsync(p => p.CantidadPrestada);

            int limitePrestamos = usuario.RolId switch
            {
                1 => 5,
                2 => 3,
                3 => 1,
                _ => 0
            };

            if(dto.CantidadPrestada > limitePrestamos)
            {
                return $"El usuario con rol {usuario.Rol.Nombre} solo puede tener hasta {limitePrestamos} material(es) prestado(s) en total. Esta solicitando {dto.CantidadPrestada} lo cual excede el limite";
            }

            if ((prestamosActivos + dto.CantidadPrestada) > limitePrestamos)
            {
                return $"El usuario con rol {usuario.Rol.Nombre} solo puede tener hasta {limitePrestamos} material(es) prestado(s) en total. Actualmente tiene {prestamosActivos}, esta solicitando {dto.CantidadPrestada} lo cual excede el limite";
            }

            var prestamo = new Prestamo
            {
                UsuarioId = dto.UsuarioId,
                MaterialId = dto.MaterialId,
                CantidadPrestada = dto.CantidadPrestada,
                FechaPrestamo = DateOnly.FromDateTime(DateTime.Now),
                Estado = true
            };

            _context.Prestamo.Add(prestamo);

            material.CantidadActual -= dto.CantidadPrestada;

            await _context.SaveChangesAsync();

            return "Préstamo registrado correctamente.";
        }

        public async Task<List<ResponseHistorialBibliotecaDto>> ObtenerHistorialBibliotecaAsync()
        {
            var prestamos = await _context.Prestamo
                .Include(p => p.Usuario).ThenInclude(u => u.Rol)
                .Include(p => p.Material).ThenInclude(m => m.TipoMaterial)
                .ToListAsync();

            var devoluciones = await _context.Devolucion.ToListAsync();

            var historial = prestamos.Select(p =>
            {
                var devolucion = devoluciones.FirstOrDefault(d => d.PrestamoId == p.Id);

                return new ResponseHistorialBibliotecaDto
                {
                    PrestamoId = p.Id,
                    FechaPrestamo = p.FechaPrestamo,
                    Estado = p.Estado,
                    FechaDevolucion = devolucion?.FechaDevolucion,
                    CantidadPrestada = p.CantidadPrestada,

                    NombreUsuario = p.Usuario.Nombre,
                    CedulaUsuario = p.Usuario.Cedula,
                    RolUsuario = p.Usuario.Rol.Nombre,

                    TituloMaterial = p.Material.Titulo,
                    NroIdentificacionMaterial = p.Material.NroIdentificacion,
                    TipoMaterial = p.Material.TipoMaterial.Nombre
                };
            }).ToList();

            return historial;
        }

    }
}
