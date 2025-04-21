using BibliotecaAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibliotecaAPI.Context.Configurations
{
    public class MaterialConfiguration: IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Material__3214EC07A6DA005D");

            builder.ToTable("Material");

            builder.Property(e => e.Estado).HasDefaultValue(true);
            builder.Property(e => e.Titulo).HasMaxLength(255);
            builder.Property(e => e.NroIdentificacion).HasMaxLength(255);

            builder.HasOne(d => d.TipoMaterial).WithMany()
                .HasForeignKey(d => d.TipoMaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Material__TipoMa__29572725");
        }
    }
    
    
}
