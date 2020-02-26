using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EncuestasV2.Models;
using EncuestasV2.Filters;


namespace EncuestasV2.Controllers
{
    [AccederAdmin]
    public class Centro_TrabajoController : Controller
    {
        private csstdura_encuestaEntities db = new csstdura_encuestaEntities();
        List<SelectListItem> listaEmpresa;
         
        private void llenarEmpresa()
        {
            using (var db = new csstdura_encuestaEntities())
            {
                listaEmpresa = (from emp in db.encuesta_empresa
                                select new SelectListItem
                                {
                                    Value = emp.emp_id.ToString(),
                                    Text = emp.emp_descrip,
                                    Selected = false

                                }).ToList();
                listaEmpresa.Insert(0, new SelectListItem { Text = "Seleccione", Value = "" });
            }
            ViewBag.listaEmpresa = listaEmpresa;

        }

        public JsonResult llenarDepto(int empresa) {

            List<encuaesta_departamento> listaDepto = db.encuaesta_departamento.Where(x => x.dep_empresa == empresa).ToList();
            return Json(listaDepto, JsonRequestBehavior.AllowGet);
        }
 

        // GET: Centro_Trabajo
        public ActionResult Index()
        {
            //return View(db.encuaesta_centro.ToList());

            List<encuesta_centroCLS> listaCentro;
            using (var db = new csstdura_encuestaEntities())
            {
                listaCentro = (from cen in db.encuaesta_centro
                              join empresa in db.encuesta_empresa
                              on cen.centro_empresa equals empresa.emp_id
                              join dep in db.encuaesta_departamento
                              on cen.centro_depto equals dep.dep_id
                              select new encuesta_centroCLS
                              {
                                  centro_id = (int)cen.centro_id,
                                  centro_desc = cen.centro_desc,
                                  centro_empresa_desc = empresa.emp_descrip,
                                  centro_depto_desc = dep.dep_desc

                              }).ToList();
            }

            return View(listaCentro);
        }

        // GET: Centro_Trabajo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //encuaesta_departamento encuaesta_departamento = db.encuaesta_departamento.Find(id

            List<encuesta_centroCLS> listaCentro = null;
            using (var db = new csstdura_encuestaEntities())
            {
                listaCentro = (from cen in db.encuaesta_centro
                              join empresa in db.encuesta_empresa
                              on cen.centro_empresa equals empresa.emp_id
                              join dep in db.encuaesta_departamento
                              on cen.centro_depto equals dep.dep_id
                              where cen.centro_id == id
                              select new encuesta_centroCLS
                              {
                                  centro_id = cen.centro_id,
                                  centro_depto = cen.centro_depto,
                                  centro_empresa_desc =empresa.emp_descrip,
                                  centro_depto_desc = dep.dep_desc

                              }).ToList();
            }

            if (listaCentro == null)
            {
                return HttpNotFound();
            }
            return View(listaCentro);
        }

        // GET: Centro_Trabajo/Create
        public ActionResult Create()
        {
            llenarEmpresa();
            //llenarDepto(empresa);
            //ViewBag.listaEmpresa = listaEmpresa;
            //ViewBag.listaDepto = listaDepto;
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
        public ActionResult Edit(int id)
        {
            encuesta_centroCLS Oencuesta_centroCLS = new encuesta_centroCLS();
            List<SelectListItem> listaDepto;
            using (var db = new csstdura_encuestaEntities())
            {
                encuaesta_centro Oencuaesta_centro = db.encuaesta_centro.Where(p => p.centro_id.Equals(id)).First();
                Oencuesta_centroCLS.centro_id = Oencuaesta_centro.centro_id;
                Oencuesta_centroCLS.centro_empresa = Oencuaesta_centro.centro_empresa;
                Oencuesta_centroCLS.centro_depto = Oencuaesta_centro.centro_depto;
                Oencuesta_centroCLS.centro_desc = Oencuaesta_centro.centro_desc;

                listaDepto = (from cen in db.encuaesta_centro
                              join dep in db.encuaesta_departamento
                              on cen.centro_depto equals dep.dep_id
                              where cen.centro_id==id
                              select new SelectListItem
                              {
                                  Value = dep.dep_id.ToString(),
                                  Text = dep.dep_desc,
                                  Selected = false

                              }).ToList();
                listaDepto.Insert(0, new SelectListItem { Text = "Seleccione", Value = "" });
            }
            llenarEmpresa();
      
            ViewBag.listaEmpresa = listaEmpresa;
            ViewBag.listaDepto = listaDepto;
            return View(Oencuesta_centroCLS);
        }

        // POST: Centro_Trabajo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(encuesta_centroCLS Oencuesta_centroCLS)
        {
            if (!ModelState.IsValid)
            {
                return View(Oencuesta_centroCLS);

            }
            int id_centro = Oencuesta_centroCLS.centro_id;

            using (var db = new csstdura_encuestaEntities())
            {
                encuaesta_centro Oencuaesta_centro = db.encuaesta_centro.Where(p => p.centro_id.Equals(id_centro)).First();
                Oencuaesta_centro.centro_desc = Oencuesta_centroCLS.centro_desc;
                Oencuaesta_centro.centro_empresa = Oencuesta_centroCLS.centro_empresa;
                Oencuaesta_centro.centro_depto = Oencuesta_centroCLS.centro_depto;
                db.SaveChanges();

            }
            return RedirectToAction("Index");
        }

        // GET: Centro_Trabajo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<encuesta_centroCLS> listaCentro = null;
            using (var db = new csstdura_encuestaEntities())
            {
                listaCentro = (from cen in db.encuaesta_centro
                               join empresa in db.encuesta_empresa
                               on cen.centro_empresa equals empresa.emp_id
                               join dep in db.encuaesta_departamento
                               on cen.centro_depto equals dep.dep_id
                               where cen.centro_id == id
                               select new encuesta_centroCLS
                               {
                                   centro_id = cen.centro_id,
                                   centro_depto = cen.centro_depto,
                                   centro_empresa_desc = empresa.emp_descrip,
                                   centro_depto_desc = dep.dep_desc

                               }).ToList();
            }

            if (listaCentro == null)
            {
                return HttpNotFound();
            }
            return View(listaCentro);
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
