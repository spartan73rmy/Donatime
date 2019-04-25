using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Donatime.DataLayer;
using Donatime.ViewModels;
using Donatime.Resources.Class;
using System.Data.Entity.Validation;

namespace Donatime.Controllers
{
    public class VoluntarioController : Controller
    {
        private DonatimeEntities db = new DonatimeEntities();
        private string rutaImagen = @"C:\Donatime";
        // GET: Voluntario
        public async Task<ActionResult> Index()
        {
            var voluntario = db.Voluntario.Include(v => v.Escolaridad).Include(v => v.Estado);
            return View(await voluntario.ToListAsync());
        }

        // GET: Voluntario/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voluntario voluntario = await db.Voluntario.FindAsync(id);
            if (voluntario == null)
            {
                return HttpNotFound();
            }
            return View(voluntario);
        }

        public ActionResult SharedSocial()
        {
            return View();
        }


        // GET: Voluntario/Create
        public async Task<ActionResult> Create()
        {
            var Modelo = new VoluntarioCreateViewModel()
            {
                estado = await db.Estado.ToListAsync(),
                escolaridad = await db.Escolaridad.ToListAsync(),
                voluntario = new Voluntario(),
                perfilImagen = null
            };
            return View(Modelo);
        }

        // POST: Voluntario/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(VoluntarioCreateViewModel Modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Modelo.voluntario.imagenPerfil = ImagesManager.savePhoto(Modelo.perfilImagen, rutaImagen, "Perfil_" + Modelo.perfilImagen.FileName);
                    db.Voluntario.Add(Modelo.voluntario);
                    await db.SaveChangesAsync();
                    return RedirectToAction("SharedSocial");
                }
                Modelo.escolaridad = await db.Escolaridad.ToListAsync();
                Modelo.estado = await db.Estado.ToListAsync();
                return View(Modelo);
            }
            catch (DbEntityValidationException e) { Log.writeDataBaseError(e); return RedirectToAction("Index"); }
            catch (Exception e) { Log.Write(e.Message, "Error"); return RedirectToAction("Index"); }
        }

        // GET: Voluntario/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voluntario voluntario = await db.Voluntario.FindAsync(id);
            if (voluntario == null)
            {
                return HttpNotFound();
            }
            ViewBag.idEscolaridad = new SelectList(db.Escolaridad, "idEscolaridad", "nombreEscolaridad", voluntario.idEscolaridad);
            ViewBag.idEstado = new SelectList(db.Estado, "idEstado", "nombreEstado", voluntario.idEstado);
            return View(voluntario);
        }

        // POST: Voluntario/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idVoluntario,imagenPerfil,nombre,municipio,comunidad,colonia,direccion,numTelOficina,numCelular,idEstado,idEscolaridad")] Voluntario voluntario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(voluntario).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idEscolaridad = new SelectList(db.Escolaridad, "idEscolaridad", "nombreEscolaridad", voluntario.idEscolaridad);
            ViewBag.idEstado = new SelectList(db.Estado, "idEstado", "nombreEstado", voluntario.idEstado);
            return View(voluntario);
        }

        // GET: Voluntario/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voluntario voluntario = await db.Voluntario.FindAsync(id);
            if (voluntario == null)
            {
                return HttpNotFound();
            }
            return View(voluntario);
        }

        // POST: Voluntario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Voluntario voluntario = await db.Voluntario.FindAsync(id);
            db.Voluntario.Remove(voluntario);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
