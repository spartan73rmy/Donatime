using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Donatime.ViewModels
{
    public class RankingViewModel
    {        
        public int idRanking { get; set; }

        public int idVoluntario { get; set; }

        [Display(Name ="Cantidad de Horas")]
        public int cantidadHoras { get; set; }

    }
}