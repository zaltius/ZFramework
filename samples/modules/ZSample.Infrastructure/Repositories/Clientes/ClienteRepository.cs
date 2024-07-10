using ZFramework.Common.Filtering;
using ZFramework.Infrastructure.EntityFrameworkCore.Repositories;
using ZSample.Domain.Entities.Clientes;
using ZSample.Domain.Entities.TiposDirecciones;

namespace ZSample.Infrastructure.Repositories.Clientes
{
    public class ClienteRepository : RepositoryBase<Cliente, Guid, ApplicationDbContext>, IClienteRepository
    {
        public ClienteRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Recupera todos los clientes que cumplan las siguientes condiciones:
        ///     Tener una dirección fiscal.
        ///     Tener alguna factura.
        /// </summary>
        /// <param name="filteringOptions">Opciones de ordenación y paginación.</param>
        /// <returns>Colección paginada de clientes.</returns>
        public IPagedEnumerable<Cliente> GetHavingFacturasAndDirecciones(IFilteringOptions? filteringOptions = null)
        {
            return DbSet.Where(c => c.Facturas.Any()
                && c.Direcciones.Any(d => d.Direccion.TipoDireccion == TipoDireccionEnum.Fiscal))
                .PageResult(filteringOptions);
        }
    }
}