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
    public class OpcionPreguntaController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: OpcionPregunta
        public async Task<ActionResult> Index()
        {
            var oPCION_PREGUNTA = db.OPCION_PREGUNTA.Include(o => o.PREGUNTA);
            return View(await oPCION_PREGUNTA.ToListAsync());
        }

        // GET: OpcionPregunta/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OPCION_PREGUNTA oPCION_PREGUNTA = await db.OPCION_PREGUNTA.FindAsync(id);
            if (oPCION_PREGUNTA == null)
            {
                return HttpNotFound();
            }
            return View(oPCION_PREGUNTA);
        }

        // GET: OpcionPregunta/Create
        public ActionResult Create()
        {
            ViewBag.IDPREGUNTA = new SelectList(db.PREGUNTA, "IDPREGUNTA", "TEXTOPREGUNTA");
            return View();
        }

        // POST: OpcionPregunta/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DESCRIPCIONOPCION,PUNTAJE,ORDEN,IDOPCIONPREGUNTA,IDPREGUNTA")] OPCION_PREGUNTA oPCION_PREGUNTA)
        {
            if (ModelState.IsValid)
            {
                db.OPCION_PREGUNTA.Add(oPCION_PREGUNTA);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IDPREGUNTA = new SelectList(db.PREGUNTA, "IDPREGUNTA", "TEXTOPREGUNTA", oPCION_PREGUNTA.IDPREGUNTA);
            return View(oPCION_PREGUNTA);
        }

        //POST: OpcionPregunta/GuardarOpcion
        [HttpPost]
        public async Task<ActionResult> GuardarOpcion(int IDPREGUNTA,String textoOpcion,short orden,short puntaje)
        {
            OPCION_PREGUNTA opcion = new OPCION_PREGUNTA();
            opcion.IDPREGUNTA = IDPREGUNTA;
            opcion.DESCRIPCIONOPCION = textoOpcion;
            opcion.ORDEN = orden;
            opcion.PUNTAJE = puntaje;
            db.OPCION_PREGUNTA.Add(opcion);
            await db.SaveChangesAsync();
            return RedirectToAction("Details", "Pregunta", new { @id = IDPREGUNTA });
        }
        // GET: OpcionPregunta/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OPCION_PREGUNTA oPCION_PREGUNTA = await db.OPCION_PREGUNTA.FindAsync(id);
            if (oPCION_PREGUNTA == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDPREGUNTA = new SelectList(db.PREGUNTA, "IDPREGUNTA", "TEXTOPREGUNTA", oPCION_PREGUNTA.IDPREGUNTA);
            return View(oPCION_PREGUNTA);
        }

        // POST: OpcionPregunta/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DESCRIPCIONOPCION,PUNTAJE,ORDEN,IDOPCIONPREGUNTA,IDPREGUNTA")] OPCION_PREGUNTA oPCION_PREGUNTA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oPCION_PREGUNTA).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IDPREGUNTA = new SelectList(db.PREGUNTA, "IDPREGUNTA", "TEXTOPREGUNTA", oPCION_PREGUNTA.IDPREGUNTA);
            return View(oPCION_PREGUNTA);
        }

        // GET: OpcionPregunta/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OPCION_PREGUNTA oPCION_PREGUNTA = await db.OPCION_PREGUNTA.FindAsync(id);
            if (oPCION_PREGUNTA == null)
            {
                return HttpNotFound();
            }
            return View(oPCION_PREGUNTA);
        }

        // POST: OpcionPregunta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            OPCION_PREGUNTA oPCION_PREGUNTA = await db.OPCION_PREGUNTA.FindAsync(id);
            db.OPCION_PREGUNTA.Remove(oPCION_PREGUNTA);
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
