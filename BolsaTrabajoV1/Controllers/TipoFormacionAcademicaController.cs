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
    public class TipoFormacionAcademicaController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: TipoFormacionAcademica
        public async Task<ActionResult> Index()
        {
            return View(await db.TIPOFORMACINONACADEMICA.ToListAsync());
        }

        // GET: TipoFormacionAcademica/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPOFORMACINONACADEMICA tIPOFORMACINONACADEMICA = await db.TIPOFORMACINONACADEMICA.FindAsync(id);
            if (tIPOFORMACINONACADEMICA == null)
            {
                return HttpNotFound();
            }
            return View(tIPOFORMACINONACADEMICA);
        }

        // GET: TipoFormacionAcademica/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoFormacionAcademica/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDTIPOFORMACION,NOMBRETIPOFORMACION")] TIPOFORMACINONACADEMICA tIPOFORMACINONACADEMICA)
        {
            if (ModelState.IsValid)
            {
                db.TIPOFORMACINONACADEMICA.Add(tIPOFORMACINONACADEMICA);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tIPOFORMACINONACADEMICA);
        }

        // GET: TipoFormacionAcademica/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPOFORMACINONACADEMICA tIPOFORMACINONACADEMICA = await db.TIPOFORMACINONACADEMICA.FindAsync(id);
            if (tIPOFORMACINONACADEMICA == null)
            {
                return HttpNotFound();
            }
            return View(tIPOFORMACINONACADEMICA);
        }

        // POST: TipoFormacionAcademica/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDTIPOFORMACION,NOMBRETIPOFORMACION")] TIPOFORMACINONACADEMICA tIPOFORMACINONACADEMICA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tIPOFORMACINONACADEMICA).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tIPOFORMACINONACADEMICA);
        }

        // GET: TipoFormacionAcademica/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPOFORMACINONACADEMICA tIPOFORMACINONACADEMICA = await db.TIPOFORMACINONACADEMICA.FindAsync(id);
            if (tIPOFORMACINONACADEMICA == null)
            {
                return HttpNotFound();
            }
            return View(tIPOFORMACINONACADEMICA);
        }

        // POST: TipoFormacionAcademica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TIPOFORMACINONACADEMICA tIPOFORMACINONACADEMICA = await db.TIPOFORMACINONACADEMICA.FindAsync(id);
            db.TIPOFORMACINONACADEMICA.Remove(tIPOFORMACINONACADEMICA);
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
