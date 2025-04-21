using BibliotecaAPI.Dtos;
using BibliotecaAPI.Entities;
using BibliotecaAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsuarioController : Controller
    {
        private readonly UsuarioService _usuarioService;
        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet("ObtenerUsuarios")]
        public async Task<IActionResult> ObtenerUsuarios()
        {
            var result = await _usuarioService.ObtenerUsuarios();
            if (result == null || !result.Any())
            {
                return NotFound(new { message = "No se encontraron Usuarios." });
            }
            return Ok(result);

        }

        [HttpPost("RegistrarUsuario")]
        public async Task<IActionResult> RegistrarUsuario([FromBody] CrearUsuarioDto dto)
        {
            if (dto == null)
                return BadRequest(new { message = "El usuario no puede ser nulo." });

            var usuario = new Usuario
            {
                Nombre = dto.Nombre,
                Cedula = dto.Cedula,
                RolId = dto.RolId,
                Estado = true
            };

            var result = await _usuarioService.RegistrarUsuario(usuario);

            if (result == "El usuario ya existe.")
                return Conflict(new { message = result });

            return Ok(new { message = result });
        }

        [HttpDelete("EliminarUsuario/{idusuario}")]
        public async Task<IActionResult> EliminarUsuario(int idusuario)
        {
            var result = await _usuarioService.EliminarUsuario(idusuario);

            if (!result)
            {
                return NotFound(new { message = "El usuario no existe." });
            }

            return Ok(new { message = "Usuario eliminado correctamente." });
        }

    }
}
