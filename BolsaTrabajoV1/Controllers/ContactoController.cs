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
    public class ContactoController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: Contacto
        public async Task<ActionResult> Index()
        {
            var cONTACTO = db.CONTACTO.Include(c => c.POSTULANTE).Include(c => c.TIPO_CONTACTO);
            return View(await cONTACTO.ToListAsync());
        }

        // GET: Contacto/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONTACTO cONTACTO = await db.CONTACTO.FindAsync(id);
            if (cONTACTO == null)
            {
                return HttpNotFound();
            }
            return View(cONTACTO);
        }

        // GET: Contacto/Create
        public ActionResult Create()
        {
            ViewBag.IDPOSTULANTE = new SelectList(db.POSTULANTE, "IDPOSTULANTE", "NOMBREPOSTULANTE");
            ViewBag.IDTIPOCONTACTO = new SelectList(db.TIPO_CONTACTO, "IDTIPOCONTACTO", "NOMBRETIPOCONTACTO");
            return View();
        }

        // POST: Contacto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDCONTACTO,IDPOSTULANTE,IDTIPOCONTACTO,VINCULO")] CONTACTO cONTACTO)
        {
            if (ModelState.IsValid)
            {
                db.CONTACTO.Add(cONTACTO);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IDPOSTULANTE = new SelectList(db.POSTULANTE, "IDPOSTULANTE", "NOMBREPOSTULANTE", cONTACTO.IDPOSTULANTE);
            ViewBag.IDTIPOCONTACTO = new SelectList(db.TIPO_CONTACTO, "IDTIPOCONTACTO", "NOMBRETIPOCONTACTO", cONTACTO.IDTIPOCONTACTO);
            return View(cONTACTO);
        }

        // GET: Contacto/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONTACTO cONTACTO = await db.CONTACTO.FindAsync(id);
            if (cONTACTO == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDPOSTULANTE = new SelectList(db.POSTULANTE, "IDPOSTULANTE", "NOMBREPOSTULANTE", cONTACTO.IDPOSTULANTE);
            ViewBag.IDTIPOCONTACTO = new SelectList(db.TIPO_CONTACTO, "IDTIPOCONTACTO", "NOMBRETIPOCONTACTO", cONTACTO.IDTIPOCONTACTO);
            return View(cONTACTO);
        }

        // POST: Contacto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDCONTACTO,IDPOSTULANTE,IDTIPOCONTACTO,VINCULO")] CONTACTO cONTACTO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cONTACTO).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IDPOSTULANTE = new SelectList(db.POSTULANTE, "IDPOSTULANTE", "NOMBREPOSTULANTE", cONTACTO.IDPOSTULANTE);
            ViewBag.IDTIPOCONTACTO = new SelectList(db.TIPO_CONTACTO, "IDTIPOCONTACTO", "NOMBRETIPOCONTACTO", cONTACTO.IDTIPOCONTACTO);
            return View(cONTACTO);
        }

        // GET: Contacto/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONTACTO cONTACTO = await db.CONTACTO.FindAsync(id);
            if (cONTACTO == null)
            {
                return HttpNotFound();
            }
            return View(cONTACTO);
        }

        // POST: Contacto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CONTACTO cONTACTO = await db.CONTACTO.FindAsync(id);
            db.CONTACTO.Remove(cONTACTO);
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
