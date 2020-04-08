using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminCampana_2020.ViewModels
{
    public class AgendaActividadesVM
    {
        public int id { get; set; }
        public string strActividad { get; set; }
        public string strLugar { get; set; }
        public string strDescripcion { get; set; }
        public string strHoraInicio { get; set; }
        public string strHoraTermino { get; set; }
        public DateTime dteFecha { get; set; }
    }
}