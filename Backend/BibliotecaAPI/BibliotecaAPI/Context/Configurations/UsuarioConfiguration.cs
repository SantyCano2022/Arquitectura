using BibliotecaAPI.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Context.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Usuario__3214EC071F583127");

            builder.ToTable("Usuario");

            builder.HasIndex(e => e.Cedula, "UQ__Usuario__B4ADFE3858EE7959").IsUnique();

            builder.Property(e => e.Cedula).HasMaxLength(50);
            builder.Property(e => e.Estado).HasDefaultValue(true);
            builder.Property(e => e.Nombre).HasMaxLength(255);

            builder.HasOne(d => d.Rol).WithMany()
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario__RolId__31EC6D26");
        }
    }
    
    
}
