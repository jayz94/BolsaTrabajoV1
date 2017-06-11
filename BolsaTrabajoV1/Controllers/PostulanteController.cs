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
using System.Data.Entity.Core.Objects;

namespace BolsaTrabajoV1.Controllers
{
    public class PostulanteController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();
        
        // GET: Postulante
        public async Task<ActionResult> Index()
        {
            ViewBag.departamentos = db.DEPARTAMENTO;
            ViewBag.areasConocimiento = db.AREA_CONOCIMIENTO;
            ViewBag.tiposHabilidades = db.TIPO_HABILIDAD;
            ViewBag.generos = db.GENERO;
            ViewBag.tiposFormaciones = db.TIPOFORMACINONACADEMICA;
            ViewBag.idiomas = db.IDIOMA;
            return View(await db.ViewPostulante.ToListAsync());
        }

        //GET: FiltrarPostulante
        public async Task<ActionResult> FiltrarPostulante(int idTabla, int idRegistro) {
            //tabla 1: departamento            //tabla 2: idioma            //tabla 3: area conocimiento
            //tabla 4: tipo habilidad          //tabla 5: formacion academica   //tabla 6: genero
            ViewBag.departamentos = db.DEPARTAMENTO;
            ViewBag.areasConocimiento = db.AREA_CONOCIMIENTO;
            ViewBag.tiposHabilidades = db.TIPO_HABILIDAD;
            ViewBag.generos = db.GENERO;
            ViewBag.tiposFormaciones = db.TIPOFORMACINONACADEMICA;
            ViewBag.idiomas = db.IDIOMA;
            dynamic postulantes;//MUY CURIOSO LA FORMA EN COMO SE DEFINE ESTA VARIABLE COMO TIPO DE DATOS DINAMICA QUE PUEDE TOMAR CUALQUIER VALOR

            switch (idTabla)
            {
                case 1:
                    postulantes = from pos in db.ViewPostulante
                                  join curr in db.ViewCurriculum on pos.idpostulante equals curr.IDPOSTULANTE
                                  where pos.iddep ==idRegistro
                                  select new
                                  {
                                      pos.nombre,
                                      pos.genero,
                                      pos.Edad,
                                      curr.INSTITUCION,
                                      curr.NOMBREHABILIDAD,
                                      curr.NOMBREIDIOMA,
                                      curr.TITULO,
                                      pos.municipio
                                  };
                    break;
                case 2:
                    IDIOMA idioma = await db.IDIOMA.FindAsync(idRegistro);
                    postulantes = from pos in db.ViewPostulante
                                  join curr in db.ViewCurriculum on pos.idpostulante equals curr.IDPOSTULANTE
                                  where curr.idacth == idRegistro || curr.NOMBREIDIOMA.ToLower() == idioma.NOMBREIDIOMA.ToLower()
                                  select new
                                  {
                                      pos.nombre,
                                      pos.genero,
                                      pos.Edad,
                                      curr.INSTITUCION,
                                      curr.NOMBREHABILIDAD,
                                      curr.NOMBREIDIOMA,
                                      curr.TITULO,
                                      pos.municipio
                                  };
                    break;
                case 3:
                    postulantes = from pos in db.ViewPostulante
                                  join curr in db.ViewCurriculum on pos.idpostulante equals curr.IDPOSTULANTE
                                  where curr.idacth == idRegistro || curr.idaccr==idRegistro
                                  select new
                                  {
                                      pos.nombre,
                                      pos.genero,
                                      pos.Edad,
                                      curr.INSTITUCION,
                                      curr.NOMBREHABILIDAD,
                                      curr.NOMBREIDIOMA,
                                      curr.TITULO,
                                      pos.municipio
                                  };
                    break;
                case 4:
                    postulantes = from pos in db.ViewPostulante
                                  join curr in db.ViewCurriculum on pos.idpostulante equals curr.IDPOSTULANTE
                                  where curr.IDTIPOHABILIDAD == idRegistro
                                  select new
                                  {
                                      pos.nombre,
                                      pos.genero,
                                      pos.Edad,
                                      curr.INSTITUCION,
                                      curr.NOMBREHABILIDAD,
                                      curr.NOMBREIDIOMA,
                                      curr.TITULO,
                                      pos.municipio
                                  };
                    break;
                case 5:
                    postulantes = from pos in db.ViewPostulante
                                  join curr in db.ViewCurriculum on pos.idpostulante equals curr.IDPOSTULANTE
                                  where curr.IDTIPOFORMACION == idRegistro
                                  select new
                                  {
                                      pos.nombre,
                                      pos.genero,
                                      pos.Edad,
                                      curr.INSTITUCION,
                                      curr.NOMBREHABILIDAD,
                                      curr.NOMBREIDIOMA,
                                      curr.TITULO,
                                      pos.municipio
                                  };
                    break;
                case 6:
                    postulantes = from pos in db.ViewPostulante
                                  join curr in db.ViewCurriculum on pos.idpostulante equals curr.IDPOSTULANTE
                                  where pos.idgenero == idRegistro
                                  select new
                                  {
                                      pos.nombre,
                                      pos.genero,
                                      pos.Edad,
                                      curr.INSTITUCION,
                                      curr.NOMBREHABILIDAD,
                                      curr.NOMBREIDIOMA,
                                      curr.TITULO,
                                      pos.municipio
                                  };
                    break;
                default:
                    postulantes = from pos in db.ViewPostulante
                                  join curr in db.ViewCurriculum on pos.idpostulante equals curr.IDPOSTULANTE
                                  select new
                                  {
                                      pos.nombre,
                                      pos.genero,
                                      pos.Edad,
                                      curr.INSTITUCION,
                                      curr.NOMBREHABILIDAD,
                                      curr.NOMBREIDIOMA,
                                      curr.TITULO,
                                      pos.municipio
                                  };
                    break;
            }


            List<PostulanteCurriculum> listaPostulantes=new List<PostulanteCurriculum>();
            foreach(var postulante in postulantes)
            {
                PostulanteCurriculum postCurr = new PostulanteCurriculum();
                postCurr.nombre = postulante.nombre;
                postCurr.genero = postulante.genero;
                postCurr.edad =(int) postulante.Edad;
                postCurr.institucion = postulante.INSTITUCION;
                postCurr.habilidad = postulante.NOMBREHABILIDAD;
                postCurr.idioma = postulante.NOMBREIDIOMA;
                postCurr.titulo = postulante.TITULO;
                postCurr.municipio = postulante.municipio;
                //agregamos a la lista el objeto creado 
                listaPostulantes.Add(postCurr);
            }
            return View(listaPostulantes.ToList());
        }

