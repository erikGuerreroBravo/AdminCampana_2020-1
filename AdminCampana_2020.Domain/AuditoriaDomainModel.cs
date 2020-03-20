using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminCampana_2020.Domain
{
    public class AuditoriaDomainModel
    {
        public int Id { get; set; }
        public int IdMovilizado { get; set; }
        public int IdUsuario { get; set; }
        public DateTime Hora { get; set; }
        public DateTime Fecha { get; set; }
        public string Operacion { get; set; }
        public string Modulo { get; set; }
    }
}
