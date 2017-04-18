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
    public class ROL_USUARIO_MMController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: ROL_USUARIO_MM
        public async Task<ActionResult> Index()
        {
            var rOL_USUARIO_MM = db.ROL_USUARIO_MM.Include(r => r.ROL).Include(r => r.USUARIO);
            return View(await rOL_USUARIO_MM.ToListAsync());
        }

        // GET: ROL_USUARIO_MM/Details/5
        public async Task<ActionResult> Details(int? idusuario, int? idrol)
        {
            if (idusuario == null || idrol == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ROL_USUARIO_MM rOL_USUARIO_MM = await db.ROL_USUARIO_MM.FindAsync(idusuario, idrol);
            if (rOL_USUARIO_MM == null)
            {
                return HttpNotFound();
            }
            return View(rOL_USUARIO_MM);
        }

        // GET: ROL_USUARIO_MM/Create
        public ActionResult Create()
        {
            ViewBag.IDROL = new SelectList(db.ROL, "IDROL", "NOMBREROL");
            ViewBag.IDUSUARIO = new SelectList(db.USUARIO, "IDUSUARIO", "NOMBREUSUARIO");
            return View();
        }

        // POST: ROL_USUARIO_MM/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDUSUARIO,IDROL")] ROL_USUARIO_MM rOL_USUARIO_MM)
        {
            if (ModelState.IsValid)
            {
                int id_usuario = rOL_USUARIO_MM.IDUSUARIO;
                var rol_usuario_viejo = from ru in db.ROL_USUARIO_MM where ru.IDUSUARIO == id_usuario select ru;
                foreach (var item in rol_usuario_viejo)
                {
                    item.ACTIVO = false;
                }
                rOL_USUARIO_MM.ACTIVO = true;
                rOL_USUARIO_MM.FECHAASIGNACION = DateTime.Now;
                db.ROL_USUARIO_MM.Add(rOL_USUARIO_MM);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IDROL = new SelectList(db.ROL, "IDROL", "NOMBREROL", rOL_USUARIO_MM.IDROL);
            ViewBag.IDUSUARIO = new SelectList(db.USUARIO, "IDUSUARIO", "NOMBREUSUARIO", rOL_USUARIO_MM.IDUSUARIO);
            return View(rOL_USUARIO_MM);
        }

        // GET: ROL_USUARIO_MM/Edit/5
        public async Task<ActionResult> Edit(int? idusuario,int? idrol)
        {
            if (idusuario == null|| idrol== null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ROL_USUARIO_MM rOL_USUARIO_MM = await db.ROL_USUARIO_MM.FindAsync(idusuario,idrol);
            if (rOL_USUARIO_MM == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDROL = new SelectList(db.ROL, "IDROL", "NOMBREROL", rOL_USUARIO_MM.IDROL);
            ViewBag.IDUSUARIO = new SelectList(db.USUARIO, "IDUSUARIO", "NOMBREUSUARIO", rOL_USUARIO_MM.IDUSUARIO);
            return View(rOL_USUARIO_MM);
        }

        // POST: ROL_USUARIO_MM/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDUSUARIO,IDROL,ACTIVO,FECHAASIGNACION")] ROL_USUARIO_MM rOL_USUARIO_MM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rOL_USUARIO_MM).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IDROL = new SelectList(db.ROL, "IDROL", "NOMBREROL", rOL_USUARIO_MM.IDROL);
            ViewBag.IDUSUARIO = new SelectList(db.USUARIO, "IDUSUARIO", "NOMBREUSUARIO", rOL_USUARIO_MM.IDUSUARIO);
            return View(rOL_USUARIO_MM);
        }

        // GET: ROL_USUARIO_MM/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ROL_USUARIO_MM rOL_USUARIO_MM = await db.ROL_USUARIO_MM.FindAsync(id);
            if (rOL_USUARIO_MM == null)
            {
                return HttpNotFound();
            }
            return View(rOL_USUARIO_MM);
        }

        // POST: ROL_USUARIO_MM/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ROL_USUARIO_MM rOL_USUARIO_MM = await db.ROL_USUARIO_MM.FindAsync(id);
            db.ROL_USUARIO_MM.Remove(rOL_USUARIO_MM);
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
