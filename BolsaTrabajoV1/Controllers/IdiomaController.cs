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
    public class IdiomaController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: Idioma
        public async Task<ActionResult> Index()
        {
            //var iDIOMA = db.IDIOMA.Include(i => i.CURRICULUM);
            int idCurriculum = (int)Session["idCurriculum"];
            var idioma = from idi in db.IDIOMA where idi.IDCURRICULUM == idCurriculum select idi;
            return View(await idioma.ToListAsync());
        }

        // GET: Idioma/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IDIOMA iDIOMA = await db.IDIOMA.FindAsync(id);
            if (iDIOMA == null)
            {
                return HttpNotFound();
            }
            return View(iDIOMA);
        }

        // GET: Idioma/Create
        public ActionResult Create()
        {
           // ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM");
            return View();
        }

        // POST: Idioma/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "NOMBREIDIOMA,LENGUAMATERNA,ESCRITURA,LECTURA,CONVERSACION,ESCUCHA")] IDIOMA iDIOMA)
        {
            if (ModelState.IsValid)
            {
                iDIOMA.IDCURRICULUM = (int)Session["idCurriculum"];
                iDIOMA.IDPOSTULANTE = (int)Session["idPostulante"];
                db.IDIOMA.Add(iDIOMA);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            //ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM", iDIOMA.IDCURRICULUM);
            return View(iDIOMA);
        }

        // GET: Idioma/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IDIOMA iDIOMA = await db.IDIOMA.FindAsync(id);
            if (iDIOMA == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM", iDIOMA.IDCURRICULUM);
            return View(iDIOMA);
        }

        // POST: Idioma/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDIDIOMA,IDCURRICULUM,IDPOSTULANTE,NOMBREIDIOMA,LENGUAMATERNA,ESCRITURA,LECTURA,CONVERSACION,ESCUCHA")] IDIOMA iDIOMA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iDIOMA).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM", iDIOMA.IDCURRICULUM);
            return View(iDIOMA);
        }

        // GET: Idioma/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IDIOMA iDIOMA = await db.IDIOMA.FindAsync(id);
            if (iDIOMA == null)
            {
                return HttpNotFound();
            }
            return View(iDIOMA);
        }

        // POST: Idioma/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            IDIOMA iDIOMA = await db.IDIOMA.FindAsync(id);
            db.IDIOMA.Remove(iDIOMA);
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
