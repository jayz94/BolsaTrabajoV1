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
    public class CurriculumController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();
        public ActionResult mainCurriculum() {
            if (Session["idCurriculum"] == null) { 
                USUARIO uSUARIO = (USUARIO)Session["usuario"];
                var postulante = from pos in db.POSTULANTE where pos.IDUSUARIO == uSUARIO.IDUSUARIO select pos.IDPOSTULANTE;
                if(postulante != null)
                {
                    foreach (var pos in postulante) {
                        var curriculum = (from curr in db.CURRICULUM where curr.IDPOSTULANTE == pos select curr.IDCURRICULUM);
                        if (curriculum != null)
                        {
                            foreach (var curr in curriculum)
                            {
                                Session["idCurriculum"] = (int)curr;
                            }
                        }
                        Session["idPostulante"] = (int)pos;
                    }
                }
            }
            return View();
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
