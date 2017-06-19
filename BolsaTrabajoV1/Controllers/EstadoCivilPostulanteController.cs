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
    public class EstadoCivilPostulanteController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: EstadoCivilPostulante
        public async Task<ActionResult> Index(int? id)
        {
            var eSTADO_CIVIL_POSTULANTE_MM = db.ESTADO_CIVIL_POSTULANTE_MM.Include(e => e.ESTADO_CIVIL).Include(e => e.POSTULANTE).Where(e=> e.IDPOSTULANTE == id);
            return View(await eSTADO_CIVIL_POSTULANTE_MM.ToListAsync());
        }

        // GET: EstadoCivilPostulante/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ESTADO_CIVIL_POSTULANTE_MM eSTADO_CIVIL_POSTULANTE_MM = await db.ESTADO_CIVIL_POSTULANTE_MM.FindAsync(id);
            if (eSTADO_CIVIL_POSTULANTE_MM == null)
            {
                return HttpNotFound();
            }
            return View(eSTADO_CIVIL_POSTULANTE_MM);
        }

        // GET: EstadoCivilPostulante/Create
        public ActionResult Create()
        {
            ViewBag.IDESTADOCIVIL = new SelectList(db.ESTADO_CIVIL, "IDESTADOCIVIL", "NOMBREESTADOCIVIL");
            ViewBag.IDPOSTULANTE = new SelectList(db.POSTULANTE, "IDPOSTULANTE", "NOMBREPOSTULANTE");
            return View();
        }

        // POST: EstadoCivilPostulante/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDPOSTULANTE,IDESTADOCIVIL,FECHACAMBIO,ACTIVO")] ESTADO_CIVIL_POSTULANTE_MM eSTADO_CIVIL_POSTULANTE_MM)
        {
            if (ModelState.IsValid)
            {
                db.ESTADO_CIVIL_POSTULANTE_MM.Add(eSTADO_CIVIL_POSTULANTE_MM);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IDESTADOCIVIL = new SelectList(db.ESTADO_CIVIL, "IDESTADOCIVIL", "NOMBREESTADOCIVIL", eSTADO_CIVIL_POSTULANTE_MM.IDESTADOCIVIL);
            ViewBag.IDPOSTULANTE = new SelectList(db.POSTULANTE, "IDPOSTULANTE", "NOMBREPOSTULANTE", eSTADO_CIVIL_POSTULANTE_MM.IDPOSTULANTE);
            return View(eSTADO_CIVIL_POSTULANTE_MM);
        }

        // GET: EstadoCivilPostulante/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ESTADO_CIVIL_POSTULANTE_MM eSTADO_CIVIL_POSTULANTE_MM = await db.ESTADO_CIVIL_POSTULANTE_MM.FindAsync(id);
            if (eSTADO_CIVIL_POSTULANTE_MM == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDESTADOCIVIL = new SelectList(db.ESTADO_CIVIL, "IDESTADOCIVIL", "NOMBREESTADOCIVIL", eSTADO_CIVIL_POSTULANTE_MM.IDESTADOCIVIL);
            ViewBag.IDPOSTULANTE = new SelectList(db.POSTULANTE, "IDPOSTULANTE", "NOMBREPOSTULANTE", eSTADO_CIVIL_POSTULANTE_MM.IDPOSTULANTE);
            return View(eSTADO_CIVIL_POSTULANTE_MM);
        }

        // POST: EstadoCivilPostulante/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDPOSTULANTE,IDESTADOCIVIL,FECHACAMBIO,ACTIVO")] ESTADO_CIVIL_POSTULANTE_MM eSTADO_CIVIL_POSTULANTE_MM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eSTADO_CIVIL_POSTULANTE_MM).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IDESTADOCIVIL = new SelectList(db.ESTADO_CIVIL, "IDESTADOCIVIL", "NOMBREESTADOCIVIL", eSTADO_CIVIL_POSTULANTE_MM.IDESTADOCIVIL);
            ViewBag.IDPOSTULANTE = new SelectList(db.POSTULANTE, "IDPOSTULANTE", "NOMBREPOSTULANTE", eSTADO_CIVIL_POSTULANTE_MM.IDPOSTULANTE);
            return View(eSTADO_CIVIL_POSTULANTE_MM);
        }

        // GET: EstadoCivilPostulante/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ESTADO_CIVIL_POSTULANTE_MM eSTADO_CIVIL_POSTULANTE_MM = await db.ESTADO_CIVIL_POSTULANTE_MM.FindAsync(id);
            if (eSTADO_CIVIL_POSTULANTE_MM == null)
            {
                return HttpNotFound();
            }
            return View(eSTADO_CIVIL_POSTULANTE_MM);
        }

        // POST: EstadoCivilPostulante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ESTADO_CIVIL_POSTULANTE_MM eSTADO_CIVIL_POSTULANTE_MM = await db.ESTADO_CIVIL_POSTULANTE_MM.FindAsync(id);
            db.ESTADO_CIVIL_POSTULANTE_MM.Remove(eSTADO_CIVIL_POSTULANTE_MM);
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
