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
    public class AreaConocimientoController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: AreaConocimiento
        public async Task<ActionResult> Index()
        {
            return View(await db.AREA_CONOCIMIENTO.ToListAsync());
        }

        // GET: AreaConocimiento/Details/5
        public async Task<ActionResult> Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AREA_CONOCIMIENTO aREA_CONOCIMIENTO = await db.AREA_CONOCIMIENTO.FindAsync(id);
            if (aREA_CONOCIMIENTO == null)
            {
                return HttpNotFound();
            }
            return View(aREA_CONOCIMIENTO);
        }

        // GET: AreaConocimiento/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AreaConocimiento/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDAREACONOCIMIENTO,NOMBREAREACONOCIMIENTO")] AREA_CONOCIMIENTO aREA_CONOCIMIENTO)
        {
            if (ModelState.IsValid)
            {
                db.AREA_CONOCIMIENTO.Add(aREA_CONOCIMIENTO);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(aREA_CONOCIMIENTO);
        }

        // GET: AreaConocimiento/Edit/5
        public async Task<ActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AREA_CONOCIMIENTO aREA_CONOCIMIENTO = await db.AREA_CONOCIMIENTO.FindAsync(id);
            if (aREA_CONOCIMIENTO == null)
            {
                return HttpNotFound();
            }
            return View(aREA_CONOCIMIENTO);
        }

        // POST: AreaConocimiento/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDAREACONOCIMIENTO,NOMBREAREACONOCIMIENTO")] AREA_CONOCIMIENTO aREA_CONOCIMIENTO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aREA_CONOCIMIENTO).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(aREA_CONOCIMIENTO);
        }

        // GET: AreaConocimiento/Delete/5
        public async Task<ActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AREA_CONOCIMIENTO aREA_CONOCIMIENTO = await db.AREA_CONOCIMIENTO.FindAsync(id);
            if (aREA_CONOCIMIENTO == null)
            {
                return HttpNotFound();
            }
            return View(aREA_CONOCIMIENTO);
        }

        // POST: AreaConocimiento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(short id)
        {
            AREA_CONOCIMIENTO aREA_CONOCIMIENTO = await db.AREA_CONOCIMIENTO.FindAsync(id);
            db.AREA_CONOCIMIENTO.Remove(aREA_CONOCIMIENTO);
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
