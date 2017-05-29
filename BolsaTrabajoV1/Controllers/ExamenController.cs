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
    public class ExamenController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: Examen
        public async Task<ActionResult> Index()
        {
            var eXAMEN = db.EXAMEN.Include(e => e.EMPRESA).Include(e => e.TIPO_EXAMEN);
            return View(await eXAMEN.ToListAsync());
        }

        // GET: Examen/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EXAMEN eXAMEN = await db.EXAMEN.FindAsync(id);
            if (eXAMEN == null)
            {
                return HttpNotFound();
            }
            return View(eXAMEN);
        }

        // GET: Examen/Create
        public ActionResult Create()
        {
            ViewBag.CODIGOEMPRESA = new SelectList(db.EMPRESA, "CODIGOEMPRESA", "NOMBREEMPRESA");
            ViewBag.IDTIPOEXAMEN = new SelectList(db.TIPO_EXAMEN, "IDTIPOEXAMEN", "DESCRIPCIONTIPO");
            return View();
        }

        // POST: Examen/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CODIGOEMPRESA,ACTIVO,PONDERACION,CODIGOEXAMEN,IDTIPOEXAMEN")] EXAMEN eXAMEN)
        {
            if (ModelState.IsValid)
            {
                db.EXAMEN.Add(eXAMEN);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CODIGOEMPRESA = new SelectList(db.EMPRESA, "CODIGOEMPRESA", "NOMBREEMPRESA", eXAMEN.CODIGOEMPRESA);
            ViewBag.IDTIPOEXAMEN = new SelectList(db.TIPO_EXAMEN, "IDTIPOEXAMEN", "DESCRIPCIONTIPO", eXAMEN.IDTIPOEXAMEN);
            return View(eXAMEN);
        }

        // GET: Examen/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EXAMEN eXAMEN = await db.EXAMEN.FindAsync(id);
            if (eXAMEN == null)
            {
                return HttpNotFound();
            }
            ViewBag.CODIGOEMPRESA = new SelectList(db.EMPRESA, "CODIGOEMPRESA", "NOMBREEMPRESA", eXAMEN.CODIGOEMPRESA);
            ViewBag.IDTIPOEXAMEN = new SelectList(db.TIPO_EXAMEN, "IDTIPOEXAMEN", "DESCRIPCIONTIPO", eXAMEN.IDTIPOEXAMEN);
            return View(eXAMEN);
        }

        // POST: Examen/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CODIGOEMPRESA,ACTIVO,PONDERACION,CODIGOEXAMEN,IDTIPOEXAMEN")] EXAMEN eXAMEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eXAMEN).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CODIGOEMPRESA = new SelectList(db.EMPRESA, "CODIGOEMPRESA", "NOMBREEMPRESA", eXAMEN.CODIGOEMPRESA);
            ViewBag.IDTIPOEXAMEN = new SelectList(db.TIPO_EXAMEN, "IDTIPOEXAMEN", "DESCRIPCIONTIPO", eXAMEN.IDTIPOEXAMEN);
            return View(eXAMEN);
        }

        // GET: Examen/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EXAMEN eXAMEN = await db.EXAMEN.FindAsync(id);
            if (eXAMEN == null)
            {
                return HttpNotFound();
            }
            return View(eXAMEN);
        }

        // POST: Examen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            EXAMEN eXAMEN = await db.EXAMEN.FindAsync(id);
            db.EXAMEN.Remove(eXAMEN);
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
