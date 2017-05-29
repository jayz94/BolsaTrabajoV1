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
    public class TipoRequisitoController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: TipoRequisito
        public async Task<ActionResult> Index()
        {
            return View(await db.TIPO_REQUISITO.ToListAsync());
        }

        // GET: TipoRequisito/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPO_REQUISITO tIPO_REQUISITO = await db.TIPO_REQUISITO.FindAsync(id);
            if (tIPO_REQUISITO == null)
            {
                return HttpNotFound();
            }
            return View(tIPO_REQUISITO);
        }

        // GET: TipoRequisito/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoRequisito/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DESCRIPCIONTIPO,TABLA,IDTIPOREQUISITO")] TIPO_REQUISITO tIPO_REQUISITO)
        {
            if (ModelState.IsValid)
            {
                db.TIPO_REQUISITO.Add(tIPO_REQUISITO);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tIPO_REQUISITO);
        }

        // GET: TipoRequisito/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPO_REQUISITO tIPO_REQUISITO = await db.TIPO_REQUISITO.FindAsync(id);
            if (tIPO_REQUISITO == null)
            {
                return HttpNotFound();
            }
            return View(tIPO_REQUISITO);
        }

        // POST: TipoRequisito/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DESCRIPCIONTIPO,TABLA,IDTIPOREQUISITO")] TIPO_REQUISITO tIPO_REQUISITO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tIPO_REQUISITO).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tIPO_REQUISITO);
        }

        // GET: TipoRequisito/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPO_REQUISITO tIPO_REQUISITO = await db.TIPO_REQUISITO.FindAsync(id);
            if (tIPO_REQUISITO == null)
            {
                return HttpNotFound();
            }
            return View(tIPO_REQUISITO);
        }

        // POST: TipoRequisito/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TIPO_REQUISITO tIPO_REQUISITO = await db.TIPO_REQUISITO.FindAsync(id);
            db.TIPO_REQUISITO.Remove(tIPO_REQUISITO);
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
