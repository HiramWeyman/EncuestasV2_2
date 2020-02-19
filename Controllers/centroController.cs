﻿using System;
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
    public class CentroController : Controller
    {
        private csstdura_encuestaEntities db = new csstdura_encuestaEntities();

        // GET: Centro
        public async Task<ActionResult> Index()
        {
            return View(await db.encuaesta_centro.ToListAsync());
        }

        // GET: Centro/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            encuaesta_centro encuaesta_centro = await db.encuaesta_centro.FindAsync(id);
            if (encuaesta_centro == null)
            {
                return HttpNotFound();
            }
            return View(encuaesta_centro);
        }

        // GET: Centro/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Centro/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "centro_id,centro_desc")] encuaesta_centro encuaesta_centro)
        {
            if (ModelState.IsValid)
            {
                db.encuaesta_centro.Add(encuaesta_centro);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(encuaesta_centro);
        }

        // GET: Centro/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            encuaesta_centro encuaesta_centro = await db.encuaesta_centro.FindAsync(id);
            if (encuaesta_centro == null)
            {
                return HttpNotFound();
            }
            return View(encuaesta_centro);
        }

        // POST: Centro/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "centro_id,centro_desc")] encuaesta_centro encuaesta_centro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(encuaesta_centro).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(encuaesta_centro);
        }

        // GET: Centro/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            encuaesta_centro encuaesta_centro = await db.encuaesta_centro.FindAsync(id);
            if (encuaesta_centro == null)
            {
                return HttpNotFound();
            }
            return View(encuaesta_centro);
        }

        // POST: Centro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            encuaesta_centro encuaesta_centro = await db.encuaesta_centro.FindAsync(id);
            db.encuaesta_centro.Remove(encuaesta_centro);
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
