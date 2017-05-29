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
    public class AreaCargoController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: AreaCargo
        public async Task<ActionResult> Index()
        {
            return View(await db.AREA_CARGO.ToListAsync());
        }

        // GET: AreaCargo/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AREA_CARGO aREA_CARGO = await db.AREA_CARGO.FindAsync(id);
            if (aREA_CARGO == null)
            {
                return HttpNotFound();
            }
            return View(aREA_CARGO);
        }

        // GET: AreaCargo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AreaCargo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DESCRIPCIONAREA,IDAREACARGO")] AREA_CARGO aREA_CARGO)
        {
            if (ModelState.IsValid)
            {
                db.AREA_CARGO.Add(aREA_CARGO);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(aREA_CARGO);
        }

        // GET: AreaCargo/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AREA_CARGO aREA_CARGO = await db.AREA_CARGO.FindAsync(id);
            if (aREA_CARGO == null)
            {
                return HttpNotFound();
            }
            return View(aREA_CARGO);
        }

        // POST: AreaCargo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DESCRIPCIONAREA,IDAREACARGO")] AREA_CARGO aREA_CARGO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aREA_CARGO).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(aREA_CARGO);
        }

        // GET: AreaCargo/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AREA_CARGO aREA_CARGO = await db.AREA_CARGO.FindAsync(id);
            if (aREA_CARGO == null)
            {
                return HttpNotFound();
            }
            return View(aREA_CARGO);
        }

        // POST: AreaCargo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AREA_CARGO aREA_CARGO = await db.AREA_CARGO.FindAsync(id);
            db.AREA_CARGO.Remove(aREA_CARGO);
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
