using BibliotecaAPI.Dtos;
using BibliotecaAPI.Entities;
using BibliotecaAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Services
{
    public class RolService
    {
        private readonly IApplicationContext _context;

        public RolService(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<Rol>> ObtenerRoles()
        {
            return await _context.Rol.ToListAsync();
                
        }
    }
}

