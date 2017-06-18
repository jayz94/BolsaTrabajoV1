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
    public class PostulanteTipoDocumentoController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: PostulanteTipoDocumento
        public async Task<ActionResult> Index()
        {
            var pOSTULANTE_TIPO_DOCUMENTO_MM = db.POSTULANTE_TIPO_DOCUMENTO_MM.Include(p => p.POSTULANTE).Include(p => p.TIPO_DOCUMENTO_IDENTIDAD);
            return View(await pOSTULANTE_TIPO_DOCUMENTO_MM.ToListAsync());
        }

        // GET: PostulanteTipoDocumento/Details/5
        public async Task<ActionResult> Details(int? id,int? idPostulante)
        {
            if (id == null || idPostulante == null )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POSTULANTE_TIPO_DOCUMENTO_MM pOSTULANTE_TIPO_DOCUMENTO_MM = await db.POSTULANTE_TIPO_DOCUMENTO_MM.FindAsync( id , idPostulante);
            if (pOSTULANTE_TIPO_DOCUMENTO_MM == null)
            {
                return HttpNotFound();
            }
            return View(pOSTULANTE_TIPO_DOCUMENTO_MM);
        }

        // GET: PostulanteTipoDocumento/Create
        public ActionResult Create(int? idPostulante)
        {
            if (idPostulante == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.IDPOSTULANTE = idPostulante;// new SelectList(db.POSTULANTE, "IDPOSTULANTE", "NOMBREPOSTULANTE");
            ViewBag.IDTIPODOCUMENTOIDENTIDAD = new SelectList(db.TIPO_DOCUMENTO_IDENTIDAD, "IDTIPODOCUMENTOIDENTIDAD", "NOMBREDOCUMENTOIDENTIDAD");
            return View();
        }

        // POST: PostulanteTipoDocumento/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDTIPODOCUMENTOIDENTIDAD,IDPOSTULANTE,NUMERO")] POSTULANTE_TIPO_DOCUMENTO_MM pOSTULANTE_TIPO_DOCUMENTO_MM)
        {
            if (ModelState.IsValid)
            {
                db.POSTULANTE_TIPO_DOCUMENTO_MM.Add(pOSTULANTE_TIPO_DOCUMENTO_MM);
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "Postulante", new { id = pOSTULANTE_TIPO_DOCUMENTO_MM.IDPOSTULANTE });
            }

            ViewBag.IDPOSTULANTE = new SelectList(db.POSTULANTE, "IDPOSTULANTE", "NOMBREPOSTULANTE", pOSTULANTE_TIPO_DOCUMENTO_MM.IDPOSTULANTE);
            ViewBag.IDTIPODOCUMENTOIDENTIDAD = new SelectList(db.TIPO_DOCUMENTO_IDENTIDAD, "IDTIPODOCUMENTOIDENTIDAD", "MASCARA", pOSTULANTE_TIPO_DOCUMENTO_MM.IDTIPODOCUMENTOIDENTIDAD);
            return View(pOSTULANTE_TIPO_DOCUMENTO_MM);
        }

        // GET: PostulanteTipoDocumento/Edit/5
        public async Task<ActionResult> Edit(int? id,int? idPostulante)
        {
            if (id == null||idPostulante==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POSTULANTE_TIPO_DOCUMENTO_MM pOSTULANTE_TIPO_DOCUMENTO_MM = await db.POSTULANTE_TIPO_DOCUMENTO_MM.FindAsync(id,idPostulante);
            if (pOSTULANTE_TIPO_DOCUMENTO_MM == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDPOSTULANTE = idPostulante;// new SelectList(db.POSTULANTE, "IDPOSTULANTE", "NOMBREPOSTULANTE", pOSTULANTE_TIPO_DOCUMENTO_MM.IDPOSTULANTE);
            ViewBag.IDTIPODOCUMENTOIDENTIDAD = new SelectList(db.TIPO_DOCUMENTO_IDENTIDAD, "IDTIPODOCUMENTOIDENTIDAD", "MASCARA", pOSTULANTE_TIPO_DOCUMENTO_MM.IDTIPODOCUMENTOIDENTIDAD);
            return View(pOSTULANTE_TIPO_DOCUMENTO_MM);
        }

        // POST: PostulanteTipoDocumento/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDTIPODOCUMENTOIDENTIDAD,IDPOSTULANTE,NUMERO")] POSTULANTE_TIPO_DOCUMENTO_MM pOSTULANTE_TIPO_DOCUMENTO_MM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pOSTULANTE_TIPO_DOCUMENTO_MM).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "Postulante", new { id = pOSTULANTE_TIPO_DOCUMENTO_MM.IDPOSTULANTE });
            }
            ViewBag.IDPOSTULANTE = new SelectList(db.POSTULANTE, "IDPOSTULANTE", "NOMBREPOSTULANTE", pOSTULANTE_TIPO_DOCUMENTO_MM.IDPOSTULANTE);
            ViewBag.IDTIPODOCUMENTOIDENTIDAD = new SelectList(db.TIPO_DOCUMENTO_IDENTIDAD, "IDTIPODOCUMENTOIDENTIDAD", "MASCARA", pOSTULANTE_TIPO_DOCUMENTO_MM.IDTIPODOCUMENTOIDENTIDAD);
            return View(pOSTULANTE_TIPO_DOCUMENTO_MM);
        }

        // GET: PostulanteTipoDocumento/Delete/5
        public async Task<ActionResult> Delete(int? id,int? idPostulante)
        {
            if (id == null || idPostulante == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POSTULANTE_TIPO_DOCUMENTO_MM pOSTULANTE_TIPO_DOCUMENTO_MM = await db.POSTULANTE_TIPO_DOCUMENTO_MM.FindAsync(id,idPostulante);
            if (pOSTULANTE_TIPO_DOCUMENTO_MM == null)
            {
                return HttpNotFound();
            }
            return View(pOSTULANTE_TIPO_DOCUMENTO_MM);
        }

        // POST: PostulanteTipoDocumento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id, int? idPostulante)
        {
            POSTULANTE_TIPO_DOCUMENTO_MM pOSTULANTE_TIPO_DOCUMENTO_MM = await db.POSTULANTE_TIPO_DOCUMENTO_MM.FindAsync(id, idPostulante);
            db.POSTULANTE_TIPO_DOCUMENTO_MM.Remove(pOSTULANTE_TIPO_DOCUMENTO_MM);
            await db.SaveChangesAsync();
            return RedirectToAction("Details", "Postulante", new { id = pOSTULANTE_TIPO_DOCUMENTO_MM.IDPOSTULANTE });
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
