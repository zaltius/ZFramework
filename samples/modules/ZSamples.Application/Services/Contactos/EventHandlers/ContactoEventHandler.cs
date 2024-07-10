using Microsoft.Extensions.Logging;
using ZFramework.Common;
using ZFramework.Domain.Events;
using ZFramework.Domain.UnitOfWork;
using ZSample.Domain.Entities.Clientes.Events;
using ZSample.Domain.Entities.Contactos;

namespace ZSample.Application.Services.Contactos.EventHandlers
{
    public class ContactoEventHandler :
        IDomainEventHandler<ClienteRazonSocialUpdatedDomainEvent>

    {
        private readonly ILogger _logger;

        private readonly IContactoRepository _repository;

        private readonly IUnitOfWork _unitOfWork;

        public ContactoEventHandler(
            ILogger<ContactoEventHandler> logger,
            IContactoRepository repository,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public Task Handle(ClienteRazonSocialUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Manejador de evento de dominio {nameof(ClienteRazonSocialUpdatedDomainEvent)}");

            var contacto = _repository.GetInitialByClienteId(notification.ClienteId);

            Check.NotNullEntity(contacto);

            contacto!.Nombre = notification.RazonSocial;

            _unitOfWork.Commit();

            return Task.CompletedTask;
        }
    }
}