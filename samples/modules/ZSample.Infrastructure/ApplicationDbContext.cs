using MediatR;
using Microsoft.EntityFrameworkCore;
using ZFramework.Infrastructure.EntityFrameworkCore;
using ZSample.Domain.Entities.Clientes;
using ZSample.Domain.Entities.ClientesDirecciones;
using ZSample.Domain.Entities.Contactos;
using ZSample.Domain.Entities.Direcciones;
using ZSample.Domain.Entities.Facturas;

namespace ZSample.Infrastructure
{
    public class ApplicationDbContext : EfCoreDbContext
    {
        public DbSet<Cliente> Clientes => Set<Cliente>();

        public DbSet<ClienteDireccion> ClientesDirecciones => Set<ClienteDireccion>();

        public DbSet<Contacto> Contactos => Set<Contacto>();

        public DbSet<Direccion> Direcciones => Set<Direccion>();

        public DbSet<Factura> Facturas => Set<Factura>();

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMediator mediator)
            : base(options, mediator)
        {
        }
    }
}