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
    public class FuncionPlazaController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: FuncionPlaza
        public async Task<ActionResult> Index()
        {
            var fUNCION_PLAZA = db.FUNCION_PLAZA.Include(f => f.PLAZA);
            return View(await fUNCION_PLAZA.ToListAsync());
        }

        // GET: FuncionPlaza/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FUNCION_PLAZA fUNCION_PLAZA = await db.FUNCION_PLAZA.FindAsync(id);
            if (fUNCION_PLAZA == null)
            {
                return HttpNotFound();
            }
            return View(fUNCION_PLAZA);
        }

        // GET: FuncionPlaza/Create
        public ActionResult Create()
        {
            ViewBag.IDPLAZA = new SelectList(db.PLAZA, "IDPLAZA", "FORMAPAGO");
            return View();
        }

        // POST: FuncionPlaza/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDFUNCION,IDPLAZA,DESCRIPCIONPLAZA")] FUNCION_PLAZA fUNCION_PLAZA)
        {
            if (ModelState.IsValid)
            {
                db.FUNCION_PLAZA.Add(fUNCION_PLAZA);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IDPLAZA = new SelectList(db.PLAZA, "IDPLAZA", "FORMAPAGO", fUNCION_PLAZA.IDPLAZA);
            return View(fUNCION_PLAZA);
        }

        //POST: FuncionPlaza/GuardarFuncion
        [HttpPost]
        public async Task<ActionResult> GuardarFuncion(int IDPLAZA, String desc)
        {
            FUNCION_PLAZA funcion = new FUNCION_PLAZA();
            funcion.IDPLAZA = IDPLAZA;
            funcion.DESCRIPCIONPLAZA = desc;
            db.FUNCION_PLAZA.Add(funcion);
            await db.SaveChangesAsync();
            return RedirectToAction("Details","Plaza", new { @id = IDPLAZA });
        }

        // GET: FuncionPlaza/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FUNCION_PLAZA fUNCION_PLAZA = await db.FUNCION_PLAZA.FindAsync(id);
            if (fUNCION_PLAZA == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDPLAZA = new SelectList(db.PLAZA, "IDPLAZA", "FORMAPAGO", fUNCION_PLAZA.IDPLAZA);
            return View(fUNCION_PLAZA);
        }

        // POST: FuncionPlaza/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDFUNCION,IDPLAZA,DESCRIPCIONPLAZA")] FUNCION_PLAZA fUNCION_PLAZA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fUNCION_PLAZA).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IDPLAZA = new SelectList(db.PLAZA, "IDPLAZA", "FORMAPAGO", fUNCION_PLAZA.IDPLAZA);
            return View(fUNCION_PLAZA);
        }

        // GET: FuncionPlaza/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FUNCION_PLAZA fUNCION_PLAZA = await db.FUNCION_PLAZA.FindAsync(id);
            if (fUNCION_PLAZA == null)
            {
                return HttpNotFound();
            }
            return View(fUNCION_PLAZA);
        }

        // POST: FuncionPlaza/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            FUNCION_PLAZA fUNCION_PLAZA = await db.FUNCION_PLAZA.FindAsync(id);
            db.FUNCION_PLAZA.Remove(fUNCION_PLAZA);
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