        // GET: Postulante/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POSTULANTE pOSTULANTE = await db.POSTULANTE.FindAsync(id);
            if (pOSTULANTE == null)
            {
                return HttpNotFound();
            }
            return View(pOSTULANTE);
        }

        // GET: Postulante/Create
        public ActionResult Create()
        {
            ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM");
            ViewBag.IDMUNICIPIO = new SelectList(db.MUNICIPIO, "IDMUNICIPIO", "NOMBREMUNICIPIO");
            ViewBag.IDUSUARIO = new SelectList(db.USUARIO, "IDUSUARIO", "NOMBREUSUARIO");
            ViewBag.IDGENERO = new SelectList(db.GENERO, "IDGENERO", "DESCRIPCIONGENERO");
            return View();
        }

        // POST: Postulante/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDPOSTULANTE,IDUSUARIO,IDMUNICIPIO,IDCURRICULUM,NOMBREPOSTULANTE,APELLIDOPOSTULANTE,IDGENERO,FECHANACIMIENTO,DIRECCION,URLCURRICULUM")] POSTULANTE pOSTULANTE)
        {
            if (ModelState.IsValid)
            {
                USUARIO us = new USUARIO();
                us = (USUARIO)Session["usuario"];
                pOSTULANTE.IDUSUARIO = us.IDUSUARIO;//(int)Session["idUs"];
                db.POSTULANTE.Add(pOSTULANTE);
                await db.SaveChangesAsync();
                //int id = pOSTULANTE.IDPOSTULANTE; // recuperar el id de postulante
                //busco usuario por el id de session
                USUARIO uSUARIO = await db.USUARIO.FindAsync(1/*(int)Session["idUs"]*/); 
                //uSUARIO.IDPOSTULANTE =id;
                db.Entry(uSUARIO).State = EntityState.Modified;
                await db.SaveChangesAsync();
                //creo el curriculum de postulante
                CURRICULUM curriculum = new CURRICULUM();
                curriculum.IDPOSTULANTE = uSUARIO.IDUSUARIO;//le asigno el id de postulante a curriculum
                //guardo el curriculum
                db.CURRICULUM.Add(curriculum);
                await db.SaveChangesAsync();
                //obtengo el id del curriculum guardado y lo guardo en session
                int idCurriculum = curriculum.IDCURRICULUM;
                Session["idCurriculum"] = idCurriculum;
                Session["idPostulante"] = curriculum.IDPOSTULANTE;
                return RedirectToAction("mainCurriculum","Curriculum");
            }

            //ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM", pOSTULANTE.IDCURRICULUM);
            ViewBag.IDMUNICIPIO = new SelectList(db.MUNICIPIO, "IDMUNICIPIO", "NOMBREMUNICIPIO", pOSTULANTE.IDMUNICIPIO);
            ViewBag.IDUSUARIO = new SelectList(db.USUARIO, "IDUSUARIO", "NOMBREUSUARIO", pOSTULANTE.IDUSUARIO);
            ViewBag.IDGENERO = new SelectList(db.GENERO, "IDGENERO", "DESCRIPCIONGENERO");
            return View(pOSTULANTE);
        }

        // GET: Postulante/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POSTULANTE pOSTULANTE = await db.POSTULANTE.FindAsync(id);
            if (pOSTULANTE == null)
            {
                return HttpNotFound();
            }
            //ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM", pOSTULANTE.IDCURRICULUM);
            ViewBag.IDMUNICIPIO = new SelectList(db.MUNICIPIO, "IDMUNICIPIO", "NOMBREMUNICIPIO", pOSTULANTE.IDMUNICIPIO);
            ViewBag.IDUSUARIO = new SelectList(db.USUARIO, "IDUSUARIO", "NOMBREUSUARIO", pOSTULANTE.IDUSUARIO);
            ViewBag.IDGENERO = new SelectList(db.GENERO, "IDGENERO", "DESCRIPCIONGENERO",pOSTULANTE.IDGENERO);
            return View(pOSTULANTE);
        }

        // POST: Postulante/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDPOSTULANTE,IDUSUARIO,IDMUNICIPIO,IDCURRICULUM,NOMBREPOSTULANTE,APELLIDOPOSTULANTE,IDGENERO,FECHANACIMIENTO,DIRECCION,URLCURRICULUM")] POSTULANTE pOSTULANTE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pOSTULANTE).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //ViewBag.IDCURRICULUM = new SelectList(db.CURRICULUM, "IDCURRICULUM", "IDCURRICULUM", pOSTULANTE.IDCURRICULUM);
            ViewBag.IDMUNICIPIO = new SelectList(db.MUNICIPIO, "IDMUNICIPIO", "NOMBREMUNICIPIO", pOSTULANTE.IDMUNICIPIO);
            ViewBag.IDUSUARIO = new SelectList(db.USUARIO, "IDUSUARIO", "NOMBREUSUARIO", pOSTULANTE.IDUSUARIO);
            ViewBag.IDGENERO = new SelectList(db.GENERO, "IDGENERO", "DESCRIPCIONGENERO");
            return View(pOSTULANTE);
        }

        // GET: Postulante/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POSTULANTE pOSTULANTE = await db.POSTULANTE.FindAsync(id);
            if (pOSTULANTE == null)
            {
                return HttpNotFound();
            }
            return View(pOSTULANTE);
        }

        // POST: Postulante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            POSTULANTE pOSTULANTE = await db.POSTULANTE.FindAsync(id);
            db.POSTULANTE.Remove(pOSTULANTE);
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
