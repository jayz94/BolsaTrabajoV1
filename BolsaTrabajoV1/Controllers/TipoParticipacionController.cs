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
    public class TipoParticipacionController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: TipoParticipacion
        public async Task<ActionResult> Index()
        {
            return View(await db.TIPO_PARTICIPACION.ToListAsync());
        }

        // GET: TipoParticipacion/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPO_PARTICIPACION tIPO_PARTICIPACION = await db.TIPO_PARTICIPACION.FindAsync(id);
            if (tIPO_PARTICIPACION == null)
            {
                return HttpNotFound();
            }
            return View(tIPO_PARTICIPACION);
        }

        // GET: TipoParticipacion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoParticipacion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDTIPOPARTICIPACION,NOMBRETIPOPARTICIPACION")] TIPO_PARTICIPACION tIPO_PARTICIPACION)
        {
            if (ModelState.IsValid)
            {
                db.TIPO_PARTICIPACION.Add(tIPO_PARTICIPACION);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tIPO_PARTICIPACION);
        }

        // GET: TipoParticipacion/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPO_PARTICIPACION tIPO_PARTICIPACION = await db.TIPO_PARTICIPACION.FindAsync(id);
            if (tIPO_PARTICIPACION == null)
            {
                return HttpNotFound();
            }
            return View(tIPO_PARTICIPACION);
        }

        // POST: TipoParticipacion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDTIPOPARTICIPACION,NOMBRETIPOPARTICIPACION")] TIPO_PARTICIPACION tIPO_PARTICIPACION)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tIPO_PARTICIPACION).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tIPO_PARTICIPACION);
        }

        // GET: TipoParticipacion/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPO_PARTICIPACION tIPO_PARTICIPACION = await db.TIPO_PARTICIPACION.FindAsync(id);
            if (tIPO_PARTICIPACION == null)
            {
                return HttpNotFound();
            }
            return View(tIPO_PARTICIPACION);
        }

        // POST: TipoParticipacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TIPO_PARTICIPACION tIPO_PARTICIPACION = await db.TIPO_PARTICIPACION.FindAsync(id);
            db.TIPO_PARTICIPACION.Remove(tIPO_PARTICIPACION);
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
