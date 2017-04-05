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
    public class TelefonoController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: Telefono
        public async Task<ActionResult> Index()
        {
            var tELEFONO = db.TELEFONO.Include(t => t.POSTULANTE).Include(t => t.TIPO_TELEFONO);
            return View(await tELEFONO.ToListAsync());
        }

        // GET: Telefono/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TELEFONO tELEFONO = await db.TELEFONO.FindAsync(id);
            if (tELEFONO == null)
            {
                return HttpNotFound();
            }
            return View(tELEFONO);
        }

        // GET: Telefono/Create
        public ActionResult Create()
        {
            ViewBag.IDPOSTULANTE = new SelectList(db.POSTULANTE, "IDPOSTULANTE", "NOMBREPOSTULANTE");
            ViewBag.IDTIPOTELEFONO = new SelectList(db.TIPO_TELEFONO, "IDTIPOTELEFONO", "NOMBRETIPOTELEFONO");
            return View();
        }

        // POST: Telefono/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDTELEFONO,IDPOSTULANTE,IDTIPOTELEFONO,NUMEROTELEFONO")] TELEFONO tELEFONO)
        {
            if (ModelState.IsValid)
            {
                db.TELEFONO.Add(tELEFONO);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IDPOSTULANTE = new SelectList(db.POSTULANTE, "IDPOSTULANTE", "NOMBREPOSTULANTE", tELEFONO.IDPOSTULANTE);
            ViewBag.IDTIPOTELEFONO = new SelectList(db.TIPO_TELEFONO, "IDTIPOTELEFONO", "NOMBRETIPOTELEFONO", tELEFONO.IDTIPOTELEFONO);
            return View(tELEFONO);
        }

        // GET: Telefono/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TELEFONO tELEFONO = await db.TELEFONO.FindAsync(id);
            if (tELEFONO == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDPOSTULANTE = new SelectList(db.POSTULANTE, "IDPOSTULANTE", "NOMBREPOSTULANTE", tELEFONO.IDPOSTULANTE);
            ViewBag.IDTIPOTELEFONO = new SelectList(db.TIPO_TELEFONO, "IDTIPOTELEFONO", "NOMBRETIPOTELEFONO", tELEFONO.IDTIPOTELEFONO);
            return View(tELEFONO);
        }

        // POST: Telefono/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDTELEFONO,IDPOSTULANTE,IDTIPOTELEFONO,NUMEROTELEFONO")] TELEFONO tELEFONO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tELEFONO).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IDPOSTULANTE = new SelectList(db.POSTULANTE, "IDPOSTULANTE", "NOMBREPOSTULANTE", tELEFONO.IDPOSTULANTE);
            ViewBag.IDTIPOTELEFONO = new SelectList(db.TIPO_TELEFONO, "IDTIPOTELEFONO", "NOMBRETIPOTELEFONO", tELEFONO.IDTIPOTELEFONO);
            return View(tELEFONO);
        }

        // GET: Telefono/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TELEFONO tELEFONO = await db.TELEFONO.FindAsync(id);
            if (tELEFONO == null)
            {
                return HttpNotFound();
            }
            return View(tELEFONO);
        }

        // POST: Telefono/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TELEFONO tELEFONO = await db.TELEFONO.FindAsync(id);
            db.TELEFONO.Remove(tELEFONO);
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
