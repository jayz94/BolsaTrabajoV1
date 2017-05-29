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
    public class CargoController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: Cargo
        public async Task<ActionResult> Index()
        {
            var cARGO = db.CARGO.Include(c => c.AREA_CARGO);
            return View(await cARGO.ToListAsync());
        }

        // GET: Cargo/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CARGO cARGO = await db.CARGO.FindAsync(id);
            if (cARGO == null)
            {
                return HttpNotFound();
            }
            return View(cARGO);
        }

        // GET: Cargo/Create
        public ActionResult Create()
        {
            ViewBag.IDAREACARGO = new SelectList(db.AREA_CARGO, "IDAREACARGO", "DESCRIPCIONAREA");
            return View();
        }

        // POST: Cargo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDCARGO,IDAREACARGO,NOMBRECARGO")] CARGO cARGO)
        {
            if (ModelState.IsValid)
            {
                db.CARGO.Add(cARGO);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IDAREACARGO = new SelectList(db.AREA_CARGO, "IDAREACARGO", "DESCRIPCIONAREA", cARGO.IDAREACARGO);
            return View(cARGO);
        }

        // GET: Cargo/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CARGO cARGO = await db.CARGO.FindAsync(id);
            if (cARGO == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDAREACARGO = new SelectList(db.AREA_CARGO, "IDAREACARGO", "DESCRIPCIONAREA", cARGO.IDAREACARGO);
            return View(cARGO);
        }

        // POST: Cargo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDCARGO,IDAREACARGO,NOMBRECARGO")] CARGO cARGO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cARGO).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IDAREACARGO = new SelectList(db.AREA_CARGO, "IDAREACARGO", "DESCRIPCIONAREA", cARGO.IDAREACARGO);
            return View(cARGO);
        }

        // GET: Cargo/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CARGO cARGO = await db.CARGO.FindAsync(id);
            if (cARGO == null)
            {
                return HttpNotFound();
            }
            return View(cARGO);
        }

        // POST: Cargo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CARGO cARGO = await db.CARGO.FindAsync(id);
            db.CARGO.Remove(cARGO);
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
