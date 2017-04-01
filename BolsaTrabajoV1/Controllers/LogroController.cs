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
        private BolsaTrabajoV1Entities1 db = new BolsaTrabajoV1Entities1();

        // GET: Logro
        public async Task<ActionResult> Index()
        {
            return View(await db.Logro.ToListAsync());
        }

        // GET: Logro/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Logro logro = await db.Logro.FindAsync(id);
            if (logro == null)
            {
                return HttpNotFound();
            }
            return View(logro);
        }

        // GET: Logro/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Logro/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idLogro,tituloLogro,fechaLogro,otorganteLogro,descripcionLogro")] Logro logro)
        {
            if (ModelState.IsValid)
            {
                db.Logro.Add(logro);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(logro);
        }

        // GET: Logro/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Logro logro = await db.Logro.FindAsync(id);
            if (logro == null)
            {
                return HttpNotFound();
            }
            return View(logro);
        }

        // POST: Logro/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idLogro,tituloLogro,fechaLogro,otorganteLogro,descripcionLogro")] Logro logro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(logro).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(logro);
        }

        // GET: Logro/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Logro logro = await db.Logro.FindAsync(id);
            if (logro == null)
            {
                return HttpNotFound();
            }
            return View(logro);
        }

        // POST: Logro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Logro logro = await db.Logro.FindAsync(id);
            db.Logro.Remove(logro);
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
