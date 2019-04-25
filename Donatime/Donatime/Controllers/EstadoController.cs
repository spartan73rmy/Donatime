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

namespace Donatime.Controllers
{
    public class EstadoController : Controller
    {
        private DonatimeEntities db = new DonatimeEntities();

        // GET: Estado
        public async Task<ActionResult> Index()
        {
            var estado = db.Estado.Include(e => e.Pais);
            return View(await estado.ToListAsync());
        }

        // GET: Estado/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estado estado = await db.Estado.FindAsync(id);
            if (estado == null)
            {
                return HttpNotFound();
            }
            return View(estado);
        }

        // GET: Estado/Create
        public ActionResult Create()
        {
            ViewBag.idPais = new SelectList(db.Pais, "idPais", "nombrePais");
            return View();
        }

        // POST: Estado/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idEstado,nombreEstado,idPais")] Estado estado)
        {
            if (ModelState.IsValid)
            {
                db.Estado.Add(estado);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.idPais = new SelectList(db.Pais, "idPais", "nombrePais", estado.idPais);
            return View(estado);
        }

        // GET: Estado/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estado estado = await db.Estado.FindAsync(id);
            if (estado == null)
            {
                return HttpNotFound();
            }
            ViewBag.idPais = new SelectList(db.Pais, "idPais", "nombrePais", estado.idPais);
            return View(estado);
        }

        // POST: Estado/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idEstado,nombreEstado,idPais")] Estado estado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estado).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idPais = new SelectList(db.Pais, "idPais", "nombrePais", estado.idPais);
            return View(estado);
        }

        // GET: Estado/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estado estado = await db.Estado.FindAsync(id);
            if (estado == null)
            {
                return HttpNotFound();
            }
            return View(estado);
        }

        // POST: Estado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Estado estado = await db.Estado.FindAsync(id);
            db.Estado.Remove(estado);
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
