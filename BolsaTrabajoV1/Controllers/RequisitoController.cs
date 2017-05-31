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
    public class RequisitoController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: Requisito
        public async Task<ActionResult> Index()
        {
            var rEQUISITO = db.REQUISITO.Include(r => r.PLAZA).Include(r => r.TIPO_REQUISITO);
            return View(await rEQUISITO.ToListAsync());
        }

        // GET: Requisito/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REQUISITO rEQUISITO = await db.REQUISITO.FindAsync(id);
            if (rEQUISITO == null)
            {
                return HttpNotFound();
            }
            return View(rEQUISITO);
        }

        // GET: Requisito/Create
        public ActionResult Create()
        {
            ViewBag.IDPLAZA = new SelectList(db.PLAZA, "IDPLAZA", "FORMAPAGO");
            ViewBag.IDTIPOREQUISITO = new SelectList(db.TIPO_REQUISITO, "IDTIPOREQUISITO", "DESCRIPCIONTIPO");
            return View();
        }

        // POST: Requisito/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDTIPOREQUISITO,DESCRIPCIONREQUISITO,IDREQUISITO,IDPLAZA")] REQUISITO rEQUISITO)
        {
            if (ModelState.IsValid)
            {
                db.REQUISITO.Add(rEQUISITO);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IDPLAZA = new SelectList(db.PLAZA, "IDPLAZA", "FORMAPAGO", rEQUISITO.IDPLAZA);
            ViewBag.IDTIPOREQUISITO = new SelectList(db.TIPO_REQUISITO, "IDTIPOREQUISITO", "DESCRIPCIONTIPO", rEQUISITO.IDTIPOREQUISITO);
            return View(rEQUISITO);
        }

        //POST: Requisito/GuardarRequisito
        [HttpPost]
        public async Task<ActionResult>  GuardarRequisito(int IDPLAZA, int TIPOREQUISITO, String desc)
        {
            REQUISITO req = new REQUISITO();
            req.IDPLAZA = IDPLAZA;
            req.IDTIPOREQUISITO = TIPOREQUISITO;
            req.DESCRIPCIONREQUISITO = desc;
            db.REQUISITO.Add(req);
            await db.SaveChangesAsync();
            return RedirectToAction("Details", "Plaza", new { @id = IDPLAZA });
        }

        // GET: Requisito/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REQUISITO rEQUISITO = await db.REQUISITO.FindAsync(id);
            if (rEQUISITO == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDPLAZA = new SelectList(db.PLAZA, "IDPLAZA", "FORMAPAGO", rEQUISITO.IDPLAZA);
            ViewBag.IDTIPOREQUISITO = new SelectList(db.TIPO_REQUISITO, "IDTIPOREQUISITO", "DESCRIPCIONTIPO", rEQUISITO.IDTIPOREQUISITO);
            return View(rEQUISITO);
        }

        // POST: Requisito/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDTIPOREQUISITO,DESCRIPCIONREQUISITO,IDREQUISITO,IDPLAZA")] REQUISITO rEQUISITO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rEQUISITO).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IDPLAZA = new SelectList(db.PLAZA, "IDPLAZA", "FORMAPAGO", rEQUISITO.IDPLAZA);
            ViewBag.IDTIPOREQUISITO = new SelectList(db.TIPO_REQUISITO, "IDTIPOREQUISITO", "DESCRIPCIONTIPO", rEQUISITO.IDTIPOREQUISITO);
            return View(rEQUISITO);
        }

        // GET: Requisito/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REQUISITO rEQUISITO = await db.REQUISITO.FindAsync(id);
            if (rEQUISITO == null)
            {
                return HttpNotFound();
            }
            return View(rEQUISITO);
        }

        // POST: Requisito/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            REQUISITO rEQUISITO = await db.REQUISITO.FindAsync(id);
            db.REQUISITO.Remove(rEQUISITO);
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
