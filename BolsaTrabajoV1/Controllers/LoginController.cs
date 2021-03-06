﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Entity.Core.Objects;
using System.Security.Cryptography;
using System.Text;




using BolsaTrabajoV1.Models;

namespace BolsaTrabajoV1.Controllers
{
    public class LoginController : Controller
    {

        public ConexionBDBolsa db = new ConexionBDBolsa();


        [AllowAnonymous]
        public ActionResult Index()
        {

            TempData["Message"] = "";
            return View();
        }




        public ActionResult Perfil()
        {
            this.cargarMenus();
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(USUARIO user)
        {
            //inicia para la encriptacion CHA1
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] inputBytes = (new UnicodeEncoding()).GetBytes(user.PASSWORD);
            byte[] hash = sha1.ComputeHash(inputBytes);
            //finaliza para la encriptacion CHA1
            ObjectResult<ValidarUsuario_Result> result = db.ValidarUsuario(user.NOMBREUSUARIO, Convert.ToBase64String(hash));

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

                    case -2:
                        TempData["Message"] = "Cuenta desactivada";
                        break;

                    case -1:
                        TempData["Message"] = "No existe ese usuario";
                        break;


                    default:

                        FormsAuthentication.SetAuthCookie(user.NOMBREUSUARIO, true);
                      
                        user.IDUSUARIO = userID.Value;
                        user.IDROL = Convert.ToInt16(rol.Value);
                        USUARIO currentU= (from u in db.USUARIO
                                          where u.IDUSUARIO == user.IDUSUARIO
                                          select u).SingleOrDefault();

                        
                        TempData["Message"] = currentU.NOMBREUSUARIO;

                        Session["usuario"] = currentU;
                        
                        ROL RolEmpresa= (from r in db.ROL
                                         where r.NOMBREROL=="Postulante"
                                         select r).SingleOrDefault();

                        this.cargarMenus();
                        int numPos = db.POSTULANTE.Where(pos => pos.IDUSUARIO == currentU.IDUSUARIO).Count();
                        if(currentU.IDROL==2 && numPos==0)//id de postulante
                        {
                            return RedirectToAction("Create", "Postulante");
                        }
                        if(currentU.IDROL==3 && currentU.CODIGOEMPRESA== null)
                        {
                            return RedirectToAction("Create", "Empresa");
                        }
                        if (currentU.IDROL == 2 && numPos==1)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        return RedirectToAction("Index", "Postulante");
                }
            }
            return View(user);
        }


        public void cargarMenus()
        {
            //var rol = 5;//obtenemos el rol

            var usr = new USUARIO();
            usr = (USUARIO)Session["usuario"];

            var query = from menu in db.MENU
                        where menu.ROL.Any(m => m.IDROL == usr.IDROL)
                        //where menu.MENU2 == null
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