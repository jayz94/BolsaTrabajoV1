﻿using System;
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
    public class ExperienciaLaboralController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: ExperienciaLaboral
        public async Task<ActionResult> Index()
        {
            //var eXPERIENCIALABORAL = db.EXPERIENCIALABORAL.Include(e => e.CURRICULUM);
            //Models.Linq.Conexion cn = new Models.Linq.Conexion();
            //List<Models.Linq.AREA_CONOCIMIENTO> area = cn.DB.AREA_CONOCIMIENTO.Where(f => f.IDAREACONOCIMIENTO == 1).ToList();
            int idCurriculum = (int)Session["idCurriculum"];
            var experiencia = from exp in db.EXPERIENCIALABORAL where exp.IDCURRICULUM == idCurriculum select exp;
            return View(await experiencia.ToListAsync());
        }

        // GET: ExperienciaLaboral/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EXPERIENCIALABORAL eXPERIENCIALABORAL = await db.EXPERIENCIALABORAL.FindAsync(id);
            if (eXPERIENCIALABORAL == null)
            {
                return HttpNotFound();
            }
            return View(eXPERIENCIALABORAL);
        }

        // GET: ExperienciaLaboral/Create
        public ActionResult Create()
        {
            //ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM");
            //campos que trae desde la base, EL CAMPO EN LA VIEWBAG DEBE LLAMARSE IGUAL QUE COMO LO GUARDA
            ViewBag.IDCARGO = new SelectList(db.CARGO,"IDCARGO","NOMBRECARGO");
            return View();
        }

        // POST: ExperienciaLaboral/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "INSTITUCION,IDCARGO,TITULOPROYECTO,FECHAINICIO,FECHAFIN,NUMEROCONTACTOORG")] EXPERIENCIALABORAL eXPERIENCIALABORAL)
        {
            if (ModelState.IsValid)
            {
                //eXPERIENCIALABORAL.IDCURRICULUM = Convert.ToInt32(Session["id_curriculum"]);
                eXPERIENCIALABORAL.IDCURRICULUM = (int)Session["idCurriculum"];
                eXPERIENCIALABORAL.IDPOSTULANTE = (int)Session["idPostulante"];
                db.EXPERIENCIALABORAL.Add(eXPERIENCIALABORAL);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM", eXPERIENCIALABORAL.IDCURRICULUM);
            return View(eXPERIENCIALABORAL);
        }

        // GET: ExperienciaLaboral/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EXPERIENCIALABORAL eXPERIENCIALABORAL = await db.EXPERIENCIALABORAL.FindAsync(id);
            if (eXPERIENCIALABORAL == null)
            {
                return HttpNotFound();
            }
            //ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM", eXPERIENCIALABORAL.IDCURRICULUM);
            //primer parametro es la lista de objetos, el segundo el identificador
            ViewBag.IDCARGO = new SelectList(db.CARGO, "IDCARGO", "NOMBRECARGO", eXPERIENCIALABORAL.IDCARGO);
            return View(eXPERIENCIALABORAL);
        }

        // POST: ExperienciaLaboral/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDEXPERIENCIALABORAL,IDCURRICULUM,IDPOSTULANTE,INSTITUCION,IDCARGO,TITULOPROYECTO,FECHAINICIO,FECHAFIN,NUMEROCONTACTOORG")] EXPERIENCIALABORAL eXPERIENCIALABORAL)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eXPERIENCIALABORAL).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM", eXPERIENCIALABORAL.IDCURRICULUM);
            return View(eXPERIENCIALABORAL);
        }

        // GET: ExperienciaLaboral/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EXPERIENCIALABORAL eXPERIENCIALABORAL = await db.EXPERIENCIALABORAL.FindAsync(id);
            if (eXPERIENCIALABORAL == null)
            {
                return HttpNotFound();
            }
            return View(eXPERIENCIALABORAL);
        }

        // POST: ExperienciaLaboral/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            EXPERIENCIALABORAL eXPERIENCIALABORAL = await db.EXPERIENCIALABORAL.FindAsync(id);
            db.EXPERIENCIALABORAL.Remove(eXPERIENCIALABORAL);
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
