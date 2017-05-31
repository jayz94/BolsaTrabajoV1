using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BolsaTrabajoV1.Models;
using System.Data.Entity;
using System.Threading.Tasks;

namespace BolsaTrabajoV1.Controllers
{
    public class HomeController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();
        public ActionResult Index()
        {
            var rol = 1;//obtenemos el rol
            Session["nombre"] = "Invitado";
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
            ViewBag.plazas = db.ViewPlazaGenerica.ToList();
            return View();
        }

        public async Task<ActionResult> TipoContacto()
        {
           
            return View(await db.TIPO_CONTACTO.ToListAsync());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public void desplegarMenu(List<MENU> menus)
        {
            Console.Write("Hola a todos");
        }
    }
}