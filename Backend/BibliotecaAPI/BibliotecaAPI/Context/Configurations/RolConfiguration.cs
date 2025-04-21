using BibliotecaAPI.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Context.Configurations
{
    public class RolConfiguration : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Rol__3214EC078546CF55");

            builder.ToTable("Rol");

            builder.HasIndex(e => e.Nombre, "UQ__Rol__75E3EFCFED34561F").IsUnique();

            builder.Property(e => e.Estado).HasDefaultValue(true);
            builder.Property(e => e.Nombre).HasMaxLength(50);
        }
    }
    
}

