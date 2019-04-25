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
    public class SponsorController : Controller
    {
        private DonatimeEntities db = new DonatimeEntities();

        // GET: Sponsor
        public async Task<ActionResult> Index()
        {
            var sponsor = db.Sponsor.Include(s => s.Actividad);
            return View(await sponsor.ToListAsync());
        }

        // GET: Sponsor/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sponsor sponsor = await db.Sponsor.FindAsync(id);
            if (sponsor == null)
            {
                return HttpNotFound();
            }
            return View(sponsor);
        }

        // GET: Sponsor/Create
        public ActionResult Create()
        {
            ViewBag.idSponsor = new SelectList(db.Actividad, "idActividad", "nombreActividad");
            return View();
        }

        // POST: Sponsor/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idSponsor,nombreEmpresa,direccion,numTelefono,email,rfc")] Sponsor sponsor)
        {
            if (ModelState.IsValid)
            {
                db.Sponsor.Add(sponsor);
                await db.SaveChangesAsync();
                return RedirectToAction("Index","Home");
            }

            ViewBag.idSponsor = new SelectList(db.Actividad, "idActividad", "nombreActividad", sponsor.idSponsor);
            return View(sponsor);
        }

        // GET: Sponsor/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sponsor sponsor = await db.Sponsor.FindAsync(id);
            if (sponsor == null)
            {
                return HttpNotFound();
            }
            ViewBag.idSponsor = new SelectList(db.Actividad, "idActividad", "nombreActividad", sponsor.idSponsor);
            return View(sponsor);
        }

        // POST: Sponsor/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idSponsor,nombreEmpresa,direccion,numTelefono,email,rfc")] Sponsor sponsor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sponsor).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idSponsor = new SelectList(db.Actividad, "idActividad", "nombreActividad", sponsor.idSponsor);
            return View(sponsor);
        }

        // GET: Sponsor/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sponsor sponsor = await db.Sponsor.FindAsync(id);
            if (sponsor == null)
            {
                return HttpNotFound();
            }
            return View(sponsor);
        }

        // POST: Sponsor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Sponsor sponsor = await db.Sponsor.FindAsync(id);
            db.Sponsor.Remove(sponsor);
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
