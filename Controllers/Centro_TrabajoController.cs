using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EncuestasV2.Models;

namespace EncuestasV2.Controllers
{
    public class Centro_TrabajoController : Controller
    {
        private csstdura_encuestaEntities db = new csstdura_encuestaEntities();

        // GET: Centro_Trabajo
        public ActionResult Index()
        {
            return View(db.encuaesta_centro.ToList());
        }

        // GET: Centro_Trabajo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            encuaesta_centro encuaesta_centro = db.encuaesta_centro.Find(id);
            if (encuaesta_centro == null)
            {
                return HttpNotFound();
            }
            return View(encuaesta_centro);
        }

        // GET: Centro_Trabajo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Centro_Trabajo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "centro_id,centro_desc,centro_empresa,centro_depto")] encuaesta_centro encuaesta_centro)
        {
            if (ModelState.IsValid)
            {
                db.encuaesta_centro.Add(encuaesta_centro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(encuaesta_centro);
        }

        // GET: Centro_Trabajo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            encuaesta_centro encuaesta_centro = db.encuaesta_centro.Find(id);
            if (encuaesta_centro == null)
            {
                return HttpNotFound();
            }
            return View(encuaesta_centro);
        }

        // POST: Centro_Trabajo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "centro_id,centro_desc,centro_empresa,centro_depto")] encuaesta_centro encuaesta_centro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(encuaesta_centro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(encuaesta_centro);
        }

        // GET: Centro_Trabajo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            encuaesta_centro encuaesta_centro = db.encuaesta_centro.Find(id);
            if (encuaesta_centro == null)
            {
                return HttpNotFound();
            }
            return View(encuaesta_centro);
        }

        // POST: Centro_Trabajo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            encuaesta_centro encuaesta_centro = db.encuaesta_centro.Find(id);
            db.encuaesta_centro.Remove(encuaesta_centro);
            db.SaveChanges();
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
