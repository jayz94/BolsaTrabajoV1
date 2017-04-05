using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BolsaTrabajoV1.Models;

namespace BolsaTrabajoV1.Controllers
{
    public class CurriculumController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: Curriculum
        public async Task<ActionResult> Index()
        {
            var cURRICULUM = db.CURRICULUM.Include(c => c.POSTULANTE);
            return View(await cURRICULUM.ToListAsync());
        }

        // GET: Curriculum/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CURRICULUM cURRICULUM = await db.CURRICULUM.FindAsync(id);
            if (cURRICULUM == null)
            {
                return HttpNotFound();
            }
            return View(cURRICULUM);
        }

        // GET: Curriculum/Create
        public ActionResult Create()
        {
            ViewBag.IDPOSTULANTE = new SelectList(db.POSTULANTE, "IDPOSTULANTE", "NOMBREPOSTULANTE");
            return View();
        }

        // POST: Curriculum/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDCURRICULUM,IDPOSTULANTE")] CURRICULUM cURRICULUM)
        {
            if (ModelState.IsValid)
            {
                db.CURRICULUM.Add(cURRICULUM);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IDPOSTULANTE = new SelectList(db.POSTULANTE, "IDPOSTULANTE", "NOMBREPOSTULANTE", cURRICULUM.IDPOSTULANTE);
            return View(cURRICULUM);
        }

        // GET: Curriculum/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CURRICULUM cURRICULUM = await db.CURRICULUM.FindAsync(id);
            if (cURRICULUM == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDPOSTULANTE = new SelectList(db.POSTULANTE, "IDPOSTULANTE", "NOMBREPOSTULANTE", cURRICULUM.IDPOSTULANTE);
            return View(cURRICULUM);
        }

        // POST: Curriculum/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDCURRICULUM,IDPOSTULANTE")] CURRICULUM cURRICULUM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cURRICULUM).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IDPOSTULANTE = new SelectList(db.POSTULANTE, "IDPOSTULANTE", "NOMBREPOSTULANTE", cURRICULUM.IDPOSTULANTE);
            return View(cURRICULUM);
        }

        // GET: Curriculum/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CURRICULUM cURRICULUM = await db.CURRICULUM.FindAsync(id);
            if (cURRICULUM == null)
            {
                return HttpNotFound();
            }
            return View(cURRICULUM);
        }

        // POST: Curriculum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CURRICULUM cURRICULUM = await db.CURRICULUM.FindAsync(id);
            db.CURRICULUM.Remove(cURRICULUM);
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
