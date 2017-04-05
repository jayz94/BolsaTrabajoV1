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
    public class TipoTelefonoController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: TipoTelefono
        public async Task<ActionResult> Index()
        {
            return View(await db.TIPO_TELEFONO.ToListAsync());
        }

        // GET: TipoTelefono/Details/5
        public async Task<ActionResult> Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPO_TELEFONO tIPO_TELEFONO = await db.TIPO_TELEFONO.FindAsync(id);
            if (tIPO_TELEFONO == null)
            {
                return HttpNotFound();
            }
            return View(tIPO_TELEFONO);
        }

        // GET: TipoTelefono/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoTelefono/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDTIPOTELEFONO,NOMBRETIPOTELEFONO")] TIPO_TELEFONO tIPO_TELEFONO)
        {
            if (ModelState.IsValid)
            {
                db.TIPO_TELEFONO.Add(tIPO_TELEFONO);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tIPO_TELEFONO);
        }

        // GET: TipoTelefono/Edit/5
        public async Task<ActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPO_TELEFONO tIPO_TELEFONO = await db.TIPO_TELEFONO.FindAsync(id);
            if (tIPO_TELEFONO == null)
            {
                return HttpNotFound();
            }
            return View(tIPO_TELEFONO);
        }

        // POST: TipoTelefono/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDTIPOTELEFONO,NOMBRETIPOTELEFONO")] TIPO_TELEFONO tIPO_TELEFONO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tIPO_TELEFONO).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tIPO_TELEFONO);
        }

        // GET: TipoTelefono/Delete/5
        public async Task<ActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPO_TELEFONO tIPO_TELEFONO = await db.TIPO_TELEFONO.FindAsync(id);
            if (tIPO_TELEFONO == null)
            {
                return HttpNotFound();
            }
            return View(tIPO_TELEFONO);
        }

        // POST: TipoTelefono/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(short id)
        {
            TIPO_TELEFONO tIPO_TELEFONO = await db.TIPO_TELEFONO.FindAsync(id);
            db.TIPO_TELEFONO.Remove(tIPO_TELEFONO);
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
