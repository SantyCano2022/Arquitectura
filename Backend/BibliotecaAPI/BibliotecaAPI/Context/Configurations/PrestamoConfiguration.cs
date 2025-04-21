using BibliotecaAPI.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Context.Configurations
{
    public class PrestamoConfiguration: IEntityTypeConfiguration<Prestamo>
    {
        public void Configure(EntityTypeBuilder<Prestamo> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Prestamo__3214EC076AF2F0AC");

            builder.ToTable("Prestamo");

            builder.Property(e => e.Estado).HasDefaultValue(true);

            builder.HasOne(d => d.Material).WithMany()
                .HasForeignKey(d => d.MaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Prestamo__Materi__36B12243");

            builder.HasOne(d => d.Usuario).WithMany()
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Prestamo__Usuari__35BCFE0A");
        }
    }
    
    
}
