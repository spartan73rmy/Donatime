using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Donatime.ViewModels
{
    public class SponsorViewModel
    {
        [Key]
        public int idSponsor { get; set; }

        [Display(Name = "Nombre de la Empresa")]
        public string nombreEmpresa { get; set; }

        [Display(Name = "Direccion")]
        public string direccion { get; set; }

        [Display(Name = "Numero de Telefono")]
        public string numTelefono { get; set; }

        [Display(Name = "E-mail")]
        public string email { get; set; }

        [Display(Name = "RFC")]
        public string rfc { get; set; }

    }
}