using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminCampana_2020.Domain
{
    public class UsuarioDomainModel
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string Clave { get; set; }
        public string ProviderName { get; set; }
        public string ProviderKey { get; set; }
        public int IdPerfil { get; set; }
        public int IdUsuario { get; set; }
        public int IdStatus { get; set; }

        //Objetos de las relaciones

        public PerfilDomainModel Perfil { get; set; }
        public StatusDomainModel Status { get; set; }
        
        public List<UsuarioRolDomainModel> UsuarioRoles { get; set; }
        
    }
}
