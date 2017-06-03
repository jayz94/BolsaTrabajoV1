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
using System.Data.Entity.Infrastructure;

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
            ROL rOL = await db.ROL
                .Include(i => i.MENU)
                .Where(i => i.IDROL == id)
                .SingleAsync();
            PopulateMenusAsignadosViewModel(rOL);
            if (rOL == null)
            {
                return HttpNotFound();
            }
            //var todosmenus = from menu in db.MENU select menu;
            //ViewBag.menus = todosmenus;
            return View(rOL);
        }

        // POST: Rol/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDROL,NOMBREROL,ACTIVO")] ROL rOL, string[] selectedMenus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rOL).State = EntityState.Modified;
                UpdateMenusDelRol(selectedMenus, rOL);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            PopulateMenusAsignadosViewModel(rOL);
            return View(rOL);
        }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedMenus)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rolPorActualizar = db.ROL
               .Include(i => i.MENU)
               .Where(i => i.IDROL == id)
               .Single();

            if (TryUpdateModel(rolPorActualizar, "",
               new string[] { "NOMBREROL", "ACTIVO", "MENU" }))
            {
                try
                {
                    /*if (String.IsNullOrWhiteSpace(rolPorActualizar.OfficeAssignment.Location))
                    {
                        rolPorActualizar.OfficeAssignment = null;
                    }*/

                    UpdateMenusDelRol(selectedMenus, rolPorActualizar);

                    db.SaveChanges();
                    this.cargarMenus();
                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateMenusAsignadosViewModel(rolPorActualizar);
            return View(rolPorActualizar);
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

        public void PopulateMenusAsignadosViewModel(ROL rol)
        {
            var todosMenus = db.MENU;
            var menusAsignados = new HashSet<int>(rol.MENU.Select(m => m.IDMENU));
            var viewModel = new List<MenusAsignadosViewModel>();
            foreach(var menu in todosMenus)
            {
                viewModel.Add(new MenusAsignadosViewModel
                {
                    IDMENU = menu.IDMENU,
                    NOMBREMENU = menu.NOMBREMENU,
                    asignado = menusAsignados.Contains(menu.IDMENU)
                });
            }
            ViewBag.menus = viewModel;
        }

        private void UpdateMenusDelRol(string[] selectedMenus,ROL rolPorActualizar)
        {
            if(selectedMenus == null)
            {
                rolPorActualizar.MENU/*Clear();//*/ = new List<MENU>();
                return;
            }

            var selectedMenusHS = new HashSet<string>(selectedMenus);//seleccionados de la vista
            var rolMenus = new HashSet<int>(rolPorActualizar.MENU.Select(m => m.IDMENU));//set de menus que ya tiene el rol
            /*var rolMenus = new HashSet<int>();
            foreach(var item in rolPorActualizar.MENU)
            {
                rolMenus.Add(item.IDMENU);
            }*/
            foreach(var menu in db.MENU)
            {
                if (selectedMenusHS.Contains(menu.IDMENU.ToString()))
                {
                    if (!rolMenus.Contains(menu.IDMENU)/*rolMenus.Count< 1*/)//Si lo ha seleccionado en la vista pero no esta en los del rol, lo agrega
                    {
                        rolPorActualizar.MENU.Add(menu);
                    }
                }
                else
                {
                    if (rolMenus.Contains(menu.IDMENU))//si no esta en la vista pero si en los del rol, lo elimina
                    {
                        rolPorActualizar.MENU.Remove(menu);
                    }
                }
            }
        }

        public void cargarMenus()
        {
            Session["menus"] = null;
            var rol = 5;//obtenemos el rol
            var query = from menu in db.MENU
                        where menu.ROL.Any(m => m.IDROL == rol)
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
        /*
        protected async Task<ActionResult> verMenus(int idRol) 
        {
            var menus = from menus in db.ROL where menus.IDROL = (short)idRol select menus;
            return RedirectToAction("Index");
        }*/
    }
}
