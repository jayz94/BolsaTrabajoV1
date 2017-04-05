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
    public class EstadoCivilController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: EstadoCivil
        public async Task<ActionResult> Index()
        {
            return View(await db.ESTADO_CIVIL.ToListAsync());
        }

        // GET: EstadoCivil/Details/5
        public async Task<ActionResult> Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ESTADO_CIVIL eSTADO_CIVIL = await db.ESTADO_CIVIL.FindAsync(id);
            if (eSTADO_CIVIL == null)
            {
                return HttpNotFound();
            }
            return View(eSTADO_CIVIL);
        }

        // GET: EstadoCivil/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstadoCivil/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDESTADOCIVIL,NOMBREESTADOCIVIL")] ESTADO_CIVIL eSTADO_CIVIL)
        {
            if (ModelState.IsValid)
            {
                db.ESTADO_CIVIL.Add(eSTADO_CIVIL);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(eSTADO_CIVIL);
        }

        // GET: EstadoCivil/Edit/5
        public async Task<ActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ESTADO_CIVIL eSTADO_CIVIL = await db.ESTADO_CIVIL.FindAsync(id);
            if (eSTADO_CIVIL == null)
            {
                return HttpNotFound();
            }
            return View(eSTADO_CIVIL);
        }

        // POST: EstadoCivil/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDESTADOCIVIL,NOMBREESTADOCIVIL")] ESTADO_CIVIL eSTADO_CIVIL)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eSTADO_CIVIL).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(eSTADO_CIVIL);
        }

        // GET: EstadoCivil/Delete/5
        public async Task<ActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ESTADO_CIVIL eSTADO_CIVIL = await db.ESTADO_CIVIL.FindAsync(id);
            if (eSTADO_CIVIL == null)
            {
                return HttpNotFound();
            }
            return View(eSTADO_CIVIL);
        }

        // POST: EstadoCivil/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(short id)
        {
            ESTADO_CIVIL eSTADO_CIVIL = await db.ESTADO_CIVIL.FindAsync(id);
            db.ESTADO_CIVIL.Remove(eSTADO_CIVIL);
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
