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
    public class TipoHabilidadController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: TipoHabilidad
        public async Task<ActionResult> Index()
        {
            var tIPO_HABILIDAD = db.TIPO_HABILIDAD.Include(t => t.AREA_CONOCIMIENTO);
            return View(await tIPO_HABILIDAD.ToListAsync());
        }

        // GET: TipoHabilidad/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPO_HABILIDAD tIPO_HABILIDAD = await db.TIPO_HABILIDAD.FindAsync(id);
            if (tIPO_HABILIDAD == null)
            {
                return HttpNotFound();
            }
            return View(tIPO_HABILIDAD);
        }

        // GET: TipoHabilidad/Create
        public ActionResult Create()
        {
            ViewBag.IDAREACONOCIMIENTO = new SelectList(db.AREA_CONOCIMIENTO, "IDAREACONOCIMIENTO", "NOMBREAREACONOCIMIENTO");
            return View();
        }

        // POST: TipoHabilidad/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDTIPOHABILIDAD,IDAREACONOCIMIENTO,NOMBRETIPOHABILIDAD")] TIPO_HABILIDAD tIPO_HABILIDAD)
        {
            if (ModelState.IsValid)
            {
                db.TIPO_HABILIDAD.Add(tIPO_HABILIDAD);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IDAREACONOCIMIENTO = new SelectList(db.AREA_CONOCIMIENTO, "IDAREACONOCIMIENTO", "NOMBREAREACONOCIMIENTO", tIPO_HABILIDAD.IDAREACONOCIMIENTO);
            return View(tIPO_HABILIDAD);
        }

        // GET: TipoHabilidad/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPO_HABILIDAD tIPO_HABILIDAD = await db.TIPO_HABILIDAD.FindAsync(id);
            if (tIPO_HABILIDAD == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDAREACONOCIMIENTO = new SelectList(db.AREA_CONOCIMIENTO, "IDAREACONOCIMIENTO", "NOMBREAREACONOCIMIENTO", tIPO_HABILIDAD.IDAREACONOCIMIENTO);
            return View(tIPO_HABILIDAD);
        }

        // POST: TipoHabilidad/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDTIPOHABILIDAD,IDAREACONOCIMIENTO,NOMBRETIPOHABILIDAD")] TIPO_HABILIDAD tIPO_HABILIDAD)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tIPO_HABILIDAD).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IDAREACONOCIMIENTO = new SelectList(db.AREA_CONOCIMIENTO, "IDAREACONOCIMIENTO", "NOMBREAREACONOCIMIENTO", tIPO_HABILIDAD.IDAREACONOCIMIENTO);
            return View(tIPO_HABILIDAD);
        }

        // GET: TipoHabilidad/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPO_HABILIDAD tIPO_HABILIDAD = await db.TIPO_HABILIDAD.FindAsync(id);
            if (tIPO_HABILIDAD == null)
            {
                return HttpNotFound();
            }
            return View(tIPO_HABILIDAD);
        }

        // POST: TipoHabilidad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TIPO_HABILIDAD tIPO_HABILIDAD = await db.TIPO_HABILIDAD.FindAsync(id);
            db.TIPO_HABILIDAD.Remove(tIPO_HABILIDAD);
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
