using BibliotecaAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : Controller
    {
        private readonly RolService _rolService;
        public RolController(RolService rolService)
        {
            _rolService = rolService;
        }

        [HttpGet("ObtenerRoles")]
        public async Task<IActionResult> ObtenerRoles()
        {
            var result = await _rolService.ObtenerRoles();
            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron Roles.");
            }
            return Ok(result);

        }
    }
}
