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
    public class TipoDocumentoIdentidadController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: TipoDocumentoIdentidad
        public async Task<ActionResult> Index()
        {
            return View(await db.TIPO_DOCUMENTO_IDENTIDAD.ToListAsync());
        }

        // GET: TipoDocumentoIdentidad/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPO_DOCUMENTO_IDENTIDAD tIPO_DOCUMENTO_IDENTIDAD = await db.TIPO_DOCUMENTO_IDENTIDAD.FindAsync(id);
            if (tIPO_DOCUMENTO_IDENTIDAD == null)
            {
                return HttpNotFound();
            }
            return View(tIPO_DOCUMENTO_IDENTIDAD);
        }

        // GET: TipoDocumentoIdentidad/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoDocumentoIdentidad/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDTIPODOCUMENTOIDENTIDAD,NOMBREDOCUMENTOIDENTIDAD,CANTIDADCARACTERES,MASCARA,EXTRANJERO")] TIPO_DOCUMENTO_IDENTIDAD tIPO_DOCUMENTO_IDENTIDAD)
        {
            if (ModelState.IsValid)
            {
                db.TIPO_DOCUMENTO_IDENTIDAD.Add(tIPO_DOCUMENTO_IDENTIDAD);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tIPO_DOCUMENTO_IDENTIDAD);
        }

        // GET: TipoDocumentoIdentidad/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPO_DOCUMENTO_IDENTIDAD tIPO_DOCUMENTO_IDENTIDAD = await db.TIPO_DOCUMENTO_IDENTIDAD.FindAsync(id);
            if (tIPO_DOCUMENTO_IDENTIDAD == null)
            {
                return HttpNotFound();
            }
            return View(tIPO_DOCUMENTO_IDENTIDAD);
        }

        // POST: TipoDocumentoIdentidad/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDTIPODOCUMENTOIDENTIDAD,NOMBREDOCUMENTOIDENTIDAD,CANTIDADCARACTERES,MASCARA,EXTRANJERO")] TIPO_DOCUMENTO_IDENTIDAD tIPO_DOCUMENTO_IDENTIDAD)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tIPO_DOCUMENTO_IDENTIDAD).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tIPO_DOCUMENTO_IDENTIDAD);
        }

        // GET: TipoDocumentoIdentidad/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPO_DOCUMENTO_IDENTIDAD tIPO_DOCUMENTO_IDENTIDAD = await db.TIPO_DOCUMENTO_IDENTIDAD.FindAsync(id);
            if (tIPO_DOCUMENTO_IDENTIDAD == null)
            {
                return HttpNotFound();
            }
            return View(tIPO_DOCUMENTO_IDENTIDAD);
        }

        // POST: TipoDocumentoIdentidad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TIPO_DOCUMENTO_IDENTIDAD tIPO_DOCUMENTO_IDENTIDAD = await db.TIPO_DOCUMENTO_IDENTIDAD.FindAsync(id);
            db.TIPO_DOCUMENTO_IDENTIDAD.Remove(tIPO_DOCUMENTO_IDENTIDAD);
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
