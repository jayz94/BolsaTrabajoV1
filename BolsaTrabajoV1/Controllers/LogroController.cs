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
    public class LogroController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: Logro
        public async Task<ActionResult> Index()
        {
            //var lOGRO = db.LOGRO.Include(l => l.CURRICULUM);
            int idCurriculum = (int)Session["idCurriculum"];
            var logro = from log in db.LOGRO where log.IDCURRICULUM == idCurriculum select log;
            return View(await logro.ToListAsync());
        }

        // GET: Logro/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOGRO lOGRO = await db.LOGRO.FindAsync(id);
            if (lOGRO == null)
            {
                return HttpNotFound();
            }
            return View(lOGRO);
        }

        // GET: Logro/Create
        public ActionResult Create()
        {
            //ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM");
            return View();
        }

        // POST: Logro/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TITULOLOGRO,FECHALOGRO,OTORGANTELOGRO,DESCRIPCIONLOGRO,PREMIO,LABORSOCIAL")] LOGRO lOGRO)
        {
            if (ModelState.IsValid)
            {
                lOGRO.IDCURRICULUM = (int)Session["idCurriculum"];
                lOGRO.IDPOSTULANTE = (int)Session["idPostulante"];
                db.LOGRO.Add(lOGRO);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            //ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM", lOGRO.IDCURRICULUM);
            return View(lOGRO);
        }

        // GET: Logro/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOGRO lOGRO = await db.LOGRO.FindAsync(id);
            if (lOGRO == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM", lOGRO.IDCURRICULUM);
            return View(lOGRO);
        }

        // POST: Logro/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDLOGRO,IDCURRICULUM,IDPOSTULANTE,TITULOLOGRO,FECHALOGRO,OTORGANTELOGRO,DESCRIPCIONLOGRO,PREMIO,LABORSOCIAL")] LOGRO lOGRO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lOGRO).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM", lOGRO.IDCURRICULUM);
            return View(lOGRO);
        }

        // GET: Logro/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOGRO lOGRO = await db.LOGRO.FindAsync(id);
            if (lOGRO == null)
            {
                return HttpNotFound();
            }
            return View(lOGRO);
        }

        // POST: Logro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            LOGRO lOGRO = await db.LOGRO.FindAsync(id);
            db.LOGRO.Remove(lOGRO);
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
