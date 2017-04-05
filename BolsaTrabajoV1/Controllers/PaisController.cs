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
    public class PaisController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: Pais
        public async Task<ActionResult> Index()
        {
            return View(await db.PAIS.ToListAsync());
        }

        // GET: Pais/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAIS pAIS = await db.PAIS.FindAsync(id);
            if (pAIS == null)
            {
                return HttpNotFound();
            }
            return View(pAIS);
        }

        // GET: Pais/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pais/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDPAIS,NOMBREPAIS,CODIGOPAIS")] PAIS pAIS)
        {
            if (ModelState.IsValid)
            {
                db.PAIS.Add(pAIS);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(pAIS);
        }

        // GET: Pais/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAIS pAIS = await db.PAIS.FindAsync(id);
            if (pAIS == null)
            {
                return HttpNotFound();
            }
            return View(pAIS);
        }

        // POST: Pais/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDPAIS,NOMBREPAIS,CODIGOPAIS")] PAIS pAIS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pAIS).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(pAIS);
        }

        // GET: Pais/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAIS pAIS = await db.PAIS.FindAsync(id);
            if (pAIS == null)
            {
                return HttpNotFound();
            }
            return View(pAIS);
        }

        // POST: Pais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PAIS pAIS = await db.PAIS.FindAsync(id);
            db.PAIS.Remove(pAIS);
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
