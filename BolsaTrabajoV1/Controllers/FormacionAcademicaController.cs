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
    public class FormacionAcademicaController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: FormacionAcademica
        public async Task<ActionResult> Index()
        {
            // var fORMACIONACADEMICA = db.FORMACIONACADEMICA.Include(f => f.CURRICULUM).Include(f => f.TIPOFORMACINONACADEMICA);
            int idCurriculum = (int)Session["idCurriculum"];
            var formacion = from form in db.FORMACIONACADEMICA where form.IDCURRICULUM == idCurriculum select form;
            return View(await formacion.ToListAsync());
        }

        // GET: FormacionAcademica/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FORMACIONACADEMICA fORMACIONACADEMICA = await db.FORMACIONACADEMICA.FindAsync(id);
            if (fORMACIONACADEMICA == null)
            {
                return HttpNotFound();
            }
            return View(fORMACIONACADEMICA);
        }

        // GET: FormacionAcademica/Create
        public ActionResult Create()
        {
           // ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM");
            ViewBag.IDTIPOFORMACION = new SelectList(db.TIPOFORMACINONACADEMICA, "IDTIPOFORMACION", "NOMBRETIPOFORMACION");
            return View();
        }

        // POST: FormacionAcademica/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDTIPOFORMACION,INSTITUCION,TITULO,FECHAINICIOFORMACION,FECHAFINFORMACION,FORMAL,GRADOACADEMICO")] FORMACIONACADEMICA fORMACIONACADEMICA)
        {
            if (ModelState.IsValid)
            {
                fORMACIONACADEMICA.IDCURRICULUM = (int)Session["idCurriculum"];
                fORMACIONACADEMICA.IDPOSTULANTE = (int)Session["idPostulante"];
                db.FORMACIONACADEMICA.Add(fORMACIONACADEMICA);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            //ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM", fORMACIONACADEMICA.IDCURRICULUM);
            ViewBag.IDTIPOFORMACION = new SelectList(db.TIPOFORMACINONACADEMICA, "IDTIPOFORMACION", "NOMBRETIPOFORMACION", fORMACIONACADEMICA.IDTIPOFORMACION);
            return View(fORMACIONACADEMICA);
        }

        // GET: FormacionAcademica/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FORMACIONACADEMICA fORMACIONACADEMICA = await db.FORMACIONACADEMICA.FindAsync(id);
            if (fORMACIONACADEMICA == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM", fORMACIONACADEMICA.IDCURRICULUM);
            ViewBag.IDTIPOFORMACION = new SelectList(db.TIPOFORMACINONACADEMICA, "IDTIPOFORMACION", "NOMBRETIPOFORMACION", fORMACIONACADEMICA.IDTIPOFORMACION);
            return View(fORMACIONACADEMICA);
        }

        // POST: FormacionAcademica/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDFORMACION,IDCURRICULUM,IDPOSTULANTE,IDTIPOFORMACION,INSTITUCION,TITULO,FECHAINICIOFORMACION,FECHAFINFORMACION,FORMAL,GRADOACADEMICO")] FORMACIONACADEMICA fORMACIONACADEMICA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fORMACIONACADEMICA).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM", fORMACIONACADEMICA.IDCURRICULUM);
            ViewBag.IDTIPOFORMACION = new SelectList(db.TIPOFORMACINONACADEMICA, "IDTIPOFORMACION", "NOMBRETIPOFORMACION", fORMACIONACADEMICA.IDTIPOFORMACION);
            return View(fORMACIONACADEMICA);
        }

        // GET: FormacionAcademica/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FORMACIONACADEMICA fORMACIONACADEMICA = await db.FORMACIONACADEMICA.FindAsync(id);
            if (fORMACIONACADEMICA == null)
            {
                return HttpNotFound();
            }
            return View(fORMACIONACADEMICA);
        }

        // POST: FormacionAcademica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            FORMACIONACADEMICA fORMACIONACADEMICA = await db.FORMACIONACADEMICA.FindAsync(id);
            db.FORMACIONACADEMICA.Remove(fORMACIONACADEMICA);
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
