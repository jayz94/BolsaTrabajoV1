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
    public class PreguntaController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: Pregunta
        public async Task<ActionResult> Index()
        {
            var pREGUNTA = db.PREGUNTA.Include(p => p.EXAMEN).Include(p => p.TIPOPREGUNTA);
            return View(await pREGUNTA.ToListAsync());
        }

        // GET: Pregunta/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PREGUNTA pREGUNTA = await db.PREGUNTA.FindAsync(id);
            if (pREGUNTA == null)
            {
                return HttpNotFound();
            }
            ViewBag.opciones = from op in db.OPCION_PREGUNTA where op.IDPREGUNTA == id select op;
            return View(pREGUNTA);
        }

        // GET: Pregunta/Create
        public ActionResult Create()
        {
            ViewBag.CODIGOEXAMEN = new SelectList(db.EXAMEN, "CODIGOEXAMEN", "CODIGOEXAMEN");
            ViewBag.IDTIPOOPCION = new SelectList(db.TIPOPREGUNTA, "IDTIPOPREGUNTA", "NOMBRETIPOPREGUNTA");
            return View();
        }

        // POST: Pregunta/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CODIGOEXAMEN,TEXTOPREGUNTA,IDPREGUNTA,IDTIPOOPCION")] PREGUNTA pREGUNTA)
        {
            if (ModelState.IsValid)
            {
                db.PREGUNTA.Add(pREGUNTA);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CODIGOEXAMEN = new SelectList(db.EXAMEN, "CODIGOEXAMEN", "CODIGOEXAMEN", pREGUNTA.CODIGOEXAMEN);
            ViewBag.IDTIPOOPCION = new SelectList(db.TIPOPREGUNTA, "IDTIPOPREGUNTA", "NOMBRETIPOPREGUNTA", pREGUNTA.IDTIPOOPCION);
            return View(pREGUNTA);
        }

        //POST: Pregunta/GuardarPregunta
        [HttpPost]
        public async Task<ActionResult> GuardarPregunta(int CODIGOEXAMEN, String textoPregunta, short TIPOPREGUNTA)
        {
            PREGUNTA pregunta = new PREGUNTA();
            pregunta.CODIGOEXAMEN = CODIGOEXAMEN;
            pregunta.TEXTOPREGUNTA = textoPregunta;
            pregunta.IDTIPOOPCION = TIPOPREGUNTA;
            db.PREGUNTA.Add(pregunta);
            await db.SaveChangesAsync();
            return RedirectToAction("Details", "Examen", new { @id = CODIGOEXAMEN });
        }

        // GET: Pregunta/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PREGUNTA pREGUNTA = await db.PREGUNTA.FindAsync(id);
            if (pREGUNTA == null)
            {
                return HttpNotFound();
            }
            ViewBag.CODIGOEXAMEN = new SelectList(db.EXAMEN, "CODIGOEXAMEN", "CODIGOEXAMEN", pREGUNTA.CODIGOEXAMEN);
            ViewBag.IDTIPOOPCION = new SelectList(db.TIPOPREGUNTA, "IDTIPOPREGUNTA", "NOMBRETIPOPREGUNTA", pREGUNTA.IDTIPOOPCION);
            return View(pREGUNTA);
        }

        // POST: Pregunta/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CODIGOEXAMEN,TEXTOPREGUNTA,IDPREGUNTA,IDTIPOOPCION")] PREGUNTA pREGUNTA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pREGUNTA).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CODIGOEXAMEN = new SelectList(db.EXAMEN, "CODIGOEXAMEN", "CODIGOEXAMEN", pREGUNTA.CODIGOEXAMEN);
            ViewBag.IDTIPOOPCION = new SelectList(db.TIPOPREGUNTA, "IDTIPOPREGUNTA", "NOMBRETIPOPREGUNTA", pREGUNTA.IDTIPOOPCION);
            return View(pREGUNTA);
        }

        // GET: Pregunta/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PREGUNTA pREGUNTA = await db.PREGUNTA.FindAsync(id);
            if (pREGUNTA == null)
            {
                return HttpNotFound();
            }
            return View(pREGUNTA);
        }

        // POST: Pregunta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PREGUNTA pREGUNTA = await db.PREGUNTA.FindAsync(id);
            db.PREGUNTA.Remove(pREGUNTA);
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
