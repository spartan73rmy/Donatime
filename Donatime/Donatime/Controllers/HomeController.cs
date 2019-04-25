using Donatime.DataLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Donatime.Controllers
{
    public class HomeController : Controller
    {
        private DonatimeEntities db = new DonatimeEntities();
        private string rutaImagen = @"C:\Donatime";

        public async Task<ActionResult> Index()
        {
            var ranking = db.Ranking.Include(r => r.Voluntario).OrderByDescending(m => m.cantidadHoras);
            return View(await ranking.ToListAsync());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }



        #region FilesManager
        /// <summary>
        /// Retorna la imagen que se encuentra en la ruta a la vista
        /// </summary>
        /// <param name="file"></param>
        /// <returns>Imagen</returns>
        public ActionResult Preview(string file)
        {
            if (System.IO.File.Exists(Path.Combine(rutaImagen, file)))
            {
                return File(Path.Combine(rutaImagen, file), "image/jpeg");
            }
            return new HttpNotFoundResult();
        }
        #endregion
    }
}