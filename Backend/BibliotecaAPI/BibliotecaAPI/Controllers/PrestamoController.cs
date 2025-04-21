using BibliotecaAPI.Dtos;
using BibliotecaAPI.Entities;
using BibliotecaAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace BibliotecaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamoController : Controller
    {
        private readonly PrestamoService _prestamoService;
        public PrestamoController(PrestamoService prestamoService)
        {
            _prestamoService = prestamoService;
        }

        [HttpPost("RegistrarPrestamo")]
        public async Task<IActionResult> RegistrarPrestamo([FromBody] CrearPrestamoDto dto)
        {
            if (dto == null)
            {
                return BadRequest(new { message = "El préstamo no puede ser nulo." });
            }

            var result = await _prestamoService.AddPrestamoAsync(dto);

            if(result != "Préstamo registrado correctamente.")
                return BadRequest(new { message = result });

            return Ok(new { message = result });
        }

        [HttpGet("ObtenerHistorial")]
        public async Task<IActionResult> ObtenerHistorial()
        {
            var historial = await _prestamoService.ObtenerHistorialBibliotecaAsync();
            if (historial == null || !historial.Any())
            {
                return NotFound(new { message = "No se encontraron registros en el historial." });
            }
            return Ok(historial);
        }
    }
}
