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
    public class ExamenController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: Examen
        public async Task<ActionResult> Index()
        {
            USUARIO user = (USUARIO)Session["usuario"];
            var eXAMEN = db.EXAMEN.Include(e => e.EMPRESA).Include(e => e.TIPO_EXAMEN).Where(e=>e.CODIGOEMPRESA==user.CODIGOEMPRESA);
            return View(await eXAMEN.ToListAsync());
        }
        
        public ActionResult Test(int? id)
        {
            var ex = id;/*esta variable se sustituira por un parametro*/
            //ordenamos de manera aleatoria las preguntas y solo tomamos 5
            var query = (from examen in db.EXAMEN
                        join pregunta in db.PREGUNTA on examen.CODIGOEXAMEN equals pregunta.CODIGOEXAMEN
                        where examen.CODIGOEXAMEN == ex
                        select new { pregunta.IDPREGUNTA,pregunta.TEXTOPREGUNTA}).OrderBy(question => Guid.NewGuid()).Take(5);

            

            List<PREGUNTA> preguntas = new List<PREGUNTA>();

            foreach (var result in query)
            {
                PREGUNTA pregunta = new PREGUNTA();
                pregunta.IDPREGUNTA = result.IDPREGUNTA;
                pregunta.TEXTOPREGUNTA = result.TEXTOPREGUNTA;

                var query2 = (from exam in db.EXAMEN
                              join preg in db.PREGUNTA on exam.CODIGOEXAMEN equals preg.CODIGOEXAMEN
                              join op in db.OPCION_PREGUNTA on preg.IDPREGUNTA equals op.IDPREGUNTA
                              where exam.CODIGOEXAMEN == ex
                              where preg.IDPREGUNTA == pregunta.IDPREGUNTA
                              select new { op.IDOPCIONPREGUNTA,op.DESCRIPCIONOPCION,op.IDPREGUNTA }
                    ).OrderBy(question => Guid.NewGuid());

                foreach (var r in query2)
                {
                    OPCION_PREGUNTA opcion = new OPCION_PREGUNTA();
                    opcion.IDOPCIONPREGUNTA = r.IDOPCIONPREGUNTA;
                    opcion.DESCRIPCIONOPCION = r.DESCRIPCIONOPCION;
                    opcion.IDPREGUNTA = r.IDPREGUNTA;

                    pregunta.OPCION_PREGUNTA.Add(opcion);

                }


                preguntas.Add(pregunta);
            }
            ViewBag.preguntas = preguntas;
            TempData["preguntas"] = preguntas;//usaremos esta variable en el metodo post
            return View();
        }

        // POST: Examen/Guardar
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Guardar(FormCollection form)
        {
            USUARIO us = (USUARIO)Session["usuario"];
            int idPos = (from pos in db.POSTULANTE where pos.IDUSUARIO == us.IDUSUARIO select pos.IDPOSTULANTE).FirstOrDefault();
            RESPUESTA respuesta; 
            foreach (var pregunta in TempData["preguntas"] as IEnumerable<PREGUNTA>)
            {
                string auxiliar = "respuestas-"+pregunta.IDPREGUNTA;                
                string resp = form[auxiliar];
                    respuesta= new RESPUESTA();
                    respuesta.IDOPCIONPREGUNTA = Int16.Parse(resp);
                    respuesta.IDPOSTULANTE = idPos;//se obtendra de la session
                    db.RESPUESTA.Add(respuesta);
                    await db.SaveChangesAsync();

            }

              return RedirectToAction("Index");
            
        }

        // GET: Examen/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EXAMEN eXAMEN = await db.EXAMEN.FindAsync(id);
            if (eXAMEN == null)
            {
                return HttpNotFound();
            }
            ViewBag.TIPOPREGUNTA = new SelectList(db.TIPOPREGUNTA,"IDTIPOPREGUNTA","NOMBRETIPOPREGUNTA");
            var preguntas = from pregunta in db.PREGUNTA where pregunta.CODIGOEXAMEN == id select pregunta;
            ViewBag.preguntas = preguntas;
            return View(eXAMEN);
        }

        // GET: Examen/Create
        public ActionResult Create()
        {
            ViewBag.CODIGOEMPRESA = new SelectList(db.EMPRESA, "CODIGOEMPRESA", "NOMBREEMPRESA");
            ViewBag.IDTIPOEXAMEN = new SelectList(db.TIPO_EXAMEN, "IDTIPOEXAMEN", "DESCRIPCIONTIPO");
            return View();
        }

        // POST: Examen/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ACTIVO,PONDERACION,CODIGOEXAMEN,IDTIPOEXAMEN")] EXAMEN eXAMEN)
        {
            if (ModelState.IsValid)
            {
                USUARIO user = (USUARIO)Session["usuario"];
                eXAMEN.CODIGOEMPRESA = (int)user.CODIGOEMPRESA;
                db.EXAMEN.Add(eXAMEN);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CODIGOEMPRESA = new SelectList(db.EMPRESA, "CODIGOEMPRESA", "NOMBREEMPRESA", eXAMEN.CODIGOEMPRESA);
            ViewBag.IDTIPOEXAMEN = new SelectList(db.TIPO_EXAMEN, "IDTIPOEXAMEN", "DESCRIPCIONTIPO", eXAMEN.IDTIPOEXAMEN);
            return View(eXAMEN);
        }

        // GET: Examen/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EXAMEN eXAMEN = await db.EXAMEN.FindAsync(id);
            if (eXAMEN == null)
            {
                return HttpNotFound();
            }
            ViewBag.CODIGOEMPRESA = new SelectList(db.EMPRESA, "CODIGOEMPRESA", "NOMBREEMPRESA", eXAMEN.CODIGOEMPRESA);
            ViewBag.IDTIPOEXAMEN = new SelectList(db.TIPO_EXAMEN, "IDTIPOEXAMEN", "DESCRIPCIONTIPO", eXAMEN.IDTIPOEXAMEN);
            return View(eXAMEN);
        }

        // POST: Examen/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CODIGOEMPRESA,ACTIVO,PONDERACION,CODIGOEXAMEN,IDTIPOEXAMEN")] EXAMEN eXAMEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eXAMEN).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CODIGOEMPRESA = new SelectList(db.EMPRESA, "CODIGOEMPRESA", "NOMBREEMPRESA", eXAMEN.CODIGOEMPRESA);
            ViewBag.IDTIPOEXAMEN = new SelectList(db.TIPO_EXAMEN, "IDTIPOEXAMEN", "DESCRIPCIONTIPO", eXAMEN.IDTIPOEXAMEN);
            return View(eXAMEN);
        }

        // GET: Examen/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EXAMEN eXAMEN = await db.EXAMEN.FindAsync(id);
            if (eXAMEN == null)
            {
                return HttpNotFound();
            }
            return View(eXAMEN);
        }

        // POST: Examen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            EXAMEN eXAMEN = await db.EXAMEN.FindAsync(id);
            db.EXAMEN.Remove(eXAMEN);
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
