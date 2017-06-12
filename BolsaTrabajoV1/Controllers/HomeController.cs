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

            if (Session["usuario"] == null) {
                var us = new USUARIO();
                us.IDROL = 1;
                us.NOMBREUSUARIO = "Invitado";
                Session["usuario"] = us;
                var query = from menu in db.MENU
                            where menu.ROL.Any(m => m.IDROL == us.IDROL)
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
            ViewBag.cargos = db.CARGO.ToList();
            ViewBag.areaCargos = db.AREA_CARGO.ToList();
            ViewBag.generos = db.GENERO.ToList();
            ViewBag.departamentos = db.DEPARTAMENTO.ToList();
            ViewBag.giros = db.GIRO.ToList();
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

        [HttpGet]
        public ActionResult filtrar(int idtabla,int idRegistro)
        {
            //tabla 1: cargo    //tabla 2: area cargo        //tabla 3: empresa
            //tabla 4: salario  //tabla 5: departamento      //tabla 6: genero
            //tabla 7: giro de empresa
            var plazas = from pla in db.ViewPlazaGenerica select pla;
            switch (idtabla) {
                case 1:
                    plazas = plazas.Where(pla=>pla.idcargo==idRegistro);
                    break;
                case 2:
                    plazas = plazas.Where(pla => pla.idareacargo == idRegistro);
                    break;
                case 3:
                    plazas = plazas.Where(pla=>pla.codempresa==idRegistro);
                    break;
                case 4:
                    switch (idRegistro)
                    {
                        case 1:
                            plazas = plazas.Where(pla=>pla.salario >0 && pla.salario<=500);
                            break;
                        case 2:
                            plazas = plazas.Where(pla => pla.salario > 500 && pla.salario <= 1000);
                            break;
                        case 3:
                            plazas = plazas.Where(pla => pla.salario > 1000 && pla.salario <= 1500);
                            break;
                        case 4:
                            plazas = plazas.Where(pla => pla.salario > 1500 && pla.salario <= 2000);
                            break;
                        case 5:
                            plazas = plazas.Where(pla => pla.salario > 2000);
                            break;
                        default:
                            plazas = plazas.Where(pla => pla.salario > 0);
                            break;
                    }
                    break;
                case 5:
                    plazas = plazas.Where(pla=>pla.iddepart==idRegistro);
                    break;
                case 6:
                    plazas = plazas.Where(pla=>pla.idgenero==idRegistro);
                    break;
                case 7:
                    plazas = plazas.Where(pla=>pla.idgiroemp==idRegistro);
                    break;
            }
            ViewBag.cargos = db.CARGO.ToList();
            ViewBag.areaCargos = db.AREA_CARGO.ToList();
            ViewBag.generos = db.GENERO.ToList();
            ViewBag.departamentos = db.DEPARTAMENTO.ToList();
            ViewBag.giros = db.GIRO.ToList();
            ViewBag.plazas = plazas;
            return View();

        }
    }
}