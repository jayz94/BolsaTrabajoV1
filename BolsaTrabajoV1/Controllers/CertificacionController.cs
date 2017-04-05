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
    public class CertificacionController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: Certificacion
        public async Task<ActionResult> Index()
        {
            var cERTIFICACION = db.CERTIFICACION.Include(c => c.AREA_CONOCIMIENTO).Include(c => c.CURRICULUM);
            return View(await cERTIFICACION.ToListAsync());
        }

        // GET: Certificacion/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CERTIFICACION cERTIFICACION = await db.CERTIFICACION.FindAsync(id);
            if (cERTIFICACION == null)
            {
                return HttpNotFound();
            }
            return View(cERTIFICACION);
        }

        // GET: Certificacion/Create
        public ActionResult Create()
        {
            ViewBag.IDAREACONOCIMIENTO = new SelectList(db.AREA_CONOCIMIENTO, "IDAREACONOCIMIENTO", "NOMBREAREACONOCIMIENTO");
            ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM");
            return View();
        }

        // POST: Certificacion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDCERTIFICACION,IDCURRICULUM,IDPOSTULANTE,IDAREACONOCIMIENTO,DESCRIPCIONCERTIFICACION,TITULOCERTIFICACION,FECHAINICIOCERTIFICACION,FECHAFINCERTIFICACION,CODIGOCERTIFICACION,INSTITUCION")] CERTIFICACION cERTIFICACION)
        {
            if (ModelState.IsValid)
            {
                db.CERTIFICACION.Add(cERTIFICACION);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IDAREACONOCIMIENTO = new SelectList(db.AREA_CONOCIMIENTO, "IDAREACONOCIMIENTO", "NOMBREAREACONOCIMIENTO", cERTIFICACION.IDAREACONOCIMIENTO);
            ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM", cERTIFICACION.IDCURRICULUM);
            return View(cERTIFICACION);
        }

        // GET: Certificacion/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CERTIFICACION cERTIFICACION = await db.CERTIFICACION.FindAsync(id);
            if (cERTIFICACION == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDAREACONOCIMIENTO = new SelectList(db.AREA_CONOCIMIENTO, "IDAREACONOCIMIENTO", "NOMBREAREACONOCIMIENTO", cERTIFICACION.IDAREACONOCIMIENTO);
            ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM", cERTIFICACION.IDCURRICULUM);
            return View(cERTIFICACION);
        }

        // POST: Certificacion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDCERTIFICACION,IDCURRICULUM,IDPOSTULANTE,IDAREACONOCIMIENTO,DESCRIPCIONCERTIFICACION,TITULOCERTIFICACION,FECHAINICIOCERTIFICACION,FECHAFINCERTIFICACION,CODIGOCERTIFICACION,INSTITUCION")] CERTIFICACION cERTIFICACION)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cERTIFICACION).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IDAREACONOCIMIENTO = new SelectList(db.AREA_CONOCIMIENTO, "IDAREACONOCIMIENTO", "NOMBREAREACONOCIMIENTO", cERTIFICACION.IDAREACONOCIMIENTO);
            ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM", cERTIFICACION.IDCURRICULUM);
            return View(cERTIFICACION);
        }

        // GET: Certificacion/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CERTIFICACION cERTIFICACION = await db.CERTIFICACION.FindAsync(id);
            if (cERTIFICACION == null)
            {
                return HttpNotFound();
            }
            return View(cERTIFICACION);
        }

        // POST: Certificacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CERTIFICACION cERTIFICACION = await db.CERTIFICACION.FindAsync(id);
            db.CERTIFICACION.Remove(cERTIFICACION);
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
