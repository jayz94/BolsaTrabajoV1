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
    public class ParticipacionProfesionalController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: ParticipacionProfesional
        public async Task<ActionResult> Index()
        {
            var pARTICIPACION_PROFESIONAL = db.PARTICIPACION_PROFESIONAL.Include(p => p.CURRICULUM).Include(p => p.PAIS).Include(p => p.TIPO_PARTICIPACION);
            return View(await pARTICIPACION_PROFESIONAL.ToListAsync());
        }

        // GET: ParticipacionProfesional/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PARTICIPACION_PROFESIONAL pARTICIPACION_PROFESIONAL = await db.PARTICIPACION_PROFESIONAL.FindAsync(id);
            if (pARTICIPACION_PROFESIONAL == null)
            {
                return HttpNotFound();
            }
            return View(pARTICIPACION_PROFESIONAL);
        }

        // GET: ParticipacionProfesional/Create
        public ActionResult Create()
        {
            ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM");
            ViewBag.IDPAIS = new SelectList(db.PAIS, "IDPAIS", "NOMBREPAIS");
            ViewBag.IDTIPOPARTICIPACION = new SelectList(db.TIPO_PARTICIPACION, "IDTIPOPARTICIPACION", "NOMBRETIPOPARTICIPACION");
            return View();
        }

        // POST: ParticipacionProfesional/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDPARTICIPACION,IDCURRICULUM,IDPOSTULANTE,IDTIPOPARTICIPACION,IDPAIS,TITULOPARTICIPACION,LUGARPARTICIPACION,FECHAPARTICIPACION,ANFITRION")] PARTICIPACION_PROFESIONAL pARTICIPACION_PROFESIONAL)
        {
            if (ModelState.IsValid)
            {
                db.PARTICIPACION_PROFESIONAL.Add(pARTICIPACION_PROFESIONAL);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM", pARTICIPACION_PROFESIONAL.IDCURRICULUM);
            ViewBag.IDPAIS = new SelectList(db.PAIS, "IDPAIS", "NOMBREPAIS", pARTICIPACION_PROFESIONAL.IDPAIS);
            ViewBag.IDTIPOPARTICIPACION = new SelectList(db.TIPO_PARTICIPACION, "IDTIPOPARTICIPACION", "NOMBRETIPOPARTICIPACION", pARTICIPACION_PROFESIONAL.IDTIPOPARTICIPACION);
            return View(pARTICIPACION_PROFESIONAL);
        }

        // GET: ParticipacionProfesional/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PARTICIPACION_PROFESIONAL pARTICIPACION_PROFESIONAL = await db.PARTICIPACION_PROFESIONAL.FindAsync(id);
            if (pARTICIPACION_PROFESIONAL == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM", pARTICIPACION_PROFESIONAL.IDCURRICULUM);
            ViewBag.IDPAIS = new SelectList(db.PAIS, "IDPAIS", "NOMBREPAIS", pARTICIPACION_PROFESIONAL.IDPAIS);
            ViewBag.IDTIPOPARTICIPACION = new SelectList(db.TIPO_PARTICIPACION, "IDTIPOPARTICIPACION", "NOMBRETIPOPARTICIPACION", pARTICIPACION_PROFESIONAL.IDTIPOPARTICIPACION);
            return View(pARTICIPACION_PROFESIONAL);
        }

        // POST: ParticipacionProfesional/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDPARTICIPACION,IDCURRICULUM,IDPOSTULANTE,IDTIPOPARTICIPACION,IDPAIS,TITULOPARTICIPACION,LUGARPARTICIPACION,FECHAPARTICIPACION,ANFITRION")] PARTICIPACION_PROFESIONAL pARTICIPACION_PROFESIONAL)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pARTICIPACION_PROFESIONAL).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM", pARTICIPACION_PROFESIONAL.IDCURRICULUM);
            ViewBag.IDPAIS = new SelectList(db.PAIS, "IDPAIS", "NOMBREPAIS", pARTICIPACION_PROFESIONAL.IDPAIS);
            ViewBag.IDTIPOPARTICIPACION = new SelectList(db.TIPO_PARTICIPACION, "IDTIPOPARTICIPACION", "NOMBRETIPOPARTICIPACION", pARTICIPACION_PROFESIONAL.IDTIPOPARTICIPACION);
            return View(pARTICIPACION_PROFESIONAL);
        }

        // GET: ParticipacionProfesional/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PARTICIPACION_PROFESIONAL pARTICIPACION_PROFESIONAL = await db.PARTICIPACION_PROFESIONAL.FindAsync(id);
            if (pARTICIPACION_PROFESIONAL == null)
            {
                return HttpNotFound();
            }
            return View(pARTICIPACION_PROFESIONAL);
        }

        // POST: ParticipacionProfesional/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PARTICIPACION_PROFESIONAL pARTICIPACION_PROFESIONAL = await db.PARTICIPACION_PROFESIONAL.FindAsync(id);
            db.PARTICIPACION_PROFESIONAL.Remove(pARTICIPACION_PROFESIONAL);
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
