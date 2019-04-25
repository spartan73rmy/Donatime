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
    public class ONGController : Controller
    {
        private DonatimeEntities db = new DonatimeEntities();

        // GET: ONG
        public async Task<ActionResult> Index()
        {
            return View(await db.ONG.ToListAsync());
        }

        // GET: ONG/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ONG oNG = await db.ONG.FindAsync(id);
            if (oNG == null)
            {
                return HttpNotFound();
            }
            return View(oNG);
        }

        public ActionResult SharedSocialONG()
        {
            return View();
        }

        // GET: ONG/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ONG/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idOng,nombreOng,razonSocial,rfc,domicilio,objetivoSocial,actividades,apoyosVoluntarios,inicioOperacion,tiempoOperacion")] ONG oNG)
        {
            if (ModelState.IsValid)
            {
                db.ONG.Add(oNG);
                await db.SaveChangesAsync();
                return RedirectToAction("SharedSocialONG");
            }

            return View(oNG);
        }

        // GET: ONG/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ONG oNG = await db.ONG.FindAsync(id);
            if (oNG == null)
            {
                return HttpNotFound();
            }
            return View(oNG);
        }

        // POST: ONG/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idOng,nombreOng,razonSocial,rfc,domicilio,objetivoSocial,actividades,apoyosVoluntarios,inicioOperacion,tiempoOperacion")] ONG oNG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oNG).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(oNG);
        }

        // GET: ONG/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ONG oNG = await db.ONG.FindAsync(id);
            if (oNG == null)
            {
                return HttpNotFound();
            }
            return View(oNG);
        }

        // POST: ONG/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ONG oNG = await db.ONG.FindAsync(id);
            db.ONG.Remove(oNG);
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
