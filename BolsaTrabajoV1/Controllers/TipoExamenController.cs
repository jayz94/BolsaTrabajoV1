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
    public class TipoExamenController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: TipoExamen
        public async Task<ActionResult> Index()
        {
            return View(await db.TIPO_EXAMEN.ToListAsync());
        }

        // GET: TipoExamen/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPO_EXAMEN tIPO_EXAMEN = await db.TIPO_EXAMEN.FindAsync(id);
            if (tIPO_EXAMEN == null)
            {
                return HttpNotFound();
            }
            return View(tIPO_EXAMEN);
        }

        // GET: TipoExamen/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoExamen/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DESCRIPCIONTIPO,IDTIPOEXAMEN")] TIPO_EXAMEN tIPO_EXAMEN)
        {
            if (ModelState.IsValid)
            {
                db.TIPO_EXAMEN.Add(tIPO_EXAMEN);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tIPO_EXAMEN);
        }

        // GET: TipoExamen/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPO_EXAMEN tIPO_EXAMEN = await db.TIPO_EXAMEN.FindAsync(id);
            if (tIPO_EXAMEN == null)
            {
                return HttpNotFound();
            }
            return View(tIPO_EXAMEN);
        }

        // POST: TipoExamen/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DESCRIPCIONTIPO,IDTIPOEXAMEN")] TIPO_EXAMEN tIPO_EXAMEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tIPO_EXAMEN).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tIPO_EXAMEN);
        }

        // GET: TipoExamen/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPO_EXAMEN tIPO_EXAMEN = await db.TIPO_EXAMEN.FindAsync(id);
            if (tIPO_EXAMEN == null)
            {
                return HttpNotFound();
            }
            return View(tIPO_EXAMEN);
        }

        // POST: TipoExamen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TIPO_EXAMEN tIPO_EXAMEN = await db.TIPO_EXAMEN.FindAsync(id);
            db.TIPO_EXAMEN.Remove(tIPO_EXAMEN);
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
