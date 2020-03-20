using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminCampana_2020.ViewModels
{
    public class StatusVM
    {
        public int Id { get; set; }
        public string StrValor { get; set; }
        public string StrDescripcion { get; set; }

        public List<UsuarioVM> Usuario { get; set; }
        public List<MovilizadoVM> Movilizado { get; set; }
    }
}