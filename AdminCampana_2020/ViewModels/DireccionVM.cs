using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminCampana_2020.ViewModels
{
    public class DireccionVM
    {
        public int Id { get; set; }
        public string StrCalle { get; set; }
        public string StrNumeroInterior { get; set; }
        public string StrNumeroExterior { get; set; }
        public int idColonia { get; set; }
        public string StrObservacion { get; set; }
        public ColoniaVM Colonia { get; set; }
        //public SeccionVM Seccion { get; set; }
        //public ZonaVM ZonaVM { get; set; }
    }
}