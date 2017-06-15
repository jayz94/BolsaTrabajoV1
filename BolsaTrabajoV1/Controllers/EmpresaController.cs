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
    public class EmpresaController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: Empresa
        public async Task<ActionResult> Index()
        {
            return View(await db.EMPRESA.ToListAsync());
        }

        // GET: Resultados
        public ActionResult Resultados()
        {
            var empresa = 1;/*variable se obtendra como parametro*/
            var plaza = 1;/*variable se obtendra como parametro*/

            //Obtenemos los resultados de los examenes para los postulantes
            //a cierta plaza de cierta empresa

            var query = db.ObtenerResultados(empresa, plaza);

            List<ObtenerResultados_Result> resultados = new List<ObtenerResultados_Result>();
            
            foreach(var result in query)
            {
                ObtenerResultados_Result resultado = new ObtenerResultados_Result();
                resultado.IDPOSTULANTE = result.IDPOSTULANTE;
                resultado.NOMBREPOSTULANTE = result.NOMBREPOSTULANTE;
                resultado.CODIGOEMPRESA = result.CODIGOEMPRESA;
                resultado.NOMBREEMPRESA = result.NOMBREEMPRESA;
                resultado.IDPLAZA = result.IDPLAZA;
                resultado.DESCRIPCIONPLAZA = result.DESCRIPCIONPLAZA;
                resultado.CODIGOEXAMEN = result.CODIGOEXAMEN;
                resultado.DESCRIPCIONEXAMEN = result.DESCRIPCIONEXAMEN;
                resultado.PUNTAJE = result.PUNTAJE;
                resultado.PONDERACION = result.PONDERACION;
                resultado.TOTAL = result.TOTAL;

                resultados.Add(resultado);
                     
            }

            ViewBag.resultados = resultados;
            
            return View();
        }

        // GET: Empresa/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPRESA eMPRESA = await db.EMPRESA.FindAsync(id);
            if (eMPRESA == null)
            {
                return HttpNotFound();
            }
            return View(eMPRESA);
        }

        // GET: Empresa/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Empresa/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDEMPRESA,CODIGOEMPRESA,NOMBREEMPRESA")] EMPRESA eMPRESA)
        {
            if (ModelState.IsValid)
            {
                db.EMPRESA.Add(eMPRESA);
                await db.SaveChangesAsync();
                USUARIO us = (USUARIO)Session["usuario"];
                USUARIO usuario = db.USUARIO.Find(us.IDUSUARIO);
                usuario.CODIGOEMPRESA = eMPRESA.CODIGOEMPRESA;
                db.Entry(usuario).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(eMPRESA);
        }

        // GET: Empresa/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPRESA eMPRESA = await db.EMPRESA.FindAsync(id);
            if (eMPRESA == null)
            {
                return HttpNotFound();
            }
            return View(eMPRESA);
        }

        // POST: Empresa/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDEMPRESA,CODIGOEMPRESA,NOMBREEMPRESA")] EMPRESA eMPRESA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eMPRESA).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(eMPRESA);
        }

        // GET: Empresa/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPRESA eMPRESA = await db.EMPRESA.FindAsync(id);
            if (eMPRESA == null)
            {
                return HttpNotFound();
            }
            return View(eMPRESA);
        }

        // POST: Empresa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            EMPRESA eMPRESA = await db.EMPRESA.FindAsync(id);
            db.EMPRESA.Remove(eMPRESA);
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
