using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminCampana_2020.ViewModels
{
    public class EmailVM
    {

        [Required, Display(Name = "Tu Nombre")]
        public string FromNombre { get; set; }
        [Required, Display(Name = "Tu Email"), EmailAddress]
        public string FromEmail { get; set; }
        [Required]
        public string Mensaje { get; set; }

        public string Body { get; set; }

        public string Asunto { get; set; }
    }
}