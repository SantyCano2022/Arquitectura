using System.Reflection;
using BibliotecaAPI.Entities;
using BibliotecaAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Context
{

    public class ApplicationContext : DbContext, IApplicationContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public virtual DbSet<Devolucion> Devolucion { get; set; }

        public virtual DbSet<Material> Material { get; set; }

        public virtual DbSet<Prestamo> Prestamo { get; set; }

        public virtual DbSet<Rol> Rol { get; set; }

        public virtual DbSet<TipoMaterial> TipoMaterial { get; set; }

        public virtual DbSet<Usuario> Usuario { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        //    OnModelCreatingPartial(modelBuilder);
        //}

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }

}
