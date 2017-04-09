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
    }
}