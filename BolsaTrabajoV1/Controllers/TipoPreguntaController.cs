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
    public class TipoPreguntaController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: TipoPregunta
        public async Task<ActionResult> Index()
        {
            return View(await db.TIPOPREGUNTA.ToListAsync());
        }

        // GET: TipoPregunta/Details/5
        public async Task<ActionResult> Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPOPREGUNTA tIPOPREGUNTA = await db.TIPOPREGUNTA.FindAsync(id);
            if (tIPOPREGUNTA == null)
            {
                return HttpNotFound();
            }
            return View(tIPOPREGUNTA);
        }

        // GET: TipoPregunta/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoPregunta/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDTIPOPREGUNTA,NOMBRETIPOPREGUNTA")] TIPOPREGUNTA tIPOPREGUNTA)
        {
            if (ModelState.IsValid)
            {
                db.TIPOPREGUNTA.Add(tIPOPREGUNTA);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tIPOPREGUNTA);
        }

        // GET: TipoPregunta/Edit/5
        public async Task<ActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPOPREGUNTA tIPOPREGUNTA = await db.TIPOPREGUNTA.FindAsync(id);
            if (tIPOPREGUNTA == null)
            {
                return HttpNotFound();
            }
            return View(tIPOPREGUNTA);
        }

        // POST: TipoPregunta/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDTIPOPREGUNTA,NOMBRETIPOPREGUNTA")] TIPOPREGUNTA tIPOPREGUNTA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tIPOPREGUNTA).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tIPOPREGUNTA);
        }

        // GET: TipoPregunta/Delete/5
        public async Task<ActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPOPREGUNTA tIPOPREGUNTA = await db.TIPOPREGUNTA.FindAsync(id);
            if (tIPOPREGUNTA == null)
            {
                return HttpNotFound();
            }
            return View(tIPOPREGUNTA);
        }

        // POST: TipoPregunta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(short id)
        {
            TIPOPREGUNTA tIPOPREGUNTA = await db.TIPOPREGUNTA.FindAsync(id);
            db.TIPOPREGUNTA.Remove(tIPOPREGUNTA);
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
