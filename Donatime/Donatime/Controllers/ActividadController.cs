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
using System.Data.Entity.Validation;
using Donatime.Resources.Class;

namespace Donatime.Controllers
{
    public class ActividadController : Controller
    {
        private DonatimeEntities db = new DonatimeEntities();

        // GET: Actividad
        public async Task<ActionResult> Index()
        {
            var actividad = db.Actividad.Include(a => a.ONG).Include(a => a.Problematica).Include(a => a.Sponsor);
            return View(await actividad.ToListAsync());
        }

        // GET: Actividad/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actividad actividad = await db.Actividad.FindAsync(id);
            if (actividad == null)
            {
                return HttpNotFound();
            }
            return View(actividad);
        }

        // GET: Actividad/Create
        public ActionResult Create()
        {
            //ViewBag.idPatrocina = new SelectList(db.ONG, "idOng", "nombreOng");

            ViewBag.idOrganiza = new SelectList(db.ONG, "idOng", "nombreOng");
            ViewBag.idProblema = new SelectList(db.Problematica, "idProblema", "Nombre");
            ViewBag.idPatrocina = new SelectList(db.Sponsor, "idSponsor", "nombreEmpresa");
            return View();
        }

        // POST: Actividad/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Actividad actividad)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    db.Actividad.Add(actividad);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                ViewBag.idOrganiza = new SelectList(db.ONG, "idOng", "nombreOng", actividad.idOrganiza);
                ViewBag.idProblema = new SelectList(db.Problematica, "idProblema", "Nombre", actividad.idProblema);
                ViewBag.idPatrocina = new SelectList(db.Sponsor, "idSponsor", "nombreEmpresa", actividad.idPatrocina);
                return View(actividad);
            }
            catch (DbEntityValidationException e) { Log.writeDataBaseError(e); return RedirectToAction("Index"); }
            catch (Exception e) { Log.Write(e.Message, "Error"); return RedirectToAction("Index"); }
        }

        // GET: Actividad/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actividad actividad = await db.Actividad.FindAsync(id);
            if (actividad == null)
            {
                return HttpNotFound();
            }
            ViewBag.idOrganiza = new SelectList(db.ONG, "idOng", "nombreOng", actividad.idOrganiza);
            ViewBag.idProblema = new SelectList(db.Problematica, "idProblema", "Nombre", actividad.idProblema);
            ViewBag.idActividad = new SelectList(db.Sponsor, "idSponsor", "nombreEmpresa", actividad.idActividad);
            return View(actividad);
        }

        // POST: Actividad/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idActividad,nombreActividad,lugar,infoActividad,habilidadesRequisitos,imagen,fechaInicio,fechaFin,idProblema,idPatrocina,idOrganiza")] Actividad actividad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(actividad).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idOrganiza = new SelectList(db.ONG, "idOng", "nombreOng", actividad.idOrganiza);
            ViewBag.idProblema = new SelectList(db.Problematica, "idProblema", "Nombre", actividad.idProblema);
            ViewBag.idActividad = new SelectList(db.Sponsor, "idSponsor", "nombreEmpresa", actividad.idActividad);
            return View(actividad);
        }

        // GET: Actividad/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actividad actividad = await db.Actividad.FindAsync(id);
            if (actividad == null)
            {
                return HttpNotFound();
            }
            return View(actividad);
        }

        // POST: Actividad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Actividad actividad = await db.Actividad.FindAsync(id);
            db.Actividad.Remove(actividad);
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
