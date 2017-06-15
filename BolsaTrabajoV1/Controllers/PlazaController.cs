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
    public class PlazaController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: Plaza
        public async Task<ActionResult> Index()
        {
            var pLAZA = db.PLAZA.Include(p => p.CARGO).Include(p => p.EMPRESA).Include(p => p.GENERO).Include(p => p.MUNICIPIO);
            return View(await pLAZA.ToListAsync());
        }

        // GET: Plaza/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PLAZA pLAZA = await db.PLAZA.FindAsync(id);
            var funcionPlaza = from funcion in db.FUNCION_PLAZA where funcion.IDPLAZA == id select funcion;
            var requisitoPlaza = from requisito in db.REQUISITO where requisito.IDPLAZA == id select requisito;
            if (pLAZA == null)
            {
                return HttpNotFound();
            }


            TempData["id_plaza"] = pLAZA.IDPLAZA;
            TempData["empresa_plaza"] = pLAZA.CODIGOEMPRESA;
            ROL RolEmpresa = (from r in db.ROL
                              where r.NOMBREROL == "Empresa"
                              select r).SingleOrDefault();

            Session["RolEmpresa"] = RolEmpresa.IDROL;


            ViewBag.TIPOREQUISITO = new SelectList(db.TIPO_REQUISITO, "IDTIPOREQUISITO", "DESCRIPCIONTIPO");
            ViewBag.funciones = funcionPlaza;
            ViewBag.requisitos = requisitoPlaza;
            return View(pLAZA);
        }





        public ActionResult Aplicar()
        {



            int idp = Convert.ToInt32(TempData["id_plaza"]);

            var usr = new USUARIO();
            usr = (USUARIO)Session["usuario"];


            POSTULANTE pos = (from p in db.POSTULANTE
                              where p.IDUSUARIO == usr.IDUSUARIO
                              select p).SingleOrDefault();


            POSTULANTE_PLAZA aplicacion = new POSTULANTE_PLAZA
            {
                IDPLAZA = idp,
                IDPOSTULANTE = pos.IDPOSTULANTE,
                FECHAAPLICACION = DateTime.Now

            };



            db.POSTULANTE_PLAZA.Add(aplicacion);

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // Make some adjustments.
                // ...
                // Try again.

            }



            return RedirectToAction("Index", "Home");
        }







        // GET: Plaza/Create
        public ActionResult Create()
        {
            ViewBag.IDCARGO = new SelectList(db.CARGO, "IDCARGO", "NOMBRECARGO");
            ViewBag.CODIGOEMPRESA = new SelectList(db.EMPRESA, "CODIGOEMPRESA", "NOMBREEMPRESA");
            ViewBag.IDGENERO = new SelectList(db.GENERO, "IDGENERO", "DESCRIPCIONGENERO");
            ViewBag.IDMUNICIPIO = new SelectList(db.MUNICIPIO, "IDMUNICIPIO", "NOMBREMUNICIPIO");
            return View();
        }

        // POST: Plaza/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDMUNICIPIO,IDGENERO,CODIGOEMPRESA,SALARIO,FORMAPAGO,EDADMIN,EDADMAX,VEHICULO,FECHAFIN,FECHAINICIO,IDPLAZA,IDCARGO,ANIOSMINIMOXP,DESCRIPCIONPLAZA,VACANTES,TIPOJORNADA")] PLAZA pLAZA)
        {
            if (ModelState.IsValid)
            {
                db.PLAZA.Add(pLAZA);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IDCARGO = new SelectList(db.CARGO, "IDCARGO", "NOMBRECARGO", pLAZA.IDCARGO);
            ViewBag.CODIGOEMPRESA = new SelectList(db.EMPRESA, "CODIGOEMPRESA", "NOMBREEMPRESA", pLAZA.CODIGOEMPRESA);
            ViewBag.IDGENERO = new SelectList(db.GENERO, "IDGENERO", "DESCRIPCIONGENERO", pLAZA.IDGENERO);
            ViewBag.IDMUNICIPIO = new SelectList(db.MUNICIPIO, "IDMUNICIPIO", "NOMBREMUNICIPIO", pLAZA.IDMUNICIPIO);
            return View(pLAZA);
        }

        // GET: Plaza/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PLAZA pLAZA = await db.PLAZA.FindAsync(id);
            if (pLAZA == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCARGO = new SelectList(db.CARGO, "IDCARGO", "NOMBRECARGO", pLAZA.IDCARGO);
            ViewBag.CODIGOEMPRESA = new SelectList(db.EMPRESA, "CODIGOEMPRESA", "NOMBREEMPRESA", pLAZA.CODIGOEMPRESA);
            ViewBag.IDGENERO = new SelectList(db.GENERO, "IDGENERO", "DESCRIPCIONGENERO", pLAZA.IDGENERO);
            ViewBag.IDMUNICIPIO = new SelectList(db.MUNICIPIO, "IDMUNICIPIO", "NOMBREMUNICIPIO", pLAZA.IDMUNICIPIO);
            return View(pLAZA);
        }

        // POST: Plaza/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDMUNICIPIO,IDGENERO,CODIGOEMPRESA,SALARIO,FORMAPAGO,EDADMIN,EDADMAX,VEHICULO,FECHAFIN,FECHAINICIO,IDPLAZA,IDCARGO,ANIOSMINIMOXP,DESCRIPCIONPLAZA,VACANTES,TIPOJORNADA")] PLAZA pLAZA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pLAZA).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IDCARGO = new SelectList(db.CARGO, "IDCARGO", "NOMBRECARGO", pLAZA.IDCARGO);
            ViewBag.CODIGOEMPRESA = new SelectList(db.EMPRESA, "CODIGOEMPRESA", "NOMBREEMPRESA", pLAZA.CODIGOEMPRESA);
            ViewBag.IDGENERO = new SelectList(db.GENERO, "IDGENERO", "DESCRIPCIONGENERO", pLAZA.IDGENERO);
            ViewBag.IDMUNICIPIO = new SelectList(db.MUNICIPIO, "IDMUNICIPIO", "NOMBREMUNICIPIO", pLAZA.IDMUNICIPIO);
            return View(pLAZA);
        }

        // GET: Plaza/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PLAZA pLAZA = await db.PLAZA.FindAsync(id);
            if (pLAZA == null)
            {
                return HttpNotFound();
            }
            return View(pLAZA);
        }

        // POST: Plaza/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PLAZA pLAZA = await db.PLAZA.FindAsync(id);
            db.PLAZA.Remove(pLAZA);
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
