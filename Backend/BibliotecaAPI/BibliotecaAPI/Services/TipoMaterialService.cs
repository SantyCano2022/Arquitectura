using BibliotecaAPI.Entities;
using BibliotecaAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Services
{
    public class TipoMaterialService
    {
        private readonly IApplicationContext _context;
        
        public TipoMaterialService(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<TipoMaterial>> TipoMaterialActivo()
        {
            return await _context.TipoMaterial.Where(t => t.Estado==true).ToListAsync();
        }
    }
}
