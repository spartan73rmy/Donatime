using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Donatime.ViewModels
{
    public class VoluntarioViewModel
    {
        [Key]
        public int idVoluntario { get; set; }

        [Display(Name = "Nombre Completo")]
        public string nombre { get; set; }

        [Display(Name = "Imagen de Perfil")]
        public string imagenPerfil { get; set; }

        [Display(Name = "Municipio")]
        public string municipio { get; set; }

        [Display(Name = "Comunidad")]
        public string comunidad { get; set; }

        [Display(Name = "Colonia")]
        public string colonia { get; set; }

        [Display(Name = "Direccion")]
        public string direccion { get; set; }

        //[Display(Name = "Num de Oficina")]
        //public string numTelOficina { get; set; }

        [Display(Name = "Numero Celular")]
        public string numCelular { get; set; }

        public int idEstado { get; set; }

        public int idEscolaridad { get; set; }


    }
}