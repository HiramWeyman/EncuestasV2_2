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
    public class DeptoController : Controller
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
        }

        // GET: Depto
        public ActionResult Index()
        {
            List<encuesta__departamentoCLS> listaDepto = null;
            using (var db = new csstdura_encuestaEntities())
            {
                 listaDepto = (from dep in db.encuaesta_departamento
                                 join empresa in db.encuesta_empresa
                                 on dep.dep_empresa equals empresa.emp_id
                                 select new encuesta__departamentoCLS
                                 {
                                     dep_id = (int)dep.dep_id,
                                     dep_desc = dep.dep_desc,
                                     dep_empresa =(int) dep.dep_empresa,
                                     dep_empresa_desc = empresa.emp_descrip
               
                                 }).ToList();
            }
          
            return View(listaDepto);
                
        }

        // GET: Depto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //encuaesta_departamento encuaesta_departamento = db.encuaesta_departamento.Find(id

            List<encuesta__departamentoCLS> listaDepto = null;
            using (var db = new csstdura_encuestaEntities())
            {
                listaDepto = (from dep in db.encuaesta_departamento
                              join empresa in db.encuesta_empresa
                              on dep.dep_empresa equals empresa.emp_id
                              where dep.dep_id==id
                              select new encuesta__departamentoCLS
                              {
                                  dep_id = dep.dep_id,
                                  dep_desc = dep.dep_desc,
                                  dep_empresa = (int)dep.dep_empresa,
                                  dep_empresa_desc = empresa.emp_descrip

                              }).ToList();
            }
         
            if (listaDepto == null)
            {
                return HttpNotFound();
            }
            return View(listaDepto);
        }

        // GET: Depto/Create
        public ActionResult Create()
        {
            llenarEmpresa();
            ViewBag.listaEmpresa = listaEmpresa;
            return View();
        }

        // POST: Depto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "dep_id,dep_desc,dep_empresa")] encuaesta_departamento encuaesta_departamento)
        {
            if (ModelState.IsValid)
            {
                db.encuaesta_departamento.Add(encuaesta_departamento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(encuaesta_departamento);
        }

        // GET: Depto/Edit/5
        public ActionResult Edit(int id)
        {
            encuesta__departamentoCLS Oencuesta_departamentoCLS = new encuesta__departamentoCLS();

            using (var db = new csstdura_encuestaEntities())
            {
                encuaesta_departamento Oencuaesta_departamento = db.encuaesta_departamento.Where(p => p.dep_id.Equals(id)).First();
                Oencuesta_departamentoCLS.dep_id = Oencuaesta_departamento.dep_id;
                Oencuesta_departamentoCLS.dep_desc = Oencuaesta_departamento.dep_desc;
                Oencuesta_departamentoCLS.dep_empresa = (int)Oencuaesta_departamento.dep_empresa;

            }
            llenarEmpresa();
            ViewBag.listaEmpresa = listaEmpresa;
            return View(Oencuesta_departamentoCLS);
        }

        // POST: Depto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(encuesta__departamentoCLS Oencuesta__departamentoCLS)
        {
            if (!ModelState.IsValid)
            {
                return View(Oencuesta__departamentoCLS);
                //db.Entry(encuaesta_departamento).State = EntityState.Modified;: 'Sequence contains no elements'

                //db.SaveChanges();
               // return RedirectToAction("Index");
            }
            int id_departamento = Oencuesta__departamentoCLS.dep_id;
            
            using (var db = new csstdura_encuestaEntities())
            {
                encuaesta_departamento Oencuaesta_departamento = db.encuaesta_departamento.Where(p => p.dep_id.Equals(id_departamento)).First();
                Oencuaesta_departamento.dep_desc = Oencuesta__departamentoCLS.dep_desc;
                Oencuaesta_departamento.dep_empresa = Oencuesta__departamentoCLS.dep_empresa;
                db.SaveChanges();

            }
            return RedirectToAction("Index");

        }

        // GET: Depto/Delete/5
        public ActionResult Delete(int id)
        {
         List<encuesta__departamentoCLS> listaDepto = null;
            using (var db = new csstdura_encuestaEntities())
            {
                listaDepto = (from dep in db.encuaesta_departamento
                              join empresa in db.encuesta_empresa
                              on dep.dep_empresa equals empresa.emp_id
                              where dep.dep_id==id
                              select new encuesta__departamentoCLS
                              {
                                  dep_id = dep.dep_id,
                                  dep_desc = dep.dep_desc,
                                  dep_empresa = (int)dep.dep_empresa,
                                  dep_empresa_desc = empresa.emp_descrip

                              }).ToList();
            }
         
            if (listaDepto == null)
            {
                return HttpNotFound();
            }
            return View(listaDepto);
        }

        // POST: Depto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            encuaesta_departamento encuaesta_departamento = db.encuaesta_departamento.Find(id);
            db.encuaesta_departamento.Remove(encuaesta_departamento);
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
