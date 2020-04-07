using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminCampana_2020.ViewModels
{
    public class MovilizadoVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El Campo es Obligatorio")]
        [DataType(DataType.Text, ErrorMessage = "El Campo Solo Acepta Letras")]
        [MinLength(3, ErrorMessage = "El Campo Tiene que tener mínimo de 3 caracteres")]
        public string StrNombre { get; set; }

        [Required(ErrorMessage = "El Campo es Obligatorio")]
        [DataType(DataType.Text, ErrorMessage = "El Campo Solo Acepta Letras")]
        [MinLength(3, ErrorMessage = "El Campo Tiene que tener mínimo de 3 caracteres")]
        public string StrApellidoPaterno { get; set; }

        [Required(ErrorMessage = "El Campo es Obligatorio")]
        [DataType(DataType.Text, ErrorMessage = "El Campo Solo Acepta Letras")]
        [MinLength(3, ErrorMessage = "El Campo Tiene que tener mínimo de 3 caracteres")]
        public string StrApellidoMaterno { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Este Campo Solo Acepta Direcciones de Correo Electrónico")]
        public string StrEmail { get; set; }


        public string StrObservaciones { get; set; }
        public int IdUsuario { get; set; }
        public int IdStatus { get; set; }
        public DireccionVM DireccionVM { get; set; }
        public TelefonoVM TelefonoVM { get; set; }
        public StatusVM StatusvM { get; set; }
        public UsuarioVM UsuarioDomainModel { get; set; }
    }
}