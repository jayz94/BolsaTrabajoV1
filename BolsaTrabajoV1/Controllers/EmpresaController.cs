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
using System.Data.SqlClient;

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
        public ActionResult Resultados(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var usr = new USUARIO();
            usr = (USUARIO)Session["usuario"];
            //var empresa = 1;/*variable se obtendra como parametro*/
            var plaza = id;/*variable se obtendra como parametro*/

            //Obtenemos los resultados de los examenes para los postulantes
            //a cierta plaza de cierta empresa

            var query = db.ObtenerResultados(usr.CODIGOEMPRESA, plaza);

            List<ObtenerResultados_Result> resultados = new List<ObtenerResultados_Result>();

            foreach (var result in query)
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






            TempData["nPais"] = eMPRESA.MUNICIPIO.DEPARTAMENTO.PAIS.NOMBREPAIS;
            TempData["nDept"] = eMPRESA.MUNICIPIO.DEPARTAMENTO.NOMBREDEPARTAMENTO;
            TempData["nGiro"] = eMPRESA.GIRO.DESCRIPCIONGIRO;
            TempData["nMn"] = eMPRESA.MUNICIPIO.NOMBREMUNICIPIO;





            return View(eMPRESA);
        }








        // GET: Empresa/Create
        public ActionResult Create()
        {



            ViewBag.PAIS = new SelectList(db.PAIS, "IDPAIS", "NOMBREPAIS");
            ViewBag.IDGIRO = new SelectList(db.GIRO, "IDGIRO", "DESCRIPCIONGIRO");


            /*ESTE ROL SE ENVIA A LA VISTA PARA FILTRAR EL CONTENIDO*/

            ROL RolEmpresa = (from r in db.ROL
                              where r.NOMBREROL == "Empresa"
                              select r).SingleOrDefault();

            if (RolEmpresa != null)
            {

                TempData["RolEmpresa"] = RolEmpresa.IDROL.ToString();
            }



            return View();
        }


        public JsonResult GetMunicipios(int id)
        {


            SelectList municipios = new SelectList(db.MUNICIPIO.Where(p => p.IDDEPARTAMENTO == id), "IDMUNICIPIO", "NOMBREMUNICIPIO");


            return Json(new SelectList(municipios, "Value", "Text"));
        }




        public JsonResult GetDepartamentos(int id)
        {


            SelectList departamentos = new SelectList(db.DEPARTAMENTO.Where(p => p.IDPAIS == id), "IDDEPARTAMENTO", "NOMBREDEPARTAMENTO");


            return Json(new SelectList(departamentos, "Value", "Text"));
        }






        // POST: Empresa/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CODIGOEMPRESA,ABREVIATURA,NOMBREEMPRESA,CORREOELECTRONICO,TELEFONO,IDMUNICIPIO,NIT,IDGIRO,DESCRIPCION")] EMPRESA eMPRESA)
        {

            try
            {

                if (ModelState.IsValid)
                {
                    db.EMPRESA.Add(eMPRESA);
                    await db.SaveChangesAsync();

                    var usr = new USUARIO();
                    usr = (USUARIO)Session["usuario"];



                    USUARIO result = (from u in db.USUARIO
                                      where u.IDUSUARIO == usr.IDUSUARIO
                                      select u).SingleOrDefault();

                    result.CODIGOEMPRESA = eMPRESA.CODIGOEMPRESA;
                    db.SaveChanges();


                    return RedirectToAction("Index");

                }

                db.EMPRESA.Add(eMPRESA);
                await db.SaveChangesAsync();
                USUARIO us = (USUARIO)Session["usuario"];
                USUARIO usuario = db.USUARIO.Find(us.IDUSUARIO);
                usuario.CODIGOEMPRESA = eMPRESA.CODIGOEMPRESA;
                db.Entry(usuario).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");

            }

            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "No se guardaron los cambios. Por favor verifique los datos.");

            }

            ViewBag.IDMUNICIPIO = new SelectList(db.MUNICIPIO, "IDMUNICIPIO", "NOMBREMUNICIPIO", eMPRESA.IDMUNICIPIO);
            ViewBag.GIRO = new SelectList(db.GIRO, "IDGIRO", "DESCRIPCIONGIRO", eMPRESA.IDGIRO);

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
            ViewBag.DEPARTAMENTO = new SelectList(db.DEPARTAMENTO, "IDDEPARTAMENTO", "NOMBREDEPARTAMENTO");
            ViewBag.PAIS = new SelectList(db.PAIS, "IDPAIS", "NOMBREPAIS");
            ViewBag.IDGIRO = new SelectList(db.GIRO, "IDGIRO", "DESCRIPCIONGIRO");


            TempData["pais"] = eMPRESA.MUNICIPIO.DEPARTAMENTO.PAIS.IDPAIS;
            TempData["dept"] = eMPRESA.MUNICIPIO.DEPARTAMENTO.IDDEPARTAMENTO;
            TempData["giro"] = eMPRESA.GIRO.IDGIRO;
            TempData["mn"] = eMPRESA.MUNICIPIO.IDMUNICIPIO;



            return View(eMPRESA);
        }









        // POST: Empresa/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CODIGOEMPRESA,ABREVIATURA,NOMBREEMPRESA,CORREOELECTRONICO,TELEFONO,IDMUNICIPIO,NIT,IDGIRO,DESCRIPCION")] EMPRESA eMPRESA)
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



            TempData["nPais"] = eMPRESA.MUNICIPIO.DEPARTAMENTO.PAIS.NOMBREPAIS;
            TempData["nDept"] = eMPRESA.MUNICIPIO.DEPARTAMENTO.NOMBREDEPARTAMENTO;
            TempData["nGiro"] = eMPRESA.GIRO.DESCRIPCIONGIRO;
            TempData["nMn"] = eMPRESA.MUNICIPIO.NOMBREMUNICIPIO;
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
