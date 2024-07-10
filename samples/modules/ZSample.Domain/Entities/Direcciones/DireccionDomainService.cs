using ZFramework.Common;
using ZFramework.Common.Exceptions;
using ZFramework.Domain;
using ZSample.Domain.Entities.TiposDirecciones;

namespace ZSample.Domain.Entities.Direcciones
{
    public sealed class DireccionDomainService : IDomainService
    {
        private readonly Dictionary<string, string> provincias = new()
        {
            { "01", "Álava" },
            { "02", "Albacete" },
            { "03", "Alicante" },
            { "04", "Almería" },
            { "05", "Ávila" },
            { "06", "Badajoz" },
            { "07", "Baleares" },
            { "08", "Barcelona" },
            { "09", "Burgos" },
            { "10", "Cáceres" },
            { "11", "Cádiz" },
            { "12", "Castellón" },
            { "13", "Ciudad Real" },
            { "14", "Córdoba" },
            { "15", "La Coruña" },
            { "16", "Cuenca" },
            { "17", "Gerona/Girona" },
            { "18", "Granada" },
            { "19", "Guadalajara" },
            { "20", "Guipúzcoa" },
            { "21", "Huelva" },
            { "22", "Huesca" },
            { "23", "Jaén" },
            { "24", "León" },
            { "25", "Lérida/Lleida" },
            { "26", "La Rioja" },
            { "27", "Lugo" },
            { "28", "Madrid" },
            { "29", "Málaga" },
            { "30", "Murcia" },
            { "31", "Navarra" },
            { "32", "Orense" },
            { "33", "Asturias" },
            { "34", "Palencia" },
            { "35", "Las Palmas" },
            { "36", "Pontevedra" },
            { "37", "Salamanca" },
            { "38", "Santa Cruz de Tenerife" },
            { "39", "Cantabria" },
            { "40", "Segovia" },
            { "41", "Sevilla" },
            { "42", "Soria" },
            { "43", "Tarragona" },
            { "44", "Teruel" },
            { "45", "Toledo" },
            { "46", "Valencia" },
            { "47", "Valladolid" },
            { "48", "Vizcaya" },
            { "49", "Zamora" },
            { "50", "Zaragoza" },
            { "51", "Ceuta" },
            { "52", "Melilla" }
        };

        public DireccionDomainService()
        {
        }

        public Direccion Create(string calle, string descripcion, string poblacion, string codigoPostal, TipoDireccionEnum tipoDireccion)
        {
            var direccion = new Direccion(calle, descripcion, poblacion, tipoDireccion);

            SetCodigoPostal(direccion, codigoPostal);

            return direccion;
        }

        // Ejemplo de servicio de dominio que accedería a otro repositorio (Provincias).
        public void SetCodigoPostal(Direccion direccion, string codigoPostal)
        {
            Check.NotNullEntity(direccion);

            if (!TryGetProvincia(codigoPostal, out var provincia))
            {
                throw new FailedValidationException($"El formato del código postal {codigoPostal} es incorrecto.");
            }

            direccion.CodigoPostal = codigoPostal;
            direccion.Provincia = provincia;
        }

        public bool TryGetProvincia(string input, out string value)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (provincias.TryGetValue(input.Substring(0, 2), out value))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            value = "";

            return false;
        }
    }
}