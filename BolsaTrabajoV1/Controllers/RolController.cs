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
    public class RolController : Controller
    {
        private ConexionBDBolsa db = new ConexionBDBolsa();

        // GET: Rol
        public async Task<ActionResult> Index()
        {
            return View(await db.ROL.ToListAsync());
        }

        // GET: Rol/Details/5
        public async Task<ActionResult> Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ROL rOL = await db.ROL.FindAsync(id);
            if (rOL == null)
            {
                return HttpNotFound();
            }
            return View(rOL);
        }

        // GET: Rol/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rol/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDROL,NOMBREROL,ACTIVO")] ROL rOL)
        {
            if (ModelState.IsValid)
            {
                var menus = from menu in db.MENU where menu.IDMENU < 3 select menu;
                foreach(var item in menus)
                {
                    rOL.MENU.Add(item);
                }                
                db.ROL.Add(rOL);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(rOL);
        }

        // GET: Rol/Edit/5
        public async Task<ActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ROL rOL = await db.ROL.FindAsync(id);
            if (rOL == null)
            {
                return HttpNotFound();
            }
            var todosmenus = from menu in db.MENU select menu;
            ViewBag.menus = todosmenus;
            return View(rOL);
        }

        // POST: Rol/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDROL,NOMBREROL,ACTIVO")] ROL rOL)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rOL).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(rOL);
        }

        // GET: Rol/Delete/5
        public async Task<ActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ROL rOL = await db.ROL.FindAsync(id);
            if (rOL == null)
            {
                return HttpNotFound();
            }
            return View(rOL);
        }

        // POST: Rol/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(short id)
        {
            ROL rOL = await db.ROL.FindAsync(id);
            db.ROL.Remove(rOL);
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
        /*
        protected async Task<ActionResult> verMenus(int idRol) 
        {
            var menus = from menus in db.ROL where menus.IDROL = (short)idRol select menus;
            return RedirectToAction("Index");
        }*/
    }
}
