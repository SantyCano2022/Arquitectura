using BibliotecaAPI.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Context.Configurations
{
    public class DevolucionConfiguration: IEntityTypeConfiguration<Devolucion>
    {
        public void Configure(EntityTypeBuilder<Devolucion> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Devoluci__3214EC07FE5BBBCC");

            builder.ToTable("Devolucion");

            builder.HasOne(d => d.Prestamo).WithMany()
                .HasForeignKey(d => d.PrestamoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Devolucio__Prest__398D8EEE");
        }
    }
    
    
}
