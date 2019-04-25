using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Donatime.ViewModels
{
    public class EstadoViewModel
    {
        [Key]
        public int idEstado { get; set; }

        [Display(Name ="Estado")]
        public string nombreEstado { get; set; }
        public int idPais { get; set; }
    }
}