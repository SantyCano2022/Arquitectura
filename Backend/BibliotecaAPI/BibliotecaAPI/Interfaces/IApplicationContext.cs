using BibliotecaAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Interfaces
{
    public interface IApplicationContext
    {
         DbSet<Devolucion> Devolucion { get; set; }

        DbSet<Material> Material { get; set; }

        DbSet<Prestamo> Prestamo { get; set; }

        DbSet<Rol> Rol { get; set; }

        DbSet<TipoMaterial> TipoMaterial { get; set; }

        DbSet<Usuario> Usuario { get; set; }

        Task<int> SaveChangesAsync();
    }
}
