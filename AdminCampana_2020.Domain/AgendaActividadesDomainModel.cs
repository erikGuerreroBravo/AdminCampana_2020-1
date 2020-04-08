using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminCampana_2020.Domain
{
    public class AgendaActividadesDomainModel
    {
        public int id { get; set; }
        public string strActividad { get; set; }
        public string strLugar { get; set; }
        public string strDescripcion { get; set; }
        public string strHoraInicio { get; set; }
        public string strHoraTermino { get; set; }
        public DateTime dteFecha { get; set; }

        //Auxiliar

        public string Fecha { get; set; }
    }
}
