using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Donatime.ViewModels
{
    public class ONGViewModel
    {
        public int idOng { get; set; }

        [Display(Name = "Nombre de la Organizacion")]
        public string nombreOng { get; set; }

        [Display(Name = "Razon Social")]

        public string razonSocial { get; set; }

        [Display(Name = "RFC")]
        public string rfc { get; set; }

        [Display(Name = "Domicilio")]
        public string domicilio { get; set; }

        [Display(Name = "Objetivo Social")]
        public string objetivoSocial { get; set; }

        [Display(Name = "Actividades Generales")]
        public string actividades { get; set; }

        [Display(Name = "Apoyos Voluntarios")]
        public decimal apoyosVoluntarios { get; set; }
    }
}