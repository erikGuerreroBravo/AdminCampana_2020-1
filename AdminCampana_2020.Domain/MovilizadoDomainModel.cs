using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminCampana_2020.Domain
{
    public class MovilizadoDomainModel
    {
        public int Id { get; set; }
        public string StrNombre { get; set; }
        public string StrApellidoPaterno { get; set; }
        public string StrApellidoMaterno{ get; set; }
        public string StrEmail { get; set; }
        public string StrObservaciones { get; set; }
        public int idUsuario { get; set; }
        public int idStatus { get; set; }

        public DireccionDomainModel Direccion { get; set; }
        public TelefonoDomainModel Telefono { get; set; }
        public UsuarioDomainModel Usuario { get; set; }
        public StatusDomainModel Status { get; set; }
    }
}
