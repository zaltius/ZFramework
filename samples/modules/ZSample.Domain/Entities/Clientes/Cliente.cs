using ZFramework.Common.Exceptions;
using ZFramework.Domain.Entities.Auditing;
using ZFramework.Domain.Events;
using ZSample.Domain.Entities.Clientes.Events;
using ZSample.Domain.Entities.ClientesDirecciones;
using ZSample.Domain.Entities.Contactos;
using ZSample.Domain.Entities.Facturas;
using System.Diagnostics.CodeAnalysis;

namespace ZSample.Domain.Entities.Clientes
{
    public class Cliente : FullAuditedEntity<Guid>
    {
        public string Cif { get; protected set; } = null!;

        public virtual IList<Contacto> Contactos { get; protected set; }

        public virtual IList<ClienteDireccion> Direcciones { get; protected set; }

        public virtual IList<Factura> Facturas { get; protected set; }

        public DateTime? FechaConstitucion { get; set; }

        public string RazonSocial { get; protected set; } = null!;

        internal Cliente(string cif, string razonSocial) : this()
        {
            SetCif(cif);

            SetRazonSocial(razonSocial);
        }

        protected Cliente() : base()
        {
            Contactos = new List<Contacto>();
            Direcciones = new List<ClienteDireccion>();
            Facturas = new List<Factura>();
        }

        [MemberNotNull(nameof(Cif))]
        public void SetCif(string cif)
        {
            if (!ValidateCif(cif))
            {
                throw new FailedValidationException($"Cif {cif} inválido. No se puede asignar al cliente.");
            }

            Cif = cif;
        }

        [MemberNotNull(nameof(RazonSocial))]
        public void SetRazonSocial(string razonSocial)
        {
            if (string.IsNullOrWhiteSpace(razonSocial))
            {
                throw new FailedValidationException($"La propiedad {nameof(RazonSocial)} no puede ser nula, cadena vacía o espacio en blanco.");
            }

            if (!string.IsNullOrWhiteSpace(RazonSocial) &&
                RazonSocial != razonSocial.ToUpper())
            {
                AddClienteDomainEvent(new ClienteRazonSocialUpdatedDomainEvent(Id, razonSocial));
            }

            RazonSocial = razonSocial.ToUpper();
        }

        // Extract to helper class
        internal static bool ValidateCif(string cif)
        {
            return !string.IsNullOrWhiteSpace(cif);
        }

        internal void AddClienteDomainEvent(IDomainEvent domainEvent)
        {
            AddDomainEvent(domainEvent);
        }
    }
}