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
    public class MunicipioController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: Municipio
        public async Task<ActionResult> Index()
        {
            var mUNICIPIO = db.MUNICIPIO.Include(m => m.DEPARTAMENTO);
            return View(await mUNICIPIO.ToListAsync());
        }

        // GET: Municipio/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MUNICIPIO mUNICIPIO = await db.MUNICIPIO.FindAsync(id);
            if (mUNICIPIO == null)
            {
                return HttpNotFound();
            }
            return View(mUNICIPIO);
        }

        // GET: Municipio/Create
        public ActionResult Create()
        {
            ViewBag.IDDEPARTAMENTO = new SelectList(db.DEPARTAMENTO, "IDDEPARTAMENTO", "NOMBREDEPARTAMENTO");
            return View();
        }

        // POST: Municipio/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDMUNICIPIO,IDDEPARTAMENTO,NOMBREMUNICIPIO")] MUNICIPIO mUNICIPIO)
        {
            if (ModelState.IsValid)
            {
                db.MUNICIPIO.Add(mUNICIPIO);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IDDEPARTAMENTO = new SelectList(db.DEPARTAMENTO, "IDDEPARTAMENTO", "NOMBREDEPARTAMENTO", mUNICIPIO.IDDEPARTAMENTO);

           
            return View(mUNICIPIO);
        }

        // GET: Municipio/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MUNICIPIO mUNICIPIO = await db.MUNICIPIO.FindAsync(id);
            if (mUNICIPIO == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDDEPARTAMENTO = new SelectList(db.DEPARTAMENTO, "IDDEPARTAMENTO", "NOMBREDEPARTAMENTO", mUNICIPIO.IDDEPARTAMENTO);
            return View(mUNICIPIO);
        }

        // POST: Municipio/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDMUNICIPIO,IDDEPARTAMENTO,NOMBREMUNICIPIO")] MUNICIPIO mUNICIPIO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mUNICIPIO).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IDDEPARTAMENTO = new SelectList(db.DEPARTAMENTO, "IDDEPARTAMENTO", "NOMBREDEPARTAMENTO", mUNICIPIO.IDDEPARTAMENTO);
            return View(mUNICIPIO);
        }

        // GET: Municipio/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MUNICIPIO mUNICIPIO = await db.MUNICIPIO.FindAsync(id);
            if (mUNICIPIO == null)
            {
                return HttpNotFound();
            }
            return View(mUNICIPIO);
        }

        // POST: Municipio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MUNICIPIO mUNICIPIO = await db.MUNICIPIO.FindAsync(id);
            db.MUNICIPIO.Remove(mUNICIPIO);
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
