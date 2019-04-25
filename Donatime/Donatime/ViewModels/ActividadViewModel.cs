using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Donatime.ViewModels
{
    public class ActividadViewModel
    {
        public int idActividad { get; set; }

        [Display(Name="Actividad")]
        public string nombreActividad { get; set; }
        [Display(Name = "Lugar")]
        public string lugar { get; set; }
        [Display(Name = "Informacion actividad")]
        public string infoActividad { get; set; }
        [Display(Name = "Skills/Herramientas")]
        public string habilidadesRequisitos { get; set; }
        [Display(Name = "Imagen/Fotografia")]
        public string imagen { get; set; }
        [Display(Name = "Fecha de inicio")]
        public System.DateTime fechaInicio { get; set; }
        [Display(Name = "Fecha Final")]
        public System.DateTime fechaFin { get; set; }
        [Display(Name = "Problema")]
        public int idProblema { get; set; }
        [Display(Name = "Patrocinador")]
        public int idPatrocina { get; set; }
        [Display(Name = "Organizador")]
        public int idOrganiza { get; set; }

    }
}