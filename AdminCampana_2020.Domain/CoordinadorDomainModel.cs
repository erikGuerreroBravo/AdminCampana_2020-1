using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminCampana_2020.Domain
{
    public class CoordinadorDomainModel
    {
        public int IdCoordinador { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }

        public string NombreMovilizado { get; set; }
        public string ApellidoPaternoMovilizado { get; set; }
        public string ApellidoMaternoMovilizado { get; set; }

        public string CalleMovilizado { get; set; }
        public string NumeroInteriorMovilizado { get; set; }

        //public string ColoniaMovilizado { get; set; }
        public string CodigoPostalMovilizado { get; set; }
        public string TipoAsentamientoMovilizado { get; set; }
        public string AsentamientoMovilizado { get; set; }

        public string TelefonoCelular { get; set; }
    }
}
