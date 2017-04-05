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
    public class NivelIdiomaController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: NivelIdioma
        public async Task<ActionResult> Index()
        {
            return View(await db.NIVEL_IDIOMA.ToListAsync());
        }

        // GET: NivelIdioma/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NIVEL_IDIOMA nIVEL_IDIOMA = await db.NIVEL_IDIOMA.FindAsync(id);
            if (nIVEL_IDIOMA == null)
            {
                return HttpNotFound();
            }
            return View(nIVEL_IDIOMA);
        }

        // GET: NivelIdioma/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NivelIdioma/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDNIVELIDIOMA,NOMBRENIVELIDIOMA,CODIGONIVELIDIOMA")] NIVEL_IDIOMA nIVEL_IDIOMA)
        {
            if (ModelState.IsValid)
            {
                db.NIVEL_IDIOMA.Add(nIVEL_IDIOMA);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(nIVEL_IDIOMA);
        }

        // GET: NivelIdioma/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NIVEL_IDIOMA nIVEL_IDIOMA = await db.NIVEL_IDIOMA.FindAsync(id);
            if (nIVEL_IDIOMA == null)
            {
                return HttpNotFound();
            }
            return View(nIVEL_IDIOMA);
        }

        // POST: NivelIdioma/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDNIVELIDIOMA,NOMBRENIVELIDIOMA,CODIGONIVELIDIOMA")] NIVEL_IDIOMA nIVEL_IDIOMA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nIVEL_IDIOMA).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(nIVEL_IDIOMA);
        }

        // GET: NivelIdioma/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NIVEL_IDIOMA nIVEL_IDIOMA = await db.NIVEL_IDIOMA.FindAsync(id);
            if (nIVEL_IDIOMA == null)
            {
                return HttpNotFound();
            }
            return View(nIVEL_IDIOMA);
        }

        // POST: NivelIdioma/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            NIVEL_IDIOMA nIVEL_IDIOMA = await db.NIVEL_IDIOMA.FindAsync(id);
            db.NIVEL_IDIOMA.Remove(nIVEL_IDIOMA);
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
