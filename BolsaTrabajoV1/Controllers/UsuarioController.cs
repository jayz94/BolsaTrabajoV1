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
using System.Text;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Web.Routing;
using BolsaTrabajoV1.App_Code;

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

        public string generarMensaje(USUARIO us)
        {
            string body = "Hola " + us.NOMBREUSUARIO + ",";
            body += "<br /><br />Por favor haga click en el siguiente link para activar su cuenta.";
            body += "<br /><a href = '" + string.Format("{0}://{1}/Usuario/Activation?id={2}&hash={3}", Request.Url.Scheme, Request.Url.Authority, us.IDUSUARIO, us.PASSWORD) + "'>Click aqui para activar cuenta.</a>";
            body += "<br /><br />Gracias!";
            return body;
        }

        public async Task<ActionResult> Activation(int id, String hash)
        {
            USUARIO user = await db.USUARIO.FindAsync(id);
            if (user == null)
            {
                TempData["MessageV"] = "Usuario No Activado";
            }
            else
            {
                String pass1=hash.ToString().Substring(0, 3);
                String pass2=user.PASSWORD.ToString().Substring(0, 3);
                String pass3 = hash.ToString().Substring(4, 7);
                String pass4 = user.PASSWORD.ToString().Substring(4, 7);
                if (pass1.Equals(pass2) || pass3.Equals(pass4)) {
                    user.ACTIVO =true;
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["MessageV"] = "Usuario Activado Exitosamente";
                }
                else
                    TempData["MessageV"] = "Usuario No Activado";
            }
            return RedirectToAction("Index", "Login");
        }


        // POST: Usuario/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(String NOMBREUSUARIO,String PASSWORD,String PASS2,String rol,String CORREO)
        {
            /*if (ModelState.IsValid)
            {*/
            if (PASSWORD.Equals(PASS2))
            {
                int usuarioBase = (from usu in db.USUARIO where usu.NOMBREUSUARIO == NOMBREUSUARIO || usu.CORREO==CORREO select usu).Count();
                if (usuarioBase > 0) {
                    ViewBag.ErrorPass = "Este Correo o Usuario ya Existe, Intente con uno nuevo";
                }
                else
                {
                    //inicia para la encriptacion CHA1
                    SHA1 sha1 = new SHA1CryptoServiceProvider();
                    byte[] inputBytes = (new UnicodeEncoding()).GetBytes(PASSWORD);
                    byte[] hash = sha1.ComputeHash(inputBytes);
                    //finaliza para la encriptacion CHA1
                    USUARIO us = new USUARIO();
                    us.NOMBREUSUARIO = NOMBREUSUARIO;
                    us.PASSWORD = Convert.ToBase64String(hash);
                    us.ACTIVO = false;
                    switch (rol)
                    {
                        case "on":
                            us.IDROL = 2;
                            break;
                        case "four":
                            us.IDROL = 4;
                            USUARIO usuarioEmpresa = (USUARIO)Session["usuario"];
                            us.CODIGOEMPRESA = usuarioEmpresa.CODIGOEMPRESA;
                            break;
                        default:
                            us.IDROL = 3;
                            break;
                    }
                    us.CORREO = CORREO;
                    db.USUARIO.Add(us);
                    await db.SaveChangesAsync();
                    int idUsuario = us.IDUSUARIO; // recuperar el id
                    Session["idUs"] = idUsuario;
                    ViewBag.Exito = "Usuario Ingresado con exito , debe confirmar su cuenta mediante el correo enviado a su cuenta";

                    string mensaje = generarMensaje(us);

                    CorreoHelper ch = new CorreoHelper(); // crea clase helper
                    ch.Correo = us.CORREO;
                    ch.Nombre = us.NOMBREUSUARIO;
                    ch.Asunto = "Activacion de cuenta SIBTRA";
                    ch.Mensaje = mensaje;
                    ch.enviarCorreo();  // se envia correo
                }
            }
              
            else {
                ViewBag.ErrorPass = "Contraseñas no Coinciden";
            }
            if (rol == "four")
                return RedirectToAction("ListaEvaluadores", "Usuario");
            return View();
           // }

            //ViewBag.IDEMPRESA = new SelectList(db.EMPRESA, "IDEMPRESA", "CODIGOEMPRESA", uSUARIO.CODIGOEMPRESA);
            //ViewBag.IDPOSTULANTE = new SelectList(db.POSTULANTE, "IDPOSTULANTE", "NOMBREPOSTULANTE", uSUARIO.IDPOSTULANTE);
            //return View(uSUARIO);
            //return RedirectToAction("Create", "Postulante");
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
        public async Task<ActionResult> Edit([Bind(Include = "IDUSUARIO,CODIGOEMPRESA,NOMBREUSUARIO,COLOR,IDIOMA,CORREO")] USUARIO uSUARIO,String PASSA, String PASSN,int IDUSUARIO)
        {
            if (ModelState.IsValid)
            {
                if (PASSA != null && PASSN != null) {
                    int id = IDUSUARIO;
                    var pass = from us in db.USUARIO where us.IDUSUARIO == id select us;
                    SHA1 sha1 = new SHA1CryptoServiceProvider();
                    byte[] inputBytes = (new UnicodeEncoding()).GetBytes(PASSA);
                    byte[] hash = sha1.ComputeHash(inputBytes);
                    if (pass.Equals(Convert.ToBase64String(hash)))
                    {
                        SHA1 sha2 = new SHA1CryptoServiceProvider();
                        byte[] inputBytes2 = (new UnicodeEncoding()).GetBytes(PASSN);
                        byte[] hash2 = sha2.ComputeHash(inputBytes2);
                        uSUARIO.PASSWORD = Convert.ToBase64String(hash2);
                    }
                    else {
                        ViewBag.MessagePass = "La contraseña Actual no es correcta";
                        return View();
                    }
                    
                }
                db.Entry(uSUARIO).State = EntityState.Modified;
                await db.SaveChangesAsync();
                ViewBag.MessageExito = "Usuario Modificado con exito";
                return View();

            }
            ViewBag.CODIGOEMPRESA = new SelectList(db.EMPRESA, "IDEMPRESA", "CODIGOEMPRESA", uSUARIO.CODIGOEMPRESA);
            return View(uSUARIO);
        }

        //GET:/ListaEvaluadores
        public async Task<ActionResult> ListaEvaluadores()
        {
            USUARIO usuarioSession = (USUARIO)Session["usuario"];
            return View(await db.USUARIO.Where(us=>us.CODIGOEMPRESA==usuarioSession.CODIGOEMPRESA).ToListAsync());
        }
        public ActionResult CreateEvaluador()
        {
            return View();
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

        public RedirectToRouteResult cerrarSesion()
        {
            Session.Clear();
            Session["idRol"] = 1;
            Session["nombre"] = "NO REGISTRADO";
            return RedirectToAction("Index", "Home");
        }

        public RedirectToRouteResult redirigirUsuario(int idUsuario)
        {
            //USUARIO uSUARIO = await db.USUARIO.FindAsync(idUsuario);
            USUARIO uSUARIO = (USUARIO) Session["usuario"];
            switch (uSUARIO.IDROL) {
                case 2://usuario postulante
                    POSTULANTE post = (from postu in db.POSTULANTE where postu.IDUSUARIO == uSUARIO.IDUSUARIO select postu).FirstOrDefault();
                    //return RedirectToAction("Details", "Postulante", new { idusuario = post.IDUSUARIO });
                    if (post == null)
                    {
                        return RedirectToAction("Create","Postulante");
                    }
                    else
                        return RedirectToAction("Details", "Postulante", new RouteValueDictionary(new { id = post.IDPOSTULANTE }));
                case 3:
                    if (uSUARIO.CODIGOEMPRESA == null)
                    {
                        return RedirectToAction("Create", "Empresa");
                    }
                    //var empresa =await db.EMPRESA.FirstAsync(e => e.CODIGOEMPRESA == uSUARIO.CODIGOEMPRESA);
                    else
                        return RedirectToAction("Details", "Empresa", new { id = uSUARIO.CODIGOEMPRESA });
                default:
                    return RedirectToAction("Index", "Home");
            }
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
