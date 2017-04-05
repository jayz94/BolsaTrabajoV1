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
    public class TipoReferenciaController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: TipoReferencia
        public async Task<ActionResult> Index()
        {
            return View(await db.TIPOREFERENCIA.ToListAsync());
        }

        // GET: TipoReferencia/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPOREFERENCIA tIPOREFERENCIA = await db.TIPOREFERENCIA.FindAsync(id);
            if (tIPOREFERENCIA == null)
            {
                return HttpNotFound();
            }
            return View(tIPOREFERENCIA);
        }

        // GET: TipoReferencia/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoReferencia/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDTIPOREFERENCIA,NOMBRETIPOREFERENCIA")] TIPOREFERENCIA tIPOREFERENCIA)
        {
            if (ModelState.IsValid)
            {
                db.TIPOREFERENCIA.Add(tIPOREFERENCIA);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tIPOREFERENCIA);
        }

        // GET: TipoReferencia/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPOREFERENCIA tIPOREFERENCIA = await db.TIPOREFERENCIA.FindAsync(id);
            if (tIPOREFERENCIA == null)
            {
                return HttpNotFound();
            }
            return View(tIPOREFERENCIA);
        }

        // POST: TipoReferencia/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDTIPOREFERENCIA,NOMBRETIPOREFERENCIA")] TIPOREFERENCIA tIPOREFERENCIA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tIPOREFERENCIA).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tIPOREFERENCIA);
        }

        // GET: TipoReferencia/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPOREFERENCIA tIPOREFERENCIA = await db.TIPOREFERENCIA.FindAsync(id);
            if (tIPOREFERENCIA == null)
            {
                return HttpNotFound();
            }
            return View(tIPOREFERENCIA);
        }

        // POST: TipoReferencia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TIPOREFERENCIA tIPOREFERENCIA = await db.TIPOREFERENCIA.FindAsync(id);
            db.TIPOREFERENCIA.Remove(tIPOREFERENCIA);
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
