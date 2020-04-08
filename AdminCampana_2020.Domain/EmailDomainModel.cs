using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminCampana_2020.Domain
{
    public class EmailDomainModel
    {

        public string FromNombre { get; set; }
        public string FromEmail { get; set; }
        public string Mensaje { get; set; }
        public string Body { get; set; }
        public string Asunto { get; set; }
    }
}
