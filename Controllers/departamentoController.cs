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

namespace EncuestasV2.Controllers
{
    public class departamentoController : Controller
    {
        private csstdura_encuestaEntities db = new csstdura_encuestaEntities();

        // GET: departamento
        public async Task<ActionResult> Index()
        {
            return View(await db.encuaesta_departamento.ToListAsync());
        }

        // GET: departamento/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            encuaesta_departamento encuaesta_departamento = await db.encuaesta_departamento.FindAsync(id);
            if (encuaesta_departamento == null)
            {
                return HttpNotFound();
            }
            return View(encuaesta_departamento);
        }

        // GET: departamento/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: departamento/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "dep_id,dep_desc")] encuaesta_departamento encuaesta_departamento)
        {
            if (ModelState.IsValid)
            {
                db.encuaesta_departamento.Add(encuaesta_departamento);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(encuaesta_departamento);
        }

        // GET: departamento/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            encuaesta_departamento encuaesta_departamento = await db.encuaesta_departamento.FindAsync(id);
            if (encuaesta_departamento == null)
            {
                return HttpNotFound();
            }
            return View(encuaesta_departamento);
        }

        // POST: departamento/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "dep_id,dep_desc")] encuaesta_departamento encuaesta_departamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(encuaesta_departamento).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(encuaesta_departamento);
        }

        // GET: departamento/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            encuaesta_departamento encuaesta_departamento = await db.encuaesta_departamento.FindAsync(id);
            if (encuaesta_departamento == null)
            {
                return HttpNotFound();
            }
            return View(encuaesta_departamento);
        }

        // POST: departamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            encuaesta_departamento encuaesta_departamento = await db.encuaesta_departamento.FindAsync(id);
            db.encuaesta_departamento.Remove(encuaesta_departamento);
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
