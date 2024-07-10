using ZFramework.Common;
using ZFramework.Domain;
using ZSample.Domain.Entities.Clientes;

namespace ZSample.Domain.Entities.Facturas
{
    public sealed class FacturaDomainService : IDomainService
    {
        private readonly IClienteRepository _clienteRepository;

        public FacturaDomainService(
            IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public Factura Create(Guid clienteId, string numero, string serie, decimal baseImponible, decimal pctIva)
        {
            var cliente = _clienteRepository.GetById(clienteId);

            Check.NotNullEntity(cliente);

            //TODO recuperar direccion fiscal y comporobar que sea válida.

            var factura = new Factura(clienteId, numero, serie, baseImponible, pctIva);

            return factura;
        }
    }
}