using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminCampana_2020.Domain
{
    public class PerfilDomainModel
    {
        public int Id { get; set; }
        public string StrValor { get; set; }
        public string StrDescripcion { get; set; }

        public List<UsuarioDomainModel> UsuarioDomainModels { get; set; }

    }
}
