using BibliotecaAPI.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Context.Configurations
{
    public class TipoMaterialConfiguration: IEntityTypeConfiguration<TipoMaterial>
    {
        public void Configure(EntityTypeBuilder<TipoMaterial> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__TipoMate__3214EC074ACB285B");

            builder.ToTable("TipoMaterial");

            builder.HasIndex(e => e.Nombre, "UQ__TipoMate__75E3EFCF91A9CAE8").IsUnique();

            builder.Property(e => e.Estado).HasDefaultValue(true);
            builder.Property(e => e.Nombre).HasMaxLength(100);
        }
    }
    
    
}
