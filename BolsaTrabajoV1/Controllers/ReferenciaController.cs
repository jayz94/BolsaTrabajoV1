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
    public class ReferenciaController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: Referencia
        public async Task<ActionResult> Index()
        {
            var rEFERENCIA = db.REFERENCIA.Include(r => r.CURRICULUM).Include(r => r.TIPOREFERENCIA);
            return View(await rEFERENCIA.ToListAsync());
        }

        // GET: Referencia/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REFERENCIA rEFERENCIA = await db.REFERENCIA.FindAsync(id);
            if (rEFERENCIA == null)
            {
                return HttpNotFound();
            }
            return View(rEFERENCIA);
        }

        // GET: Referencia/Create
        public ActionResult Create()
        {
            ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM");
            ViewBag.IDTIPOREFERENCIA = new SelectList(db.TIPOREFERENCIA, "IDTIPOREFERENCIA", "NOMBRETIPOREFERENCIA");
            return View();
        }

        // POST: Referencia/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDREFERENCIA,IDTIPOREFERENCIA,IDCURRICULUM,IDPOSTULANTE,DESCRIPCIONREFERENCIA,TELEFONOREFERENCIA,PUESTOREFERENCIA,PERSONAREFERENCIA,EMPRESAORIGENREFERENCIA")] REFERENCIA rEFERENCIA)
        {
            if (ModelState.IsValid)
            {
                db.REFERENCIA.Add(rEFERENCIA);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM", rEFERENCIA.IDCURRICULUM);
            ViewBag.IDTIPOREFERENCIA = new SelectList(db.TIPOREFERENCIA, "IDTIPOREFERENCIA", "NOMBRETIPOREFERENCIA", rEFERENCIA.IDTIPOREFERENCIA);
            return View(rEFERENCIA);
        }

        // GET: Referencia/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REFERENCIA rEFERENCIA = await db.REFERENCIA.FindAsync(id);
            if (rEFERENCIA == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM", rEFERENCIA.IDCURRICULUM);
            ViewBag.IDTIPOREFERENCIA = new SelectList(db.TIPOREFERENCIA, "IDTIPOREFERENCIA", "NOMBRETIPOREFERENCIA", rEFERENCIA.IDTIPOREFERENCIA);
            return View(rEFERENCIA);
        }

        // POST: Referencia/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDREFERENCIA,IDTIPOREFERENCIA,IDCURRICULUM,IDPOSTULANTE,DESCRIPCIONREFERENCIA,TELEFONOREFERENCIA,PUESTOREFERENCIA,PERSONAREFERENCIA,EMPRESAORIGENREFERENCIA")] REFERENCIA rEFERENCIA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rEFERENCIA).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM", rEFERENCIA.IDCURRICULUM);
            ViewBag.IDTIPOREFERENCIA = new SelectList(db.TIPOREFERENCIA, "IDTIPOREFERENCIA", "NOMBRETIPOREFERENCIA", rEFERENCIA.IDTIPOREFERENCIA);
            return View(rEFERENCIA);
        }

        // GET: Referencia/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REFERENCIA rEFERENCIA = await db.REFERENCIA.FindAsync(id);
            if (rEFERENCIA == null)
            {
                return HttpNotFound();
            }
            return View(rEFERENCIA);
        }

        // POST: Referencia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            REFERENCIA rEFERENCIA = await db.REFERENCIA.FindAsync(id);
            db.REFERENCIA.Remove(rEFERENCIA);
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
