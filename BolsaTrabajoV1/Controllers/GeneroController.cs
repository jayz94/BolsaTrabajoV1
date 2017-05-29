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
    public class GeneroController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: Genero
        public async Task<ActionResult> Index()
        {
            return View(await db.GENERO.ToListAsync());
        }

        // GET: Genero/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GENERO gENERO = await db.GENERO.FindAsync(id);
            if (gENERO == null)
            {
                return HttpNotFound();
            }
            return View(gENERO);
        }

        // GET: Genero/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genero/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDGENERO,DESCRIPCIONGENERO")] GENERO gENERO)
        {
            if (ModelState.IsValid)
            {
                db.GENERO.Add(gENERO);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(gENERO);
        }

        // GET: Genero/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GENERO gENERO = await db.GENERO.FindAsync(id);
            if (gENERO == null)
            {
                return HttpNotFound();
            }
            return View(gENERO);
        }

        // POST: Genero/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDGENERO,DESCRIPCIONGENERO")] GENERO gENERO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gENERO).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(gENERO);
        }

        // GET: Genero/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GENERO gENERO = await db.GENERO.FindAsync(id);
            if (gENERO == null)
            {
                return HttpNotFound();
            }
            return View(gENERO);
        }

        // POST: Genero/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            GENERO gENERO = await db.GENERO.FindAsync(id);
            db.GENERO.Remove(gENERO);
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
