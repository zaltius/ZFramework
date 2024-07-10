using ZFramework.Domain.Events;

namespace ZSample.Domain.Entities.Clientes.Events
{
    public class ClienteRazonSocialUpdatedDomainEvent : IDomainEvent
    {
        public Guid ClienteId { get; }

        public string RazonSocial { get; }

        public ClienteRazonSocialUpdatedDomainEvent(Guid clienteId, string razonSocial)
        {
            ClienteId = clienteId;
            RazonSocial = razonSocial;
        }
    }
}