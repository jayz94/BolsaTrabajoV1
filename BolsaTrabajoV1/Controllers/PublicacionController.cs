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
    public class PublicacionController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: Publicacion
        public async Task<ActionResult> Index()
        {
            int idCurriculum = (int)Session["idCurriculum"];
            var publicacion = from pub in db.PUBLICACION where pub.IDCURRICULUM == idCurriculum select pub;
            //var pUBLICACION = db.PUBLICACION.Include(p => p.CURRICULUM);
            return View(await publicacion.ToListAsync());
        }

        // GET: Publicacion/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PUBLICACION pUBLICACION = await db.PUBLICACION.FindAsync(id);
            if (pUBLICACION == null)
            {
                return HttpNotFound();
            }
            return View(pUBLICACION);
        }

        // GET: Publicacion/Create
        public ActionResult Create()
        {
            ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM");
            return View();
        }

        // POST: Publicacion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TITULOPUBLICACION,EDITORIAL,REVISTA,FECHAPUBLICACION,ISBN,LIBRO,NUMEROEDICION")] PUBLICACION pUBLICACION)
        {
            if (ModelState.IsValid)
            {
                pUBLICACION.IDCURRICULUM = (int)Session["idCurriculum"];
                pUBLICACION.IDPOSTULANTE = (int)Session["idPostulante"];
                db.PUBLICACION.Add(pUBLICACION);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

           // ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM", pUBLICACION.IDCURRICULUM);
            return View(pUBLICACION);
        }

        // GET: Publicacion/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PUBLICACION pUBLICACION = await db.PUBLICACION.FindAsync(id);
            if (pUBLICACION == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM", pUBLICACION.IDCURRICULUM);
            return View(pUBLICACION);
        }

        // POST: Publicacion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDPUBLICACION,IDCURRICULUM,IDPOSTULANTE,TITULOPUBLICACION,EDITORIAL,REVISTA,FECHAPUBLICACION,ISBN,LIBRO,NUMEROEDICION")] PUBLICACION pUBLICACION)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pUBLICACION).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM", pUBLICACION.IDCURRICULUM);
            return View(pUBLICACION);
        }

        // GET: Publicacion/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PUBLICACION pUBLICACION = await db.PUBLICACION.FindAsync(id);
            if (pUBLICACION == null)
            {
                return HttpNotFound();
            }
            return View(pUBLICACION);
        }

        // POST: Publicacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PUBLICACION pUBLICACION = await db.PUBLICACION.FindAsync(id);
            db.PUBLICACION.Remove(pUBLICACION);
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
