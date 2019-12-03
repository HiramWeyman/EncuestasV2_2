using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EncuestasV2.Models;
using EncuestasV2.Filters;
using System.Transactions;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace EncuestasV2.Controllers
{

    [Acceder]
    public class EncuestaController : Controller
    {
        // GET: Encuesta
        public ActionResult Index(string user)
        {
            if (user != null)
            {
                ViewBag.user = user.ToString();

                List<encuesta_mostrarPreguntasCLS> list;
                using (var db = new csstdura_encuestaEntities())
                {
                    //hacemos un select a nuestra tabla con los campos que queremos mostrar
                    list = (from preguntas in db.encuesta_det_encuesta
                            where preguntas.denc_parte == 1
                            select new encuesta_mostrarPreguntasCLS
                            {
                                denc_id = preguntas.denc_id,
                                denc_descrip = preguntas.denc_descrip,
                                denc_valor_1 = preguntas.denc_valor_1,
                                denc_valor_2 = preguntas.denc_valor_2,
                            }).ToList();

                    string encabezado = db.Database.SqlQuery<string>("select encu_descrip from encuesta_encuesta where encu_id = 1").FirstOrDefault();
                    int id_encabezado = db.Database.SqlQuery<int>("select encu_id from encuesta_encuesta where encu_id = 1").FirstOrDefault();
                    int id_empresa = db.Database.SqlQuery<int>("select usua_empresa from encuesta_usuarios where usua_n_usuario = '"+ user + "'").FirstOrDefault();
                    string nombreEmpleado = db.Database.SqlQuery<string>("select usua_nombre from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
                    int id_usuario = db.Database.SqlQuery<int>("select usua_id from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();

                    ViewBag.encabezado = encabezado;
                    ViewBag.id_encabezado = id_encabezado;
                    ViewBag.id_empresa = id_empresa;
                    ViewBag.nombreEmpleado = nombreEmpleado;
                    ViewBag.id_usuario = id_usuario;
                }
                return View(list);
            }
            else {
                return RedirectToAction("Index", "Login");
                //return RedirectToAction("Reporting", "ReportManagement", new { area = "Admin" })
            }
            
       

        }



        public ActionResult Agregar()
        {
            return View();
        }
 
        [HttpPost]
        public ActionResult Agregar(encuesta_mostrarPreguntasCLS Oencuesta_mostrarPreguntasCLS)
        {
            String Usuario = Request.Form["user"];
            int res = 0;
            int bandera = 0;
            using (var db = new csstdura_encuestaEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    int id_empresa = db.Database.SqlQuery<int>("select usua_empresa from encuesta_usuarios where usua_n_usuario = '" + Usuario + "'").FirstOrDefault();
                    String num_empleados = db.Database.SqlQuery<String>("select emp_no_trabajadores from encuesta_empresa where emp_id = '" + id_empresa + "'").FirstOrDefault();

                    try
                    {
                        for (int x = 1; x<7; x++)
                        {
                            //var nombreVariable = "radio_"+x;
                            encuesta_resultados resultado = new encuesta_resultados();
                            resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                            resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                            resultado.resu_denc_id = int.Parse(Request.Form["denc_id_" + x]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                            resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                            resultado.resu_resultado = Request.Form["Valor_radio_"+x];
                            if (Request.Form["Valor_radio_" + x].Equals("SI"))
                            {
                                bandera = 1;
                            }
                            resultado.resu_fecha = DateTime.Now;
                            db.encuesta_resultados.Add(resultado);
                            res = db.SaveChanges();
                        }
                        
                        transaction.Complete();
                    }
                    catch (DbEntityValidationException dbEx)
                    {

                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                Trace.TraceInformation("Property: {0} Error: {1}",
                                    validationError.PropertyName,
                                    validationError.ErrorMessage);
                            }
                        }

                    }
                    if (res == 1)
                    {
                        if (bandera == 1)
                        {
                            return Content("<script language='javascript' type='text/javascript'>alert('Registro exitoso!');window.location = '/Encuesta2/Index?user=" + Usuario + " ';</script>");
                        }
                        else
                        {

                            if (int.Parse(num_empleados) < 51)
                            {
                                return Content("<script language='javascript' type='text/javascript'>alert('Registro exitoso!');window.location = '/Encuesta3/Index5?user=" + Usuario + " ';</script>");
                            }
                            {
                                return Content("<script language='javascript' type='text/javascript'>alert('Registro exitoso!');window.location = '/Encuesta3/Index13?user=" + Usuario + " ';</script>");
                            }
                        }

                    }
                    else
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Ocurrio un error!');window.location = '/Admin/CatalogoEmpresa';</script>");

                    }

                }
            }

        }

    }

    
}