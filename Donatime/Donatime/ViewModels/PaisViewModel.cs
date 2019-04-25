using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Donatime.ViewModels
{
    public class PaisViewModel
    {
        [Key]
        public int idPais { get; set; }

        [Display(Name ="Pais")]
        public string nombrePais { get; set; }

    }
}