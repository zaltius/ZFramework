using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZSample.Domain.Entities.ClientesDirecciones;

namespace ZSample.Infrastructure.Repositories.ClientesDirecciones
{
    /// <summary>
    /// Many to many relationship.
    /// </summary>
    public sealed class ClienteDireccionConfiguration
        : IEntityTypeConfiguration<ClienteDireccion>
    {
        public void Configure(EntityTypeBuilder<ClienteDireccion> builder)
        {
            builder
                .HasIndex(co => new { co.ClienteId, co.DireccionId })
                .IsUnique();

            builder
                .HasOne(co => co.Direccion)
                .WithMany(td => td.Clientes)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(co => co.Cliente)
                .WithMany(td => td.Direcciones)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}