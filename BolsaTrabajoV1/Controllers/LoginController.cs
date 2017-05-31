using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Entity.Core.Objects;



using BolsaTrabajoV1.Models;

namespace BolsaTrabajoV1.Controllers
{
    public class LoginController : Controller
    {

        private ConexionBDBolsa db = new ConexionBDBolsa();


        [AllowAnonymous]
        public ActionResult Index()
        {

            TempData["Message"] = "";
            return View();
        }




        public ActionResult Perfil()
        {
         
            var rol = 1;//obtenemos el rol

            var query = from menu in db.MENU
                        where menu.ROL.Any(m => m.IDROL == rol)
                        where menu.MENU2 == null
                        select menu;
            List<MENU> menus = new List<MENU>();
            foreach (var result in query)
            {
                MENU menu = new MENU();
                menu.IDMENU = result.IDMENU;
                menu.NOMBREMENU = result.NOMBREMENU;
                menu.URL = result.URL;
                menu.IMAGEN = result.IMAGEN;
                menu.DESCRIPCIONMENU = result.DESCRIPCIONMENU;
                menu.ORDEN = result.ORDEN;
                menu.MENU1 = result.MENU1;
                menu.MENU2 = result.MENU2;
                //menu.IDPADRE = result.IDPADRE;
                menus.Add(menu);
            }
            Session["menus"] = menus;



            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(USUARIO user)
        {


            ObjectResult<ValidarUsuario_Result> result = db.ValidarUsuario(user.NOMBREUSUARIO, user.PASSWORD);

            int? valido;
            int? userID;
            int? rol;

            foreach (var v in result.ToList())

            {
                valido = v.Status;
                userID = v.ID;
                rol = v.Rol;

                switch (valido.Value)
                {
                    case 0:
                        TempData["Message"] = "Contraseña invalida";
                        break;
                    case -3:
                        TempData["Message"] = "Cuenta bloqueada";
                        break;
                    default:

                        FormsAuthentication.SetAuthCookie(user.NOMBREUSUARIO, true);
                        TempData["Message"] = user.NOMBREUSUARIO;
                        user.IDUSUARIO = userID.Value;
                        user.IDROL = Convert.ToInt16(rol.Value);
                       
                        Session["usuario"] = user;
                        return RedirectToAction("Perfil");


                }




            }




            return View(user);



        }


        [HttpPost]
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }


    }
}