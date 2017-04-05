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
    public class HabilidadController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: Habilidad
        public async Task<ActionResult> Index()
        {
            var hABILIDAD = db.HABILIDAD.Include(h => h.CURRICULUM).Include(h => h.TIPO_HABILIDAD);
            return View(await hABILIDAD.ToListAsync());
        }

        // GET: Habilidad/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HABILIDAD hABILIDAD = await db.HABILIDAD.FindAsync(id);
            if (hABILIDAD == null)
            {
                return HttpNotFound();
            }
            return View(hABILIDAD);
        }

        // GET: Habilidad/Create
        public ActionResult Create()
        {
            ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM");
            ViewBag.IDTIPOHABILIDAD = new SelectList(db.TIPO_HABILIDAD, "IDTIPOHABILIDAD", "NOMBRETIPOHABILIDAD");
            return View();
        }

        // POST: Habilidad/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDHABILIDAD,IDCURRICULUM,IDPOSTULANTE,IDTIPOHABILIDAD,NOMBREHABILIDAD")] HABILIDAD hABILIDAD)
        {
            if (ModelState.IsValid)
            {
                db.HABILIDAD.Add(hABILIDAD);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM", hABILIDAD.IDCURRICULUM);
            ViewBag.IDTIPOHABILIDAD = new SelectList(db.TIPO_HABILIDAD, "IDTIPOHABILIDAD", "NOMBRETIPOHABILIDAD", hABILIDAD.IDTIPOHABILIDAD);
            return View(hABILIDAD);
        }

        // GET: Habilidad/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HABILIDAD hABILIDAD = await db.HABILIDAD.FindAsync(id);
            if (hABILIDAD == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM", hABILIDAD.IDCURRICULUM);
            ViewBag.IDTIPOHABILIDAD = new SelectList(db.TIPO_HABILIDAD, "IDTIPOHABILIDAD", "NOMBRETIPOHABILIDAD", hABILIDAD.IDTIPOHABILIDAD);
            return View(hABILIDAD);
        }

        // POST: Habilidad/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDHABILIDAD,IDCURRICULUM,IDPOSTULANTE,IDTIPOHABILIDAD,NOMBREHABILIDAD")] HABILIDAD hABILIDAD)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hABILIDAD).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM", hABILIDAD.IDCURRICULUM);
            ViewBag.IDTIPOHABILIDAD = new SelectList(db.TIPO_HABILIDAD, "IDTIPOHABILIDAD", "NOMBRETIPOHABILIDAD", hABILIDAD.IDTIPOHABILIDAD);
            return View(hABILIDAD);
        }

        // GET: Habilidad/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HABILIDAD hABILIDAD = await db.HABILIDAD.FindAsync(id);
            if (hABILIDAD == null)
            {
                return HttpNotFound();
            }
            return View(hABILIDAD);
        }

        // POST: Habilidad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            HABILIDAD hABILIDAD = await db.HABILIDAD.FindAsync(id);
            db.HABILIDAD.Remove(hABILIDAD);
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
