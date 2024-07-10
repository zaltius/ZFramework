using ZFramework.Domain;
using ZSample.Domain.Entities.Contactos;

namespace ZSample.Domain.Entities.Clientes
{
    public class ClienteDomainService : IDomainService
    {
        //protected readonly ContactoDomainService ContactoDomainService;

        public ClienteDomainService(
           // ContactoDomainService contactoDomainService
           )
        {
            //ContactoDomainService = contactoDomainService;
        }

        public Cliente Create(string cif, string razonSocial)
        {
            var cliente = new Cliente(cif, razonSocial);

            AddContacto(cliente);

            return cliente;
        }

        private void AddContacto(Cliente cliente)
        {
            var contacto = new Contacto()
            {
                Nombre = cliente.RazonSocial,
                ClienteId = cliente.Id,
                InitialCreation = true,
            };

            cliente.Contactos.Add(contacto);
        }
    }
}