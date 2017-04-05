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
    public class TipoContactoController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: TipoContacto
        public async Task<ActionResult> Index()
        {
            return View(await db.TIPO_CONTACTO.ToListAsync());
        }

        // GET: TipoContacto/Details/5
        public async Task<ActionResult> Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPO_CONTACTO tIPO_CONTACTO = await db.TIPO_CONTACTO.FindAsync(id);
            if (tIPO_CONTACTO == null)
            {
                return HttpNotFound();
            }
            return View(tIPO_CONTACTO);
        }

        // GET: TipoContacto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoContacto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDTIPOCONTACTO,NOMBRETIPOCONTACTO")] TIPO_CONTACTO tIPO_CONTACTO)
        {
            if (ModelState.IsValid)
            {
                db.TIPO_CONTACTO.Add(tIPO_CONTACTO);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tIPO_CONTACTO);
        }

        // GET: TipoContacto/Edit/5
        public async Task<ActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPO_CONTACTO tIPO_CONTACTO = await db.TIPO_CONTACTO.FindAsync(id);
            if (tIPO_CONTACTO == null)
            {
                return HttpNotFound();
            }
            return View(tIPO_CONTACTO);
        }

        // POST: TipoContacto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDTIPOCONTACTO,NOMBRETIPOCONTACTO")] TIPO_CONTACTO tIPO_CONTACTO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tIPO_CONTACTO).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tIPO_CONTACTO);
        }

        // GET: TipoContacto/Delete/5
        public async Task<ActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPO_CONTACTO tIPO_CONTACTO = await db.TIPO_CONTACTO.FindAsync(id);
            if (tIPO_CONTACTO == null)
            {
                return HttpNotFound();
            }
            return View(tIPO_CONTACTO);
        }

        // POST: TipoContacto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(short id)
        {
            TIPO_CONTACTO tIPO_CONTACTO = await db.TIPO_CONTACTO.FindAsync(id);
            db.TIPO_CONTACTO.Remove(tIPO_CONTACTO);
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
