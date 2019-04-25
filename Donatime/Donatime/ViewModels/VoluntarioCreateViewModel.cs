using Donatime.DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Donatime.ViewModels
{
    public class VoluntarioCreateViewModel
    {

        public List<Escolaridad> escolaridad { get; set; }
        public List<Estado> estado { get; set; }
        public Voluntario voluntario{ get; set; }

        [Display(Name ="Imagen de Perfil")]
        [Required(AllowEmptyStrings =false,ErrorMessage ="Selecciona una imagen")]
        public HttpPostedFileBase perfilImagen { get; set; }
    }
}