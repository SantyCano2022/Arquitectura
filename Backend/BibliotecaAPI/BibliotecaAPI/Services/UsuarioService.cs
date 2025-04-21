using BibliotecaAPI.Dtos;
using BibliotecaAPI.Entities;
using BibliotecaAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Services
{
    public class UsuarioService
    {
        private readonly IApplicationContext _context;

        public UsuarioService(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<ConsultarUsuarioDto>> ObtenerUsuarios()
        {
            return await _context.Usuario
                .Include(u => u.Rol)
                .Select(u => new ConsultarUsuarioDto
                {
                    Id = u.Id,
                    Nombre = u.Nombre,
                    Cedula = u.Cedula,
                    NombreRol = u.Rol.Nombre
                })
                .ToListAsync();
        }

        public async Task<string> RegistrarUsuario(Usuario usuario)
        {
            bool existeUsuario = await _context.Usuario.AnyAsync(u => u.Cedula == usuario.Cedula);

            if (existeUsuario)
            {
                return "El usuario ya existe.";
            }

            usuario.Rol = null!;

            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();

            return "Usuario registrado correctamente.";
        }

        public async Task<bool> EliminarUsuario(int idusuario)
        {
            var usuarioExistente = await _context.Usuario.FindAsync(idusuario);

            if (usuarioExistente == null)
            {
                return false;
            }

            _context.Usuario.Remove(usuarioExistente);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
