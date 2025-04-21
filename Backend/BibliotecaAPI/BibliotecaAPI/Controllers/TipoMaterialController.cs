using BibliotecaAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoMaterialController : Controller
    {
        private readonly TipoMaterialService _tipoMaterialService;
        public TipoMaterialController(TipoMaterialService tipoMaterialService)
        {
            _tipoMaterialService = tipoMaterialService;
        }

        [HttpGet("ObtenerTipoMaterial")]
        public async Task<IActionResult> ObtenerTipoMaterial()
        {
            var result = await _tipoMaterialService.TipoMaterialActivo();
            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron tipos de material.");
            }
            return Ok(result);

        }
    }
}
