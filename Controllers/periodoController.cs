using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EncuestasV2.Models;
using EncuestasV2.Filters;


namespace EncuestasV2.Controllers
{
    [AccederAdmin]
    public class PeriodoController : Controller
    {
        private csstdura_encuestaEntities db = new csstdura_encuestaEntities();

        // GET: Periodo
        public async Task<ActionResult> Index()
        {
            return View(await db.encuaesta_periodo.ToListAsync());
        }

        // GET: Periodo/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            encuaesta_periodo encuaesta_periodo = await db.encuaesta_periodo.FindAsync(id);
            if (encuaesta_periodo == null)
            {
                return HttpNotFound();
            }
            return View(encuaesta_periodo);
        }

        // GET: Periodo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Periodo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "periodo_id,periodo_desc,periodo_estatus")] encuaesta_periodo encuaesta_periodo)
        {
            if (ModelState.IsValid)
            {
                db.encuaesta_periodo.Add(encuaesta_periodo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(encuaesta_periodo);
        }

        // GET: Periodo/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            encuaesta_periodo encuaesta_periodo = await db.encuaesta_periodo.FindAsync(id);
            if (encuaesta_periodo == null)
            {
                return HttpNotFound();
            }
            return View(encuaesta_periodo);
        }

        // POST: Periodo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "periodo_id,periodo_desc,periodo_estatus")] encuaesta_periodo encuaesta_periodo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(encuaesta_periodo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(encuaesta_periodo);
        }

        // GET: Periodo/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            encuaesta_periodo encuaesta_periodo = await db.encuaesta_periodo.FindAsync(id);
            if (encuaesta_periodo == null)
            {
                return HttpNotFound();
            }
            return View(encuaesta_periodo);
        }

        // POST: Periodo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            encuaesta_periodo encuaesta_periodo = await db.encuaesta_periodo.FindAsync(id);
            db.encuaesta_periodo.Remove(encuaesta_periodo);
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
