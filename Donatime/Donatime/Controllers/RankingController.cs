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
    public class RankingController : Controller
    {
        private DonatimeEntities db = new DonatimeEntities();

        // GET: Ranking
        public async Task<ActionResult> Index()
        {
            var ranking = db.Ranking.Include(r => r.Voluntario);
            return View(await ranking.ToListAsync());
        }

        // GET: Ranking/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ranking ranking = await db.Ranking.FindAsync(id);
            if (ranking == null)
            {
                return HttpNotFound();
            }
            return View(ranking);
        }

        // GET: Ranking/Create
        public ActionResult Create()
        {
            ViewBag.idActividad = new SelectList(db.Actividad, "idActividad", "nombreActividad");
            ViewBag.idVoluntario = new SelectList(db.Voluntario, "idVoluntario", "nombre");
            return View();
        }

        // POST: Ranking/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Ranking ranking)
        {
            if (ModelState.IsValid)
            {
                Ranking exist = await db.Ranking.Where(m => m.idVoluntario == ranking.idVoluntario).FirstOrDefaultAsync();
                if (exist != null)
                {
                    exist.cantidadHoras += ranking.cantidadHoras;
                    db.Entry(exist).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("SharedSocial", "Voluntario");
                }
                else
                {
                    ranking.fechaRegistro = DateTime.Now;
                    db.Ranking.Add(ranking);
                    await db.SaveChangesAsync();
                    return RedirectToAction("SharedSocial","Voluntario");
                }
            }

            ViewBag.idActividad = new SelectList(db.Actividad, "idActividad", "nombreActividad", ranking.idActividad);
            ViewBag.idVoluntario = new SelectList(db.Voluntario, "idVoluntario", "nombre", ranking.idVoluntario);
            return View(ranking);
        }

        // GET: Ranking/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ranking ranking = await db.Ranking.FindAsync(id);
            if (ranking == null)
            {
                return HttpNotFound();
            }
            ViewBag.idActividad = new SelectList(db.Actividad, "idActividad", "nombreActividad", ranking.idActividad);
            ViewBag.idVoluntario = new SelectList(db.Voluntario, "idVoluntario", "imagenPerfil", ranking.idVoluntario);
            return View(ranking);
        }

        // POST: Ranking/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idRanking,cantidadHoras,fechaRegistro,idVoluntario,idActividad")] Ranking ranking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ranking).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idActividad = new SelectList(db.Actividad, "idActividad", "nombreActividad", ranking.idActividad);
            ViewBag.idVoluntario = new SelectList(db.Voluntario, "idVoluntario", "imagenPerfil", ranking.idVoluntario);
            return View(ranking);
        }

        // GET: Ranking/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ranking ranking = await db.Ranking.FindAsync(id);
            if (ranking == null)
            {
                return HttpNotFound();
            }
            return View(ranking);
        }

        // POST: Ranking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Ranking ranking = await db.Ranking.FindAsync(id);
            db.Ranking.Remove(ranking);
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
