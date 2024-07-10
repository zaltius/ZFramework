using ZFramework.Domain.Entities.Auditing;
using ZSample.Domain.Entities.ClientesDirecciones;
using ZSample.Domain.Entities.TiposDirecciones;
using System.Text.RegularExpressions;

namespace ZSample.Domain.Entities.Direcciones
{
    public class Direccion : AuditedEntity<Guid>
    {
        private readonly string RegexCodigoPostal = @"^(?:0[1-9]|[1-4]\d|5[0-2])\d{3}$";

        public string? Calle { get; protected set; }

        public virtual IList<ClienteDireccion> Clientes { get; protected set; }

        public string? CodigoPostal { get; internal set; }

        public string? Descripcion { get; protected set; }

        public string? Poblacion { get; protected set; }

        public string? Provincia { get; internal set; }

        public TipoDireccionEnum TipoDireccion { get; protected set; }

        internal Direccion(string? calle, string? descripcion, string? poblacion, TipoDireccionEnum tipoDireccion) : this()
        {
            Calle = calle;
            Descripcion = descripcion;
            Poblacion = poblacion;
            TipoDireccion = tipoDireccion;
        }

        protected Direccion() : base()
        {
            Clientes = new List<ClienteDireccion>();
        }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Calle) &&
                !string.IsNullOrWhiteSpace(Poblacion) &&
                !string.IsNullOrWhiteSpace(Provincia) &&
                ValidateCodigoPostal(CodigoPostal);
        }

        public override string ToString()
        {
            return $"{Calle}, {CodigoPostal} - {Poblacion}, {Provincia}";
        }

        // Extract to helper class
        public bool ValidateCodigoPostal(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            input = input.Trim().ToUpper();

            return Regex.IsMatch(input, RegexCodigoPostal);
        }
    }
}