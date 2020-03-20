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

        [Required(ErrorMessage ="El Campo es Obligatorio")]
        [DataType(DataType.Text,ErrorMessage ="El Campo Solo Acepta Letras")]
        public string StrNombre { get; set; }
        [Required(ErrorMessage = "El Campo es Obligatorio")]
        [DataType(DataType.Text, ErrorMessage = "El Campo Solo Acepta Letras")]
        public string StrApellidoPaterno { get; set; }
        [Required(ErrorMessage = "El Campo es Obligatorio")]
        [DataType(DataType.Text, ErrorMessage = "El Campo Solo Acepta Letras")]
        public string StrApellidoMaterno { get; set; }

        [DataType(DataType.EmailAddress)]
        public string StrEmail { get; set; }
        public string StrObservaciones { get; set; }
        public int idUsuario { get; set; }
        public int idStatus { get; set; }
        public DireccionVM Direccion { get; set; }
        public TelefonoVM Telefono { get; set; }
        public StatusVM Status { get; set; }
    }
}