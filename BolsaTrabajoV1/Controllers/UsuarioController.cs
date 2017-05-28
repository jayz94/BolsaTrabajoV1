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
    public class UsuarioController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: Usuario
        public async Task<ActionResult> Index()
        {
            //var uSUARIO = db.USUARIO.Include(u => u.EMPRESA).Include(u => u.POSTULANTE1);
            return View(await db.USUARIO.ToListAsync());
        }

        // GET: Usuario/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USUARIO uSUARIO = await db.USUARIO.FindAsync(id);
            if (uSUARIO == null)
            {
                return HttpNotFound();
            }
            return View(uSUARIO);
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            ViewBag.IDEMPRESA = new SelectList(db.EMPRESA, "IDEMPRESA", "CODIGOEMPRESA");
            //ViewBag.IDPOSTULANTE = new SelectList(db.POSTULANTE, "IDPOSTULANTE", "NOMBREPOSTULANTE");
            //ViewBag.IDIOMA = new SelectList(db.IDIOMA,"IDIDIOMA","NOMBREIDIOMA");
            return View();
        }

        // POST: Usuario/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(String NOMBREUSUARIO,String PASSWORD,String PASS2,String IDIOMA,String CORREO)
        {
            /*if (ModelState.IsValid)
            {*/
            if (PASSWORD.Equals(PASS2))
            {
                USUARIO us = new USUARIO();
                us.NOMBREUSUARIO = NOMBREUSUARIO;
                us.PASSWORD = PASSWORD;
                us.ACTIVO = false;
                us.IDIOMA = IDIOMA;
                us.CORREO = CORREO;
                db.USUARIO.Add(us);
                await db.SaveChangesAsync();
                int idUsuario = us.IDUSUARIO; // recuperar el id
                Session["idUs"] = idUsuario;
                //return RedirectToAction("Create", "Postulante");
                ViewBag.Exito = "Usuario Ingresado con exito";
            }
            else {
                ViewBag.ErrorPass = "Contraseñas no Coinciden";
            }
            return View();
           // }

            //ViewBag.IDEMPRESA = new SelectList(db.EMPRESA, "IDEMPRESA", "CODIGOEMPRESA", uSUARIO.CODIGOEMPRESA);
            //ViewBag.IDPOSTULANTE = new SelectList(db.POSTULANTE, "IDPOSTULANTE", "NOMBREPOSTULANTE", uSUARIO.IDPOSTULANTE);
            //return View(uSUARIO);
            return RedirectToAction("Create", "Postulante");
        }

        // GET: Usuario/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USUARIO uSUARIO = await db.USUARIO.FindAsync(id);
            if (uSUARIO == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDEMPRESA = new SelectList(db.EMPRESA, "IDEMPRESA", "CODIGOEMPRESA", uSUARIO.CODIGOEMPRESA);
            //ViewBag.IDPOSTULANTE = new SelectList(db.POSTULANTE, "IDPOSTULANTE", "NOMBREPOSTULANTE", uSUARIO.IDPOSTULANTE);
            return View(uSUARIO);
        }

        // POST: Usuario/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDUSUARIO,CODIGOEMPRESA,NOMBREUSUARIO,COLOR,IDIOMA,CORREO")] USUARIO uSUARIO,String PASSA, String PASSN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uSUARIO).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CODIGOEMPRESA = new SelectList(db.EMPRESA, "IDEMPRESA", "CODIGOEMPRESA", uSUARIO.CODIGOEMPRESA);
           // ViewBag.IDPOSTULANTE = new SelectList(db.POSTULANTE, "IDPOSTULANTE", "NOMBREPOSTULANTE", uSUARIO.IDPOSTULANTE);
            return View(uSUARIO);
        }

        // GET: Usuario/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USUARIO uSUARIO = await db.USUARIO.FindAsync(id);
            if (uSUARIO == null)
            {
                return HttpNotFound();
            }
            return View(uSUARIO);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            USUARIO uSUARIO = await db.USUARIO.FindAsync(id);
            db.USUARIO.Remove(uSUARIO);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public String CambiarEstadoUsuario(int id) {
            db.cambiarEstadoUsuario(id);
            return "ya :D "+ id;
        }

        [HttpPost]
        public void DesbloquearIntentos(int id) {
            db.DesbloqueoUsuario(id);
        }

        public async Task<ActionResult> cerrarSesion()
        {
            Session.Clear();
            Session["idRol"] = 1;
            Session["nombre"] = "NO REGISTRADO";
            return RedirectToAction("Index", "Home");
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
