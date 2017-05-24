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
    public class PostulanteController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: Postulante
        public async Task<ActionResult> Index()
        {
            /* 
                        {
                            menu.IDMENU,
                            menu.IDPADRE,
                            menu.NOMBREMENU,
                            menu.URL,
                            menu.IMAGEN,
                            menu.DESCRIPCIONMENU,
                            menu.ORDEN
                        };*/
            //ViewData["menus"] = menus;
            // ObjectQuery<MENU> q = new ObjectQuery<MENU>(query, db, MergeOption.NoTracking);
            //List<MENU> submenus = menus;
            //Session["submenus"] = submenus;
            //List<MENU> subsubmenus = menus;
            //Session["subsubmenus"] = subsubmenus;
            var pOSTULANTE = db.POSTULANTE.Include(p => p.CURRICULUM1).Include(p => p.MUNICIPIO).Include(p => p.USUARIO);
            return View(await pOSTULANTE.ToListAsync());
        }

        // GET: Postulante/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POSTULANTE pOSTULANTE = await db.POSTULANTE.FindAsync(id);
            if (pOSTULANTE == null)
            {
                return HttpNotFound();
            }
            return View(pOSTULANTE);
        }

        // GET: Postulante/Create
        public ActionResult Create()
        {
            ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM");
            ViewBag.IDMUNICIPIO = new SelectList(db.MUNICIPIO, "IDMUNICIPIO", "NOMBREMUNICIPIO");
            ViewBag.IDUSUARIO = new SelectList(db.USUARIO, "IDUSUARIO", "NOMBREUSUARIO");
            return View();
        }

        // POST: Postulante/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDPOSTULANTE,IDUSUARIO,IDMUNICIPIO,IDCURRICULUM,NOMBREPOSTULANTE,APELLIDOPOSTULANTE,GENERO,FECHANACIMIENTO,DIRECCION,URLCURRICULUM")] POSTULANTE pOSTULANTE)
        {
            if (ModelState.IsValid)
            {
                pOSTULANTE.IDUSUARIO =(int)Session["idUs"];
                db.POSTULANTE.Add(pOSTULANTE);
                await db.SaveChangesAsync();
                int id = pOSTULANTE.IDPOSTULANTE; // recuperar el id de postulante
                //busco usuario por el id de session
                USUARIO uSUARIO = await db.USUARIO.FindAsync((int)Session["idUs"]); 
                uSUARIO.IDPOSTULANTE =id;
                db.Entry(uSUARIO).State = EntityState.Modified;
                await db.SaveChangesAsync();
                //creo el curriculum de postulante
                CURRICULUM curriculum = new CURRICULUM();
                curriculum.IDPOSTULANTE = id;//le asigno el id de postulante a curriculum
                //guardo el curriculum
                db.CURRICULUM.Add(curriculum);
                await db.SaveChangesAsync();
                //obtengo el id del curriculum guardado y lo guardo en session
                int idCurriculum = curriculum.IDCURRICULUM;
                Session["idCurriculum"] = idCurriculum;
                Session["idPostulante"] = curriculum.IDPOSTULANTE;
                return RedirectToAction("mainCurriculum","Curriculum");
            }

            ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM", pOSTULANTE.IDCURRICULUM);
            ViewBag.IDMUNICIPIO = new SelectList(db.MUNICIPIO, "IDMUNICIPIO", "NOMBREMUNICIPIO", pOSTULANTE.IDMUNICIPIO);
            ViewBag.IDUSUARIO = new SelectList(db.USUARIO, "IDUSUARIO", "NOMBREUSUARIO", pOSTULANTE.IDUSUARIO);
            return View(pOSTULANTE);
        }

        // GET: Postulante/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POSTULANTE pOSTULANTE = await db.POSTULANTE.FindAsync(id);
            if (pOSTULANTE == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM", pOSTULANTE.IDCURRICULUM);
            ViewBag.IDMUNICIPIO = new SelectList(db.MUNICIPIO, "IDMUNICIPIO", "NOMBREMUNICIPIO", pOSTULANTE.IDMUNICIPIO);
            ViewBag.IDUSUARIO = new SelectList(db.USUARIO, "IDUSUARIO", "NOMBREUSUARIO", pOSTULANTE.IDUSUARIO);
            return View(pOSTULANTE);
        }

        // POST: Postulante/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDPOSTULANTE,IDUSUARIO,IDMUNICIPIO,IDCURRICULUM,NOMBREPOSTULANTE,APELLIDOPOSTULANTE,GENERO,FECHANACIMIENTO,DIRECCION,URLCURRICULUM")] POSTULANTE pOSTULANTE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pOSTULANTE).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM", pOSTULANTE.IDCURRICULUM);
            ViewBag.IDMUNICIPIO = new SelectList(db.MUNICIPIO, "IDMUNICIPIO", "NOMBREMUNICIPIO", pOSTULANTE.IDMUNICIPIO);
            ViewBag.IDUSUARIO = new SelectList(db.USUARIO, "IDUSUARIO", "NOMBREUSUARIO", pOSTULANTE.IDUSUARIO);
            return View(pOSTULANTE);
        }

        // GET: Postulante/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POSTULANTE pOSTULANTE = await db.POSTULANTE.FindAsync(id);
            if (pOSTULANTE == null)
            {
                return HttpNotFound();
            }
            return View(pOSTULANTE);
        }

        // POST: Postulante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            POSTULANTE pOSTULANTE = await db.POSTULANTE.FindAsync(id);
            db.POSTULANTE.Remove(pOSTULANTE);
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
