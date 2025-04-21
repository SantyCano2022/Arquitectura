using BibliotecaAPI.Dtos;
using BibliotecaAPI.Entities;
using BibliotecaAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Services
{
    public class MaterialService
    {
        private readonly IApplicationContext _context;

        public MaterialService(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<ObtenerMaterialesDto>> GetAllMaterialsAsync()
        {
            return await _context.Material
                .Include(m => m.TipoMaterial)
                .Select(m => new ObtenerMaterialesDto
                {
                    Id = m.Id,
                    Titulo = m.Titulo,
                    NroIdentificacion = m.NroIdentificacion,
                    FechaRegistro = m.FechaRegistro,
                    CantidadRegistrada = m.CantidadRegistrada,
                    CantidadActual = m.CantidadActual,
                    TipoMaterialId = m.TipoMaterialId,
                    NombreTipoMaterial = m.TipoMaterial.Nombre,
                    Estado = m.Estado
                })
                .ToListAsync();
        }

        public async Task<string> AddMaterialAsync(Material material)
        {
            bool existeMaterial = await _context.Material.AnyAsync(m => m.NroIdentificacion == material.NroIdentificacion);
            if (existeMaterial)
            {
                return "El numero de material ya existe.";
            }
            material.TipoMaterial = null!;

            _context.Material.Add(material);
            await _context.SaveChangesAsync();

            return "Material registrado correctamente.";
        }

        public async Task<bool> UpdateMaterialAsync(MaterialActualizarDto materialDto)
        {
            var existingMaterial = await _context.Material.FindAsync(materialDto.Id);
            if (existingMaterial == null)
            {
                return false;
            }

            existingMaterial.CantidadRegistrada += materialDto.Cantidad;
            existingMaterial.CantidadActual += materialDto.Cantidad;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
