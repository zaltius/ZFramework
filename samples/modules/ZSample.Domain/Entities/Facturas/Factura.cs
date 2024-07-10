using ZFramework.Domain.Entities.Auditing;
using ZSample.Domain.Entities.Clientes;

namespace ZSample.Domain.Entities.Facturas
{
    public class Factura : FullAuditedEntity<Guid>
    {
        public decimal BaseImponible { get; private set; }

        public virtual Cliente Cliente { get; internal set; } = null!;

        public Guid ClienteId { get; protected set; }

        public DateTime FechaEntrada { get; protected set; }

        // Propiedad derivada no almacenada en BD.
        public decimal ImporteIva { get => BaseImponible * (PctIva / 100); }

        public string Numero { get; protected set; } = null!;

        public decimal PctIva { get; private set; }

        public string Serie { get; protected set; } = null!;

        // Propiedad derivada almacenada en BD.
        public decimal Total { get; private set; }

        // Constructor, todas las creaciones de facturas deben pasar por aquí para setear las propiedades de una determinada forma.
        internal Factura(Guid clienteId, string numero, string serie, decimal baseImponible, decimal pctIva) : this()
        {
            ClienteId = clienteId;
            Numero = numero;
            Serie = serie;
            FechaEntrada = DateTime.UtcNow;

            SetImportes(baseImponible, pctIva);
        }

        protected Factura() : base()
        { }

        internal void SetImportes(decimal baseImponible, decimal iva)
        {
            BaseImponible = baseImponible;
            PctIva = iva;
            Total = BaseImponible * (1 + (PctIva / 100));
        }
    }
}