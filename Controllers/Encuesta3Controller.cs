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
    public class Encuesta3Controller : Controller
    {

        // GET: Encuesta2
        public ActionResult Index(string user)
        {
            if (user != null)
            {

                ViewBag.user = user.ToString();

                List<encuesta_mostrarPreguntas2CLS> list;
                using (var db = new csstdura_encuestaEntities())
                {
                    //hacemos un select a nuestra tabla con los campos que queremos mostrar
                    list = (from preguntas in db.encuesta_det_encuesta
                            where preguntas.denc_parte == 3
                            select new encuesta_mostrarPreguntas2CLS
                            {
                                denc_id = preguntas.denc_id,
                                denc_descrip = preguntas.denc_descrip,
                                denc_valor_1 = preguntas.denc_valor_1,
                                denc_valor_2 = preguntas.denc_valor_2,
                            }).ToList();

                    string encabezado = db.Database.SqlQuery<string>("select encu_descrip from encuesta_encuesta where encu_id = 1").FirstOrDefault();
                    int id_encabezado = db.Database.SqlQuery<int>("select encu_id from encuesta_encuesta where encu_id = 1").FirstOrDefault();
                    int id_empresa = db.Database.SqlQuery<int>("select usua_empresa from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
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
            else
            {
                return RedirectToAction("Index", "Login");
                //return RedirectToAction("Reporting", "ReportManagement", new { area = "Admin" })
            }



        }

        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar(encuesta_mostrarPreguntas2CLS Oencuesta_mostrarPreguntasCLS)
        {
            String Usuario = Request.Form["user"];
            int res = 0;
            using (var db = new csstdura_encuestaEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {
                        for (int x = 1; x < 8; x++)
                        {
                            //var nombreVariable = "radio_"+x;
                            encuesta_resultados resultado = new encuesta_resultados();
                            resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                            resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                            resultado.resu_denc_id = int.Parse(Request.Form["denc_id_" + x]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                            resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                            resultado.resu_resultado = Request.Form["Valor_radio_" + x];
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

                        return Content("<script language='javascript' type='text/javascript'>alert('Registro exitoso!');window.location = '/Encuesta3/Index4?user=" + Usuario + " ';</script>");

                    }
                    else
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Ocurrio un error!');window.location = '/Admin/CatalogoEmpresa';</script>");

                    }

                }
            }

        }

        public ActionResult Index4(string user)
        {
            if (user != null)
            {

                ViewBag.user = user.ToString();

                List<encuesta_mostrarPreguntas2CLS> list;
                using (var db = new csstdura_encuestaEntities())
                {
                    //hacemos un select a nuestra tabla con los campos que queremos mostrar
                    list = (from preguntas in db.encuesta_det_encuesta
                            where preguntas.denc_parte == 4
                            select new encuesta_mostrarPreguntas2CLS
                            {
                                denc_id = preguntas.denc_id,
                                denc_descrip = preguntas.denc_descrip,
                                denc_valor_1 = preguntas.denc_valor_1,
                                denc_valor_2 = preguntas.denc_valor_2,
                            }).ToList();

                    string encabezado = db.Database.SqlQuery<string>("select encu_descrip from encuesta_encuesta where encu_id = 1").FirstOrDefault();
                    int id_encabezado = db.Database.SqlQuery<int>("select encu_id from encuesta_encuesta where encu_id = 1").FirstOrDefault();
                    int id_empresa = db.Database.SqlQuery<int>("select usua_empresa from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
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
            else
            {
                return RedirectToAction("Index", "Login");
                //return RedirectToAction("Reporting", "ReportManagement", new { area = "Admin" })
            }



        }

        public ActionResult Agregar4()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar4(encuesta_mostrarPreguntas2CLS Oencuesta_mostrarPreguntasCLS)
        {
            String Usuario = Request.Form["user"];
            int res = 0;
            using (var db = new csstdura_encuestaEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {
                        for (int x = 1; x < 6; x++)
                        {
                            //var nombreVariable = "radio_"+x;
                            encuesta_resultados resultado = new encuesta_resultados();
                            resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                            resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                            resultado.resu_denc_id = int.Parse(Request.Form["denc_id_" + x]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                            resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                            resultado.resu_resultado = Request.Form["Valor_radio_" + x];
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

                        return Content("<script language='javascript' type='text/javascript'>alert('Registro exitoso!');window.location = '/Encuesta3/Index5?user=" + Usuario + " ';</script>");

                    }
                    else
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Ocurrio un error!');window.location = '/Admin/CatalogoEmpresa';</script>");

                    }

                }
            }

        }

        public ActionResult Index5(string user)
        {
            if (user != null)
            {

                ViewBag.user = user.ToString();

                List<encuesta_mostrarPreguntas2CLS> list;
                using (var db = new csstdura_encuestaEntities())
                {
                    //hacemos un select a nuestra tabla con los campos que queremos mostrar
                    list = (from preguntas in db.encuesta_det_encuesta
                            where preguntas.denc_parte == 5
                            select new encuesta_mostrarPreguntas2CLS
                            {
                                denc_id = preguntas.denc_id,
                                denc_descrip = preguntas.denc_descrip,
                                denc_valor_1 = preguntas.denc_valor_1,
                                denc_valor_2 = preguntas.denc_valor_2,
                                denc_valor_3 = preguntas.denc_valor_3,
                                denc_valor_4 = preguntas.denc_valor_4,
                                denc_valor_5 = preguntas.denc_valor_5,
                            }).ToList();

                    string encabezado = db.Database.SqlQuery<string>("select encu_descrip from encuesta_encuesta where encu_id = 2").FirstOrDefault();
                    int id_encabezado = db.Database.SqlQuery<int>("select encu_id from encuesta_encuesta where encu_id = 2").FirstOrDefault();
                    int id_empresa = db.Database.SqlQuery<int>("select usua_empresa from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
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
            else
            {
                return RedirectToAction("Index", "Login");
                //return RedirectToAction("Reporting", "ReportManagement", new { area = "Admin" })
            }



        }

        public ActionResult Agregar5()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar5(encuesta_mostrarPreguntas2CLS Oencuesta_mostrarPreguntasCLS)
        {
            String Usuario = Request.Form["user"];
            int res = 0;
            using (var db = new csstdura_encuestaEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {
                        for (int x = 1; x < 10; x++)
                        {
                            //var nombreVariable = "radio_"+x;
                            encuesta_resultados resultado = new encuesta_resultados();
                            resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                            resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                            resultado.resu_denc_id = int.Parse(Request.Form["denc_id_" + x]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                            resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                            resultado.resu_resultado = Request.Form["Valor_radio_" + x];
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

                        return Content("<script language='javascript' type='text/javascript'>alert('Registro exitoso!');window.location = '/Encuesta3/Index6?user=" + Usuario + " ';</script>");

                    }
                    else
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Ocurrio un error!');window.location = '/Admin/CatalogoEmpresa';</script>");

                    }

                }
            }

        }

        public ActionResult Index6(string user)
        {
            if (user != null)
            {

                ViewBag.user = user.ToString();

                List<encuesta_mostrarPreguntas2CLS> list;
                using (var db = new csstdura_encuestaEntities())
                {
                    //hacemos un select a nuestra tabla con los campos que queremos mostrar
                    list = (from preguntas in db.encuesta_det_encuesta
                            where preguntas.denc_parte == 6
                            select new encuesta_mostrarPreguntas2CLS
                            {
                                denc_id = preguntas.denc_id,
                                denc_descrip = preguntas.denc_descrip,
                                denc_valor_1 = preguntas.denc_valor_1,
                                denc_valor_2 = preguntas.denc_valor_2,
                                denc_valor_3 = preguntas.denc_valor_3,
                                denc_valor_4 = preguntas.denc_valor_4,
                                denc_valor_5 = preguntas.denc_valor_5,
                            }).ToList();

                    string encabezado = db.Database.SqlQuery<string>("select encu_descrip from encuesta_encuesta where encu_id = 2").FirstOrDefault();
                    int id_encabezado = db.Database.SqlQuery<int>("select encu_id from encuesta_encuesta where encu_id = 2").FirstOrDefault();
                    int id_empresa = db.Database.SqlQuery<int>("select usua_empresa from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
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
            else
            {
                return RedirectToAction("Index", "Login");
                //return RedirectToAction("Reporting", "ReportManagement", new { area = "Admin" })
            }



        }

        public ActionResult Agregar6()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar6(encuesta_mostrarPreguntas2CLS Oencuesta_mostrarPreguntasCLS)
        {
            String Usuario = Request.Form["user"];
            int res = 0;
            using (var db = new csstdura_encuestaEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {
                        for (int x = 1; x < 5; x++)
                        {
                            //var nombreVariable = "radio_"+x;
                            encuesta_resultados resultado = new encuesta_resultados();
                            resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                            resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                            resultado.resu_denc_id = int.Parse(Request.Form["denc_id_" + x]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                            resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                            resultado.resu_resultado = Request.Form["Valor_radio_" + x];
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

                        return Content("<script language='javascript' type='text/javascript'>alert('Registro exitoso!');window.location = '/Encuesta3/Index7?user=" + Usuario + " ';</script>");

                    }
                    else
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Ocurrio un error!');window.location = '/Admin/CatalogoEmpresa';</script>");

                    }

                }
            }

        }

        public ActionResult Index7(string user)
        {
            if (user != null)
            {

                ViewBag.user = user.ToString();

                List<encuesta_mostrarPreguntas2CLS> list;
                using (var db = new csstdura_encuestaEntities())
                {
                    //hacemos un select a nuestra tabla con los campos que queremos mostrar
                    list = (from preguntas in db.encuesta_det_encuesta
                            where preguntas.denc_parte == 7
                            select new encuesta_mostrarPreguntas2CLS
                            {
                                denc_id = preguntas.denc_id,
                                denc_descrip = preguntas.denc_descrip,
                                denc_valor_1 = preguntas.denc_valor_1,
                                denc_valor_2 = preguntas.denc_valor_2,
                                denc_valor_3 = preguntas.denc_valor_3,
                                denc_valor_4 = preguntas.denc_valor_4,
                                denc_valor_5 = preguntas.denc_valor_5,
                            }).ToList();

                    string encabezado = db.Database.SqlQuery<string>("select encu_descrip from encuesta_encuesta where encu_id = 2").FirstOrDefault();
                    int id_encabezado = db.Database.SqlQuery<int>("select encu_id from encuesta_encuesta where encu_id = 2").FirstOrDefault();
                    int id_empresa = db.Database.SqlQuery<int>("select usua_empresa from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
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
            else
            {
                return RedirectToAction("Index", "Login");
                //return RedirectToAction("Reporting", "ReportManagement", new { area = "Admin" })
            }



        }

        public ActionResult Agregar7()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar7(encuesta_mostrarPreguntas2CLS Oencuesta_mostrarPreguntasCLS)
        {
            String Usuario = Request.Form["user"];
            int res = 0;
            using (var db = new csstdura_encuestaEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {
                        for (int x = 1; x < 5; x++)
                        {
                            //var nombreVariable = "radio_"+x;
                            encuesta_resultados resultado = new encuesta_resultados();
                            resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                            resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                            resultado.resu_denc_id = int.Parse(Request.Form["denc_id_" + x]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                            resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                            resultado.resu_resultado = Request.Form["Valor_radio_" + x];
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

                        return Content("<script language='javascript' type='text/javascript'>alert('Registro exitoso!');window.location = '/Encuesta3/Index8?user=" + Usuario + " ';</script>");

                    }
                    else
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Ocurrio un error!');window.location = '/Admin/CatalogoEmpresa';</script>");

                    }

                }
            }

        }

        public ActionResult Index8(string user)
        {
            if (user != null)
            {

                ViewBag.user = user.ToString();

                List<encuesta_mostrarPreguntas2CLS> list;
                using (var db = new csstdura_encuestaEntities())
                {
                    //hacemos un select a nuestra tabla con los campos que queremos mostrar
                    list = (from preguntas in db.encuesta_det_encuesta
                            where preguntas.denc_parte == 8
                            select new encuesta_mostrarPreguntas2CLS
                            {
                                denc_id = preguntas.denc_id,
                                denc_descrip = preguntas.denc_descrip,
                                denc_valor_1 = preguntas.denc_valor_1,
                                denc_valor_2 = preguntas.denc_valor_2,
                                denc_valor_3 = preguntas.denc_valor_3,
                                denc_valor_4 = preguntas.denc_valor_4,
                                denc_valor_5 = preguntas.denc_valor_5,
                            }).ToList();

                    string encabezado = db.Database.SqlQuery<string>("select encu_descrip from encuesta_encuesta where encu_id = 2").FirstOrDefault();
                    int id_encabezado = db.Database.SqlQuery<int>("select encu_id from encuesta_encuesta where encu_id = 2").FirstOrDefault();
                    int id_empresa = db.Database.SqlQuery<int>("select usua_empresa from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
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
            else
            {
                return RedirectToAction("Index", "Login");
                //return RedirectToAction("Reporting", "ReportManagement", new { area = "Admin" })
            }



        }

        public ActionResult Agregar8()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar8(encuesta_mostrarPreguntas2CLS Oencuesta_mostrarPreguntasCLS)
        {
            String Usuario = Request.Form["user"];
            int res = 0;
            using (var db = new csstdura_encuestaEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {
                        for (int x = 1; x < 6; x++)
                        {
                            //var nombreVariable = "radio_"+x;
                            encuesta_resultados resultado = new encuesta_resultados();
                            resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                            resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                            resultado.resu_denc_id = int.Parse(Request.Form["denc_id_" + x]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                            resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                            resultado.resu_resultado = Request.Form["Valor_radio_" + x];
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

                        return Content("<script language='javascript' type='text/javascript'>alert('Registro exitoso!');window.location = '/Encuesta3/Index9?user=" + Usuario + " ';</script>");

                    }
                    else
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Ocurrio un error!');window.location = '/Admin/CatalogoEmpresa';</script>");

                    }

                }
            }

        }

        public ActionResult Index9(string user)
        {
            if (user != null)
            {

                ViewBag.user = user.ToString();

                List<encuesta_mostrarPreguntas2CLS> list;
                using (var db = new csstdura_encuestaEntities())
                {
                    //hacemos un select a nuestra tabla con los campos que queremos mostrar
                    list = (from preguntas in db.encuesta_det_encuesta
                            where preguntas.denc_parte == 9
                            select new encuesta_mostrarPreguntas2CLS
                            {
                                denc_id = preguntas.denc_id,
                                denc_descrip = preguntas.denc_descrip,
                                denc_valor_1 = preguntas.denc_valor_1,
                                denc_valor_2 = preguntas.denc_valor_2,
                                denc_valor_3 = preguntas.denc_valor_3,
                                denc_valor_4 = preguntas.denc_valor_4,
                                denc_valor_5 = preguntas.denc_valor_5,
                            }).ToList();

                    string encabezado = db.Database.SqlQuery<string>("select encu_descrip from encuesta_encuesta where encu_id = 2").FirstOrDefault();
                    int id_encabezado = db.Database.SqlQuery<int>("select encu_id from encuesta_encuesta where encu_id = 2").FirstOrDefault();
                    int id_empresa = db.Database.SqlQuery<int>("select usua_empresa from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
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
            else
            {
                return RedirectToAction("Index", "Login");
                //return RedirectToAction("Reporting", "ReportManagement", new { area = "Admin" })
            }



        }

        public ActionResult Agregar9()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar9(encuesta_mostrarPreguntas2CLS Oencuesta_mostrarPreguntasCLS)
        {
            String Usuario = Request.Form["user"];
            int res = 0;
            using (var db = new csstdura_encuestaEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {
                        for (int x = 1; x < 6; x++)
                        {
                            //var nombreVariable = "radio_"+x;
                            encuesta_resultados resultado = new encuesta_resultados();
                            resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                            resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                            resultado.resu_denc_id = int.Parse(Request.Form["denc_id_" + x]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                            resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                            resultado.resu_resultado = Request.Form["Valor_radio_" + x];
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

                        return Content("<script language='javascript' type='text/javascript'>alert('Registro exitoso!');window.location = '/Encuesta3/Index10?user=" + Usuario + " ';</script>");

                    }
                    else
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Ocurrio un error!');window.location = '/Admin/CatalogoEmpresa';</script>");

                    }

                }
            }

        }

        public ActionResult Index10(string user)
        {
            if (user != null)
            {
                
                ViewBag.user = user.ToString();

                List<encuesta_mostrarPreguntas2CLS> list;
                using (var db = new csstdura_encuestaEntities())
                {
                    //hacemos un select a nuestra tabla con los campos que queremos mostrar
                    list = (from preguntas in db.encuesta_det_encuesta
                            where preguntas.denc_parte == 10
                            select new encuesta_mostrarPreguntas2CLS
                            {
                                denc_id = preguntas.denc_id,
                                denc_descrip = preguntas.denc_descrip,
                                denc_valor_1 = preguntas.denc_valor_1,
                                denc_valor_2 = preguntas.denc_valor_2,
                                denc_valor_3 = preguntas.denc_valor_3,
                                denc_valor_4 = preguntas.denc_valor_4,
                                denc_valor_5 = preguntas.denc_valor_5,
                            }).ToList();

                    string encabezado = db.Database.SqlQuery<string>("select encu_descrip from encuesta_encuesta where encu_id = 2").FirstOrDefault();
                    int id_encabezado = db.Database.SqlQuery<int>("select encu_id from encuesta_encuesta where encu_id = 2").FirstOrDefault();
                    int id_empresa = db.Database.SqlQuery<int>("select usua_empresa from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
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
            else
            {
                return RedirectToAction("Index", "Login");
                //return RedirectToAction("Reporting", "ReportManagement", new { area = "Admin" })
            }



        }

        public ActionResult Agregar10()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar10(encuesta_mostrarPreguntas2CLS Oencuesta_mostrarPreguntasCLS)
        {
            String Usuario = Request.Form["user"];
            int res = 0;
            using (var db = new csstdura_encuestaEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {
                        for (int x = 1; x < 14; x++)
                        {
                            //var nombreVariable = "radio_"+x;
                            encuesta_resultados resultado = new encuesta_resultados();
                            resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                            resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                            resultado.resu_denc_id = int.Parse(Request.Form["denc_id_" + x]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                            resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                            resultado.resu_resultado = Request.Form["Valor_radio_" + x];
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

                        return Content("<script language='javascript' type='text/javascript'>alert('Registro exitoso!');window.location = '/Encuesta3/Index11?user=" + Usuario + " ';</script>");

                    }
                    else
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Ocurrio un error!');window.location = '/Admin/CatalogoEmpresa';</script>");

                    }

                }
            }

        }

        public ActionResult Index11(string user)
        {
            if (user != null)
            {
                ViewBag.user = user.ToString();

                List<encuesta_mostrarPreguntas2CLS> list;
                using (var db = new csstdura_encuestaEntities())
                {
                    //hacemos un select a nuestra tabla con los campos que queremos mostrar
                    list = (from preguntas in db.encuesta_det_encuesta
                            where preguntas.denc_parte == 11
                            select new encuesta_mostrarPreguntas2CLS
                            {
                                denc_id = preguntas.denc_id,
                                denc_descrip = preguntas.denc_descrip,
                                denc_valor_1 = preguntas.denc_valor_1,
                                denc_valor_2 = preguntas.denc_valor_2,
                                denc_valor_3 = preguntas.denc_valor_3,
                                denc_valor_4 = preguntas.denc_valor_4,
                                denc_valor_5 = preguntas.denc_valor_5,
                            }).ToList();

                    string encabezado = db.Database.SqlQuery<string>("select encu_descrip from encuesta_encuesta where encu_id = 2").FirstOrDefault();
                    int id_encabezado = db.Database.SqlQuery<int>("select encu_id from encuesta_encuesta where encu_id = 2").FirstOrDefault();
                    int id_empresa = db.Database.SqlQuery<int>("select usua_empresa from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
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
            else
            {
                return RedirectToAction("Index", "Login");
                //return RedirectToAction("Reporting", "ReportManagement", new { area = "Admin" })
            }



        }

        public ActionResult Agregar11()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar11(encuesta_mostrarPreguntas2CLS Oencuesta_mostrarPreguntasCLS)
        {
            String Usuario = Request.Form["user"];
            int res = 0;
            using (var db = new csstdura_encuestaEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {
                        String Valor = Request.Form["Valor_radio"];
                        if (Valor.Equals("NO"))
                        {
                            encuesta_resultados resultado = new encuesta_resultados();
                            resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                            resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                            resultado.resu_denc_id = int.Parse(Request.Form["denc_id"]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                            resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                            resultado.resu_resultado = Request.Form["Valor_radio"];
                            resultado.resu_fecha = DateTime.Now;
                            db.encuesta_resultados.Add(resultado);
                            res = db.SaveChanges();
                        }
                        else
                        {
                            encuesta_resultados resultado = new encuesta_resultados();
                            resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                            resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                            resultado.resu_denc_id = int.Parse(Request.Form["denc_id"]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                            resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                            resultado.resu_resultado = Request.Form["Valor_radio"];
                            resultado.resu_fecha = DateTime.Now;
                            db.encuesta_resultados.Add(resultado);
                            res = db.SaveChanges();
                            for (int x = 2; x < 5; x++)
                            {
                                resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                                resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                                resultado.resu_denc_id = int.Parse(Request.Form["denc_id_" + x]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                                resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                                resultado.resu_resultado = Request.Form["Valor_radio_" + x];
                                resultado.resu_fecha = DateTime.Now;
                                db.encuesta_resultados.Add(resultado);
                                res = db.SaveChanges();
                            }
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

                        return Content("<script language='javascript' type='text/javascript'>alert('Registro exitoso!');window.location = '/Encuesta3/Index12?user=" + Usuario + " ';</script>");

                    }
                    else
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Ocurrio un error!');window.location = '/Admin/CatalogoEmpresa';</script>");

                    }

                }
            }

        }

        public ActionResult Index12(string user)
        {
            if (user != null)
            {
                ViewBag.user = user.ToString();

                List<encuesta_mostrarPreguntas2CLS> list;
                using (var db = new csstdura_encuestaEntities())
                {
                    //hacemos un select a nuestra tabla con los campos que queremos mostrar
                    list = (from preguntas in db.encuesta_det_encuesta
                            where preguntas.denc_parte == 12
                            select new encuesta_mostrarPreguntas2CLS
                            {
                                denc_id = preguntas.denc_id,
                                denc_descrip = preguntas.denc_descrip,
                                denc_valor_1 = preguntas.denc_valor_1,
                                denc_valor_2 = preguntas.denc_valor_2,
                                denc_valor_3 = preguntas.denc_valor_3,
                                denc_valor_4 = preguntas.denc_valor_4,
                                denc_valor_5 = preguntas.denc_valor_5,
                            }).ToList();

                    string encabezado = db.Database.SqlQuery<string>("select encu_descrip from encuesta_encuesta where encu_id = 2").FirstOrDefault();
                    int id_encabezado = db.Database.SqlQuery<int>("select encu_id from encuesta_encuesta where encu_id = 2").FirstOrDefault();
                    int id_empresa = db.Database.SqlQuery<int>("select usua_empresa from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
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
            else
            {
                return RedirectToAction("Index", "Login");
                //return RedirectToAction("Reporting", "ReportManagement", new { area = "Admin" })
            }



        }

        public ActionResult Agregar12()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar12(encuesta_mostrarPreguntas2CLS Oencuesta_mostrarPreguntasCLS)
        {
            String Usuario = Request.Form["user"];
            int res = 0;
            using (var db = new csstdura_encuestaEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {
                        String Valor = Request.Form["Valor_radio"];
                        if (Valor.Equals("NO"))
                        {
                            encuesta_resultados resultado = new encuesta_resultados();
                            resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                            resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                            resultado.resu_denc_id = int.Parse(Request.Form["denc_id"]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                            resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                            resultado.resu_resultado = Request.Form["Valor_radio"];
                            resultado.resu_fecha = DateTime.Now;
                            db.encuesta_resultados.Add(resultado);
                            res = db.SaveChanges();
                        }
                        else
                        {
                            encuesta_resultados resultado = new encuesta_resultados();
                            resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                            resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                            resultado.resu_denc_id = int.Parse(Request.Form["denc_id"]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                            resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                            resultado.resu_resultado = Request.Form["Valor_radio"];
                            resultado.resu_fecha = DateTime.Now;
                            db.encuesta_resultados.Add(resultado);
                            res = db.SaveChanges();
                            for (int x = 2; x < 5; x++)
                            {
                                resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                                resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                                resultado.resu_denc_id = int.Parse(Request.Form["denc_id_" + x]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                                resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                                resultado.resu_resultado = Request.Form["Valor_radio_" + x];
                                resultado.resu_fecha = DateTime.Now;
                                db.encuesta_resultados.Add(resultado);
                                res = db.SaveChanges();
                            }
                        }

                        int id_user = int.Parse(Request.Form["id_usuario"]);

                        encuesta_usuarios Oencuesta_usuarios = db.encuesta_usuarios.Where(p => p.usua_id.Equals(id_user)).First();
                        Oencuesta_usuarios.usua_presento = "S";
                        res = db.SaveChanges();

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

                        return Content("<script language='javascript' type='text/javascript'>alert('Registro exitoso!');window.location = '/Encuesta3/Index27?user=" + Usuario + " ';</script>");

                    }
                    else
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Ocurrio un error!');window.location = '/Admin/CatalogoEmpresa';</script>");

                    }

                }
            }

        }

        public ActionResult Index13(string user)
        {
            if (user != null)
            {
                ViewBag.user = user.ToString();

                List<encuesta_mostrarPreguntas2CLS> list;
                using (var db = new csstdura_encuestaEntities())
                {
                    //hacemos un select a nuestra tabla con los campos que queremos mostrar
                    list = (from preguntas in db.encuesta_det_encuesta
                            where preguntas.denc_parte == 13
                            select new encuesta_mostrarPreguntas2CLS
                            {
                                denc_id = preguntas.denc_id,
                                denc_descrip = preguntas.denc_descrip,
                                denc_valor_1 = preguntas.denc_valor_1,
                                denc_valor_2 = preguntas.denc_valor_2,
                                denc_valor_3 = preguntas.denc_valor_3,
                                denc_valor_4 = preguntas.denc_valor_4,
                                denc_valor_5 = preguntas.denc_valor_5,
                            }).ToList();

                    string encabezado = db.Database.SqlQuery<string>("select encu_descrip from encuesta_encuesta where encu_id = 3").FirstOrDefault();
                    int id_encabezado = db.Database.SqlQuery<int>("select encu_id from encuesta_encuesta where encu_id = 3").FirstOrDefault();
                    int id_empresa = db.Database.SqlQuery<int>("select usua_empresa from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
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
            else
            {
                return RedirectToAction("Index", "Login");
                //return RedirectToAction("Reporting", "ReportManagement", new { area = "Admin" })
            }



        }

        public ActionResult Agregar13()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar13(encuesta_mostrarPreguntas2CLS Oencuesta_mostrarPreguntasCLS)
        {
            String Usuario = Request.Form["user"];
            int res = 0;
            using (var db = new csstdura_encuestaEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {

                        for (int x = 1; x < 6; x++)
                        {
                            encuesta_resultados resultado = new encuesta_resultados();
                            resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                            resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                            resultado.resu_denc_id = int.Parse(Request.Form["denc_id_" + x]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                            resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                            resultado.resu_resultado = Request.Form["Valor_radio_" + x];
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

                        return Content("<script language='javascript' type='text/javascript'>alert('Registro exitoso!');window.location = '/Encuesta3/Index14?user=" + Usuario + " ';</script>");

                    }
                    else
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Ocurrio un error!');window.location = '/Admin/CatalogoEmpresa';</script>");

                    }

                }
            }

        }

        public ActionResult Index14(string user)
        {
            if (user != null)
            {
                ViewBag.user = user.ToString();

                List<encuesta_mostrarPreguntas2CLS> list;
                using (var db = new csstdura_encuestaEntities())
                {
                    //hacemos un select a nuestra tabla con los campos que queremos mostrar
                    list = (from preguntas in db.encuesta_det_encuesta
                            where preguntas.denc_parte == 14
                            select new encuesta_mostrarPreguntas2CLS
                            {
                                denc_id = preguntas.denc_id,
                                denc_descrip = preguntas.denc_descrip,
                                denc_valor_1 = preguntas.denc_valor_1,
                                denc_valor_2 = preguntas.denc_valor_2,
                                denc_valor_3 = preguntas.denc_valor_3,
                                denc_valor_4 = preguntas.denc_valor_4,
                                denc_valor_5 = preguntas.denc_valor_5,
                            }).ToList();

                    string encabezado = db.Database.SqlQuery<string>("select encu_descrip from encuesta_encuesta where encu_id = 3").FirstOrDefault();
                    int id_encabezado = db.Database.SqlQuery<int>("select encu_id from encuesta_encuesta where encu_id = 3").FirstOrDefault();
                    int id_empresa = db.Database.SqlQuery<int>("select usua_empresa from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
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
            else
            {
                return RedirectToAction("Index", "Login");
                //return RedirectToAction("Reporting", "ReportManagement", new { area = "Admin" })
            }



        }

        public ActionResult Agregar14()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar14(encuesta_mostrarPreguntas2CLS Oencuesta_mostrarPreguntasCLS)
        {
            String Usuario = Request.Form["user"];
            int res = 0;
            using (var db = new csstdura_encuestaEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {

                        for (int x = 1; x < 4; x++)
                        {
                            encuesta_resultados resultado = new encuesta_resultados();
                            resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                            resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                            resultado.resu_denc_id = int.Parse(Request.Form["denc_id_" + x]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                            resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                            resultado.resu_resultado = Request.Form["Valor_radio_" + x];
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

                        return Content("<script language='javascript' type='text/javascript'>alert('Registro exitoso!');window.location = '/Encuesta3/Index15?user=" + Usuario + " ';</script>");

                    }
                    else
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Ocurrio un error!');window.location = '/Admin/CatalogoEmpresa';</script>");

                    }

                }
            }

        }

        public ActionResult Index15(string user)
        {
            if (user != null)
            {
                ViewBag.user = user.ToString();

                List<encuesta_mostrarPreguntas2CLS> list;
                using (var db = new csstdura_encuestaEntities())
                {
                    //hacemos un select a nuestra tabla con los campos que queremos mostrar
                    list = (from preguntas in db.encuesta_det_encuesta
                            where preguntas.denc_parte == 15
                            select new encuesta_mostrarPreguntas2CLS
                            {
                                denc_id = preguntas.denc_id,
                                denc_descrip = preguntas.denc_descrip,
                                denc_valor_1 = preguntas.denc_valor_1,
                                denc_valor_2 = preguntas.denc_valor_2,
                                denc_valor_3 = preguntas.denc_valor_3,
                                denc_valor_4 = preguntas.denc_valor_4,
                                denc_valor_5 = preguntas.denc_valor_5,
                            }).ToList();

                    string encabezado = db.Database.SqlQuery<string>("select encu_descrip from encuesta_encuesta where encu_id = 3").FirstOrDefault();
                    int id_encabezado = db.Database.SqlQuery<int>("select encu_id from encuesta_encuesta where encu_id = 3").FirstOrDefault();
                    int id_empresa = db.Database.SqlQuery<int>("select usua_empresa from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
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
            else
            {
                return RedirectToAction("Index", "Login");
                //return RedirectToAction("Reporting", "ReportManagement", new { area = "Admin" })
            }



        }

        public ActionResult Agregar15()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar15(encuesta_mostrarPreguntas2CLS Oencuesta_mostrarPreguntasCLS)
        {
            String Usuario = Request.Form["user"];
            int res = 0;
            using (var db = new csstdura_encuestaEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {

                        for (int x = 1; x < 5; x++)
                        {
                            encuesta_resultados resultado = new encuesta_resultados();
                            resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                            resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                            resultado.resu_denc_id = int.Parse(Request.Form["denc_id_" + x]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                            resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                            resultado.resu_resultado = Request.Form["Valor_radio_" + x];
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

                        return Content("<script language='javascript' type='text/javascript'>alert('Registro exitoso!');window.location = '/Encuesta3/Index16?user=" + Usuario + " ';</script>");

                    }
                    else
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Ocurrio un error!');window.location = '/Admin/CatalogoEmpresa';</script>");

                    }

                }
            }

        }

        public ActionResult Index16(string user)
        {
            if (user != null)
            {
                ViewBag.user = user.ToString();

                List<encuesta_mostrarPreguntas2CLS> list;
                using (var db = new csstdura_encuestaEntities())
                {
                    //hacemos un select a nuestra tabla con los campos que queremos mostrar
                    list = (from preguntas in db.encuesta_det_encuesta
                            where preguntas.denc_parte == 16
                            select new encuesta_mostrarPreguntas2CLS
                            {
                                denc_id = preguntas.denc_id,
                                denc_descrip = preguntas.denc_descrip,
                                denc_valor_1 = preguntas.denc_valor_1,
                                denc_valor_2 = preguntas.denc_valor_2,
                                denc_valor_3 = preguntas.denc_valor_3,
                                denc_valor_4 = preguntas.denc_valor_4,
                                denc_valor_5 = preguntas.denc_valor_5,
                            }).ToList();

                    string encabezado = db.Database.SqlQuery<string>("select encu_descrip from encuesta_encuesta where encu_id = 3").FirstOrDefault();
                    int id_encabezado = db.Database.SqlQuery<int>("select encu_id from encuesta_encuesta where encu_id = 3").FirstOrDefault();
                    int id_empresa = db.Database.SqlQuery<int>("select usua_empresa from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
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
            else
            {
                return RedirectToAction("Index", "Login");
                //return RedirectToAction("Reporting", "ReportManagement", new { area = "Admin" })
            }



        }

        public ActionResult Agregar16()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar16(encuesta_mostrarPreguntas2CLS Oencuesta_mostrarPreguntasCLS)
        {
            String Usuario = Request.Form["user"];
            int res = 0;
            using (var db = new csstdura_encuestaEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {

                        for (int x = 1; x < 5; x++)
                        {
                            encuesta_resultados resultado = new encuesta_resultados();
                            resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                            resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                            resultado.resu_denc_id = int.Parse(Request.Form["denc_id_" + x]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                            resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                            resultado.resu_resultado = Request.Form["Valor_radio_" + x];
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

                        return Content("<script language='javascript' type='text/javascript'>alert('Registro exitoso!');window.location = '/Encuesta3/Index17?user=" + Usuario + " ';</script>");

                    }
                    else
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Ocurrio un error!');window.location = '/Admin/CatalogoEmpresa';</script>");

                    }

                }
            }

        }

        public ActionResult Index17(string user)
        {
            if (user != null)
            {
                ViewBag.user = user.ToString();

                List<encuesta_mostrarPreguntas2CLS> list;
                using (var db = new csstdura_encuestaEntities())
                {
                    //hacemos un select a nuestra tabla con los campos que queremos mostrar
                    list = (from preguntas in db.encuesta_det_encuesta
                            where preguntas.denc_parte == 17
                            select new encuesta_mostrarPreguntas2CLS
                            {
                                denc_id = preguntas.denc_id,
                                denc_descrip = preguntas.denc_descrip,
                                denc_valor_1 = preguntas.denc_valor_1,
                                denc_valor_2 = preguntas.denc_valor_2,
                                denc_valor_3 = preguntas.denc_valor_3,
                                denc_valor_4 = preguntas.denc_valor_4,
                                denc_valor_5 = preguntas.denc_valor_5,
                            }).ToList();

                    string encabezado = db.Database.SqlQuery<string>("select encu_descrip from encuesta_encuesta where encu_id = 3").FirstOrDefault();
                    int id_encabezado = db.Database.SqlQuery<int>("select encu_id from encuesta_encuesta where encu_id = 3").FirstOrDefault();
                    int id_empresa = db.Database.SqlQuery<int>("select usua_empresa from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
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
            else
            {
                return RedirectToAction("Index", "Login");
                //return RedirectToAction("Reporting", "ReportManagement", new { area = "Admin" })
            }



        }

        public ActionResult Agregar17()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar17(encuesta_mostrarPreguntas2CLS Oencuesta_mostrarPreguntasCLS)
        {
            String Usuario = Request.Form["user"];
            int res = 0;
            using (var db = new csstdura_encuestaEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {

                        for (int x = 1; x < 7; x++)
                        {
                            encuesta_resultados resultado = new encuesta_resultados();
                            resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                            resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                            resultado.resu_denc_id = int.Parse(Request.Form["denc_id_" + x]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                            resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                            resultado.resu_resultado = Request.Form["Valor_radio_" + x];
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

                        return Content("<script language='javascript' type='text/javascript'>alert('Registro exitoso!');window.location = '/Encuesta3/Index18?user=" + Usuario + " ';</script>");

                    }
                    else
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Ocurrio un error!');window.location = '/Admin/CatalogoEmpresa';</script>");

                    }

                }
            }

        }

        public ActionResult Index18(string user)
        {
            if (user != null)
            {
                ViewBag.user = user.ToString();

                List<encuesta_mostrarPreguntas2CLS> list;
                using (var db = new csstdura_encuestaEntities())
                {
                    //hacemos un select a nuestra tabla con los campos que queremos mostrar
                    list = (from preguntas in db.encuesta_det_encuesta
                            where preguntas.denc_parte == 18
                            select new encuesta_mostrarPreguntas2CLS
                            {
                                denc_id = preguntas.denc_id,
                                denc_descrip = preguntas.denc_descrip,
                                denc_valor_1 = preguntas.denc_valor_1,
                                denc_valor_2 = preguntas.denc_valor_2,
                                denc_valor_3 = preguntas.denc_valor_3,
                                denc_valor_4 = preguntas.denc_valor_4,
                                denc_valor_5 = preguntas.denc_valor_5,
                            }).ToList();

                    string encabezado = db.Database.SqlQuery<string>("select encu_descrip from encuesta_encuesta where encu_id = 3").FirstOrDefault();
                    int id_encabezado = db.Database.SqlQuery<int>("select encu_id from encuesta_encuesta where encu_id = 3").FirstOrDefault();
                    int id_empresa = db.Database.SqlQuery<int>("select usua_empresa from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
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
            else
            {
                return RedirectToAction("Index", "Login");
                //return RedirectToAction("Reporting", "ReportManagement", new { area = "Admin" })
            }



        }

        public ActionResult Agregar18()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar18(encuesta_mostrarPreguntas2CLS Oencuesta_mostrarPreguntasCLS)
        {
            String Usuario = Request.Form["user"];
            int res = 0;
            using (var db = new csstdura_encuestaEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {

                        for (int x = 1; x < 7; x++)
                        {
                            encuesta_resultados resultado = new encuesta_resultados();
                            resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                            resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                            resultado.resu_denc_id = int.Parse(Request.Form["denc_id_" + x]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                            resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                            resultado.resu_resultado = Request.Form["Valor_radio_" + x];
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

                        return Content("<script language='javascript' type='text/javascript'>alert('Registro exitoso!');window.location = '/Encuesta3/Index19?user=" + Usuario + " ';</script>");

                    }
                    else
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Ocurrio un error!');window.location = '/Admin/CatalogoEmpresa';</script>");

                    }

                }
            }

        }

        public ActionResult Index19(string user)
        {
            if (user != null)
            {
                ViewBag.user = user.ToString();

                List<encuesta_mostrarPreguntas2CLS> list;
                using (var db = new csstdura_encuestaEntities())
                {
                    //hacemos un select a nuestra tabla con los campos que queremos mostrar
                    list = (from preguntas in db.encuesta_det_encuesta
                            where preguntas.denc_parte == 19
                            select new encuesta_mostrarPreguntas2CLS
                            {
                                denc_id = preguntas.denc_id,
                                denc_descrip = preguntas.denc_descrip,
                                denc_valor_1 = preguntas.denc_valor_1,
                                denc_valor_2 = preguntas.denc_valor_2,
                                denc_valor_3 = preguntas.denc_valor_3,
                                denc_valor_4 = preguntas.denc_valor_4,
                                denc_valor_5 = preguntas.denc_valor_5,
                            }).ToList();

                    string encabezado = db.Database.SqlQuery<string>("select encu_descrip from encuesta_encuesta where encu_id = 3").FirstOrDefault();
                    int id_encabezado = db.Database.SqlQuery<int>("select encu_id from encuesta_encuesta where encu_id = 3").FirstOrDefault();
                    int id_empresa = db.Database.SqlQuery<int>("select usua_empresa from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
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
            else
            {
                return RedirectToAction("Index", "Login");
                //return RedirectToAction("Reporting", "ReportManagement", new { area = "Admin" })
            }



        }

        public ActionResult Agregar19()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar19(encuesta_mostrarPreguntas2CLS Oencuesta_mostrarPreguntasCLS)
        {
            String Usuario = Request.Form["user"];
            int res = 0;
            using (var db = new csstdura_encuestaEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {

                        for (int x = 1; x < 3; x++)
                        {
                            encuesta_resultados resultado = new encuesta_resultados();
                            resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                            resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                            resultado.resu_denc_id = int.Parse(Request.Form["denc_id_" + x]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                            resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                            resultado.resu_resultado = Request.Form["Valor_radio_" + x];
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

                        return Content("<script language='javascript' type='text/javascript'>alert('Registro exitoso!');window.location = '/Encuesta3/Index20?user=" + Usuario + " ';</script>");

                    }
                    else
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Ocurrio un error!');window.location = '/Admin/CatalogoEmpresa';</script>");

                    }

                }
            }

        }

        public ActionResult Index20(string user)
        {
            if (user != null)
            {
                ViewBag.user = user.ToString();

                List<encuesta_mostrarPreguntas2CLS> list;
                using (var db = new csstdura_encuestaEntities())
                {
                    //hacemos un select a nuestra tabla con los campos que queremos mostrar
                    list = (from preguntas in db.encuesta_det_encuesta
                            where preguntas.denc_parte == 20
                            select new encuesta_mostrarPreguntas2CLS
                            {
                                denc_id = preguntas.denc_id,
                                denc_descrip = preguntas.denc_descrip,
                                denc_valor_1 = preguntas.denc_valor_1,
                                denc_valor_2 = preguntas.denc_valor_2,
                                denc_valor_3 = preguntas.denc_valor_3,
                                denc_valor_4 = preguntas.denc_valor_4,
                                denc_valor_5 = preguntas.denc_valor_5,
                            }).ToList();

                    string encabezado = db.Database.SqlQuery<string>("select encu_descrip from encuesta_encuesta where encu_id = 3").FirstOrDefault();
                    int id_encabezado = db.Database.SqlQuery<int>("select encu_id from encuesta_encuesta where encu_id = 3").FirstOrDefault();
                    int id_empresa = db.Database.SqlQuery<int>("select usua_empresa from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
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
            else
            {
                return RedirectToAction("Index", "Login");
                //return RedirectToAction("Reporting", "ReportManagement", new { area = "Admin" })
            }



        }

        public ActionResult Agregar20()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar20(encuesta_mostrarPreguntas2CLS Oencuesta_mostrarPreguntasCLS)
        {
            String Usuario = Request.Form["user"];
            int res = 0;
            using (var db = new csstdura_encuestaEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {

                        for (int x = 1; x < 7; x++)
                        {
                            encuesta_resultados resultado = new encuesta_resultados();
                            resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                            resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                            resultado.resu_denc_id = int.Parse(Request.Form["denc_id_" + x]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                            resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                            resultado.resu_resultado = Request.Form["Valor_radio_" + x];
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

                        return Content("<script language='javascript' type='text/javascript'>alert('Registro exitoso!');window.location = '/Encuesta3/Index21?user=" + Usuario + " ';</script>");

                    }
                    else
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Ocurrio un error!');window.location = '/Admin/CatalogoEmpresa';</script>");

                    }

                }
            }

        }

        public ActionResult Index21(string user)
        {
            if (user != null)
            {
                ViewBag.user = user.ToString();

                List<encuesta_mostrarPreguntas2CLS> list;
                using (var db = new csstdura_encuestaEntities())
                {
                    //hacemos un select a nuestra tabla con los campos que queremos mostrar
                    list = (from preguntas in db.encuesta_det_encuesta
                            where preguntas.denc_parte == 21
                            select new encuesta_mostrarPreguntas2CLS
                            {
                                denc_id = preguntas.denc_id,
                                denc_descrip = preguntas.denc_descrip,
                                denc_valor_1 = preguntas.denc_valor_1,
                                denc_valor_2 = preguntas.denc_valor_2,
                                denc_valor_3 = preguntas.denc_valor_3,
                                denc_valor_4 = preguntas.denc_valor_4,
                                denc_valor_5 = preguntas.denc_valor_5,
                            }).ToList();

                    string encabezado = db.Database.SqlQuery<string>("select encu_descrip from encuesta_encuesta where encu_id = 3").FirstOrDefault();
                    int id_encabezado = db.Database.SqlQuery<int>("select encu_id from encuesta_encuesta where encu_id = 3").FirstOrDefault();
                    int id_empresa = db.Database.SqlQuery<int>("select usua_empresa from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
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
            else
            {
                return RedirectToAction("Index", "Login");
                //return RedirectToAction("Reporting", "ReportManagement", new { area = "Admin" })
            }



        }

        public ActionResult Agregar21()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar21(encuesta_mostrarPreguntas2CLS Oencuesta_mostrarPreguntasCLS)
        {
            String Usuario = Request.Form["user"];
            int res = 0;
            using (var db = new csstdura_encuestaEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {

                        for (int x = 1; x < 6; x++)
                        {
                            encuesta_resultados resultado = new encuesta_resultados();
                            resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                            resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                            resultado.resu_denc_id = int.Parse(Request.Form["denc_id_" + x]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                            resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                            resultado.resu_resultado = Request.Form["Valor_radio_" + x];
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

                        return Content("<script language='javascript' type='text/javascript'>alert('Registro exitoso!');window.location = '/Encuesta3/Index22?user=" + Usuario + " ';</script>");

                    }
                    else
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Ocurrio un error!');window.location = '/Admin/CatalogoEmpresa';</script>");

                    }

                }
            }

        }

        public ActionResult Index22(string user)
        {
            if (user != null)
            {
                ViewBag.user = user.ToString();

                List<encuesta_mostrarPreguntas2CLS> list;
                using (var db = new csstdura_encuestaEntities())
                {
                    //hacemos un select a nuestra tabla con los campos que queremos mostrar
                    list = (from preguntas in db.encuesta_det_encuesta
                            where preguntas.denc_parte == 22
                            select new encuesta_mostrarPreguntas2CLS
                            {
                                denc_id = preguntas.denc_id,
                                denc_descrip = preguntas.denc_descrip,
                                denc_valor_1 = preguntas.denc_valor_1,
                                denc_valor_2 = preguntas.denc_valor_2,
                                denc_valor_3 = preguntas.denc_valor_3,
                                denc_valor_4 = preguntas.denc_valor_4,
                                denc_valor_5 = preguntas.denc_valor_5,
                            }).ToList();

                    string encabezado = db.Database.SqlQuery<string>("select encu_descrip from encuesta_encuesta where encu_id = 3").FirstOrDefault();
                    int id_encabezado = db.Database.SqlQuery<int>("select encu_id from encuesta_encuesta where encu_id = 3").FirstOrDefault();
                    int id_empresa = db.Database.SqlQuery<int>("select usua_empresa from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
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
            else
            {
                return RedirectToAction("Index", "Login");
                //return RedirectToAction("Reporting", "ReportManagement", new { area = "Admin" })
            }



        }

        public ActionResult Agregar22()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar22(encuesta_mostrarPreguntas2CLS Oencuesta_mostrarPreguntasCLS)
        {
            String Usuario = Request.Form["user"];
            int res = 0;
            using (var db = new csstdura_encuestaEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {

                        for (int x = 1; x < 6; x++)
                        {
                            encuesta_resultados resultado = new encuesta_resultados();
                            resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                            resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                            resultado.resu_denc_id = int.Parse(Request.Form["denc_id_" + x]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                            resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                            resultado.resu_resultado = Request.Form["Valor_radio_" + x];
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

                        return Content("<script language='javascript' type='text/javascript'>alert('Registro exitoso!');window.location = '/Encuesta3/Index23?user=" + Usuario + " ';</script>");

                    }
                    else
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Ocurrio un error!');window.location = '/Admin/CatalogoEmpresa';</script>");

                    }

                }
            }

        }

        public ActionResult Index23(string user)
        {
            if (user != null)
            {
                ViewBag.user = user.ToString();

                List<encuesta_mostrarPreguntas2CLS> list;
                using (var db = new csstdura_encuestaEntities())
                {
                    //hacemos un select a nuestra tabla con los campos que queremos mostrar
                    list = (from preguntas in db.encuesta_det_encuesta
                            where preguntas.denc_parte == 23
                            select new encuesta_mostrarPreguntas2CLS
                            {
                                denc_id = preguntas.denc_id,
                                denc_descrip = preguntas.denc_descrip,
                                denc_valor_1 = preguntas.denc_valor_1,
                                denc_valor_2 = preguntas.denc_valor_2,
                                denc_valor_3 = preguntas.denc_valor_3,
                                denc_valor_4 = preguntas.denc_valor_4,
                                denc_valor_5 = preguntas.denc_valor_5,
                            }).ToList();

                    string encabezado = db.Database.SqlQuery<string>("select encu_descrip from encuesta_encuesta where encu_id = 3").FirstOrDefault();
                    int id_encabezado = db.Database.SqlQuery<int>("select encu_id from encuesta_encuesta where encu_id = 3").FirstOrDefault();
                    int id_empresa = db.Database.SqlQuery<int>("select usua_empresa from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
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
            else
            {
                return RedirectToAction("Index", "Login");
                //return RedirectToAction("Reporting", "ReportManagement", new { area = "Admin" })
            }



        }

        public ActionResult Agregar23()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar23(encuesta_mostrarPreguntas2CLS Oencuesta_mostrarPreguntasCLS)
        {
            String Usuario = Request.Form["user"];
            int res = 0;
            using (var db = new csstdura_encuestaEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {

                        for (int x = 1; x < 11; x++)
                        {
                            encuesta_resultados resultado = new encuesta_resultados();
                            resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                            resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                            resultado.resu_denc_id = int.Parse(Request.Form["denc_id_" + x]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                            resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                            resultado.resu_resultado = Request.Form["Valor_radio_" + x];
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

                        return Content("<script language='javascript' type='text/javascript'>alert('Registro exitoso!');window.location = '/Encuesta3/Index24?user=" + Usuario + " ';</script>");

                    }
                    else
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Ocurrio un error!');window.location = '/Admin/CatalogoEmpresa';</script>");

                    }

                }
            }

        }

        public ActionResult Index24(string user)
        {
            if (user != null)
            {
                ViewBag.user = user.ToString();

                List<encuesta_mostrarPreguntas2CLS> list;
                using (var db = new csstdura_encuestaEntities())
                {
                    //hacemos un select a nuestra tabla con los campos que queremos mostrar
                    list = (from preguntas in db.encuesta_det_encuesta
                            where preguntas.denc_parte == 24
                            select new encuesta_mostrarPreguntas2CLS
                            {
                                denc_id = preguntas.denc_id,
                                denc_descrip = preguntas.denc_descrip,
                                denc_valor_1 = preguntas.denc_valor_1,
                                denc_valor_2 = preguntas.denc_valor_2,
                                denc_valor_3 = preguntas.denc_valor_3,
                                denc_valor_4 = preguntas.denc_valor_4,
                                denc_valor_5 = preguntas.denc_valor_5,
                            }).ToList();

                    string encabezado = db.Database.SqlQuery<string>("select encu_descrip from encuesta_encuesta where encu_id = 3").FirstOrDefault();
                    int id_encabezado = db.Database.SqlQuery<int>("select encu_id from encuesta_encuesta where encu_id = 3").FirstOrDefault();
                    int id_empresa = db.Database.SqlQuery<int>("select usua_empresa from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
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
            else
            {
                return RedirectToAction("Index", "Login");
                //return RedirectToAction("Reporting", "ReportManagement", new { area = "Admin" })
            }



        }

        public ActionResult Agregar24()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar24(encuesta_mostrarPreguntas2CLS Oencuesta_mostrarPreguntasCLS)
        {
            String Usuario = Request.Form["user"];
            int res = 0;
            using (var db = new csstdura_encuestaEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {

                        for (int x = 1; x < 9; x++)
                        {
                            encuesta_resultados resultado = new encuesta_resultados();
                            resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                            resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                            resultado.resu_denc_id = int.Parse(Request.Form["denc_id_" + x]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                            resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                            resultado.resu_resultado = Request.Form["Valor_radio_" + x];
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

                        return Content("<script language='javascript' type='text/javascript'>alert('Registro exitoso!');window.location = '/Encuesta3/Index25?user=" + Usuario + " ';</script>");

                    }
                    else
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Ocurrio un error!');window.location = '/Admin/CatalogoEmpresa';</script>");

                    }

                }
            }

        }

        public ActionResult Index25(string user)
        {
            if (user != null)
            {
                ViewBag.user = user.ToString();

                List<encuesta_mostrarPreguntas2CLS> list;
                using (var db = new csstdura_encuestaEntities())
                {
                    //hacemos un select a nuestra tabla con los campos que queremos mostrar
                    list = (from preguntas in db.encuesta_det_encuesta
                            where preguntas.denc_parte == 25
                            select new encuesta_mostrarPreguntas2CLS
                            {
                                denc_id = preguntas.denc_id,
                                denc_descrip = preguntas.denc_descrip,
                                denc_valor_1 = preguntas.denc_valor_1,
                                denc_valor_2 = preguntas.denc_valor_2,
                                denc_valor_3 = preguntas.denc_valor_3,
                                denc_valor_4 = preguntas.denc_valor_4,
                                denc_valor_5 = preguntas.denc_valor_5,
                            }).ToList();

                    string encabezado = db.Database.SqlQuery<string>("select encu_descrip from encuesta_encuesta where encu_id = 2").FirstOrDefault();
                    int id_encabezado = db.Database.SqlQuery<int>("select encu_id from encuesta_encuesta where encu_id = 2").FirstOrDefault();
                    int id_empresa = db.Database.SqlQuery<int>("select usua_empresa from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
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
            else
            {
                return RedirectToAction("Index", "Login");
                //return RedirectToAction("Reporting", "ReportManagement", new { area = "Admin" })
            }



        }

        public ActionResult Agregar25()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar25(encuesta_mostrarPreguntas2CLS Oencuesta_mostrarPreguntasCLS)
        {
            String Usuario = Request.Form["user"];
            int res = 0;
            using (var db = new csstdura_encuestaEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {
                        String Valor = Request.Form["Valor_radio"];
                        if (Valor.Equals("NO"))
                        {
                            encuesta_resultados resultado = new encuesta_resultados();
                            resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                            resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                            resultado.resu_denc_id = int.Parse(Request.Form["denc_id"]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                            resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                            resultado.resu_resultado = Request.Form["Valor_radio"];
                            resultado.resu_fecha = DateTime.Now;
                            db.encuesta_resultados.Add(resultado);
                            res = db.SaveChanges();
                        }
                        else
                        {
                            encuesta_resultados resultado = new encuesta_resultados();
                            resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                            resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                            resultado.resu_denc_id = int.Parse(Request.Form["denc_id"]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                            resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                            resultado.resu_resultado = Request.Form["Valor_radio"];
                            resultado.resu_fecha = DateTime.Now;
                            db.encuesta_resultados.Add(resultado);
                            res = db.SaveChanges();
                            for (int x = 2; x < 5; x++)
                            {
                                resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                                resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                                resultado.resu_denc_id = int.Parse(Request.Form["denc_id_" + x]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                                resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                                resultado.resu_resultado = Request.Form["Valor_radio_" + x];
                                resultado.resu_fecha = DateTime.Now;
                                db.encuesta_resultados.Add(resultado);
                                res = db.SaveChanges();
                            }
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

                        return Content("<script language='javascript' type='text/javascript'>alert('Registro exitoso!');window.location = '/Encuesta3/Index26?user=" + Usuario + " ';</script>");

                    }
                    else
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Ocurrio un error!');window.location = '/Admin/CatalogoEmpresa';</script>");

                    }

                }
            }

        }

        public ActionResult Index26(string user)
        {
            if (user != null)
            {
                ViewBag.user = user.ToString();

                List<encuesta_mostrarPreguntas2CLS> list;
                using (var db = new csstdura_encuestaEntities())
                {
                    //hacemos un select a nuestra tabla con los campos que queremos mostrar
                    list = (from preguntas in db.encuesta_det_encuesta
                            where preguntas.denc_parte == 26
                            select new encuesta_mostrarPreguntas2CLS
                            {
                                denc_id = preguntas.denc_id,
                                denc_descrip = preguntas.denc_descrip,
                                denc_valor_1 = preguntas.denc_valor_1,
                                denc_valor_2 = preguntas.denc_valor_2,
                                denc_valor_3 = preguntas.denc_valor_3,
                                denc_valor_4 = preguntas.denc_valor_4,
                                denc_valor_5 = preguntas.denc_valor_5,
                            }).ToList();

                    string encabezado = db.Database.SqlQuery<string>("select encu_descrip from encuesta_encuesta where encu_id = 2").FirstOrDefault();
                    int id_encabezado = db.Database.SqlQuery<int>("select encu_id from encuesta_encuesta where encu_id = 2").FirstOrDefault();
                    int id_empresa = db.Database.SqlQuery<int>("select usua_empresa from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
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
            else
            {
                return RedirectToAction("Index", "Login");
                //return RedirectToAction("Reporting", "ReportManagement", new { area = "Admin" })
            }



        }

        public ActionResult Agregar26()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar26(encuesta_mostrarPreguntas2CLS Oencuesta_mostrarPreguntasCLS)
        {
            String Usuario = Request.Form["user"];
            int res = 0;
            using (var db = new csstdura_encuestaEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {
                        String Valor = Request.Form["Valor_radio"];
                        if (Valor.Equals("NO"))
                        {
                            encuesta_resultados resultado = new encuesta_resultados();
                            resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                            resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                            resultado.resu_denc_id = int.Parse(Request.Form["denc_id"]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                            resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                            resultado.resu_resultado = Request.Form["Valor_radio"];
                            resultado.resu_fecha = DateTime.Now;
                            db.encuesta_resultados.Add(resultado);
                            res = db.SaveChanges();
                        }
                        else
                        {
                            encuesta_resultados resultado = new encuesta_resultados();
                            resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                            resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                            resultado.resu_denc_id = int.Parse(Request.Form["denc_id"]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                            resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                            resultado.resu_resultado = Request.Form["Valor_radio"];
                            resultado.resu_fecha = DateTime.Now;
                            db.encuesta_resultados.Add(resultado);
                            res = db.SaveChanges();
                            for (int x = 2; x < 5; x++)
                            {
                                resultado.resu_emp_id = int.Parse(Request.Form["id_empresa"]);
                                resultado.resu_encu_id = Oencuesta_mostrarPreguntasCLS.encu_id;
                                resultado.resu_denc_id = int.Parse(Request.Form["denc_id_" + x]);//Oencuesta_mostrarPreguntasCLS.denc_id;
                                resultado.resu_usua_id = int.Parse(Request.Form["id_usuario"]);
                                resultado.resu_resultado = Request.Form["Valor_radio_" + x];
                                resultado.resu_fecha = DateTime.Now;
                                db.encuesta_resultados.Add(resultado);
                                res = db.SaveChanges();
                            }
                        }
                        int id_user = int.Parse(Request.Form["id_usuario"]);

                        encuesta_usuarios Oencuesta_usuarios = db.encuesta_usuarios.Where(p => p.usua_id.Equals(id_user)).First();
                        Oencuesta_usuarios.usua_presento = "S";
                        res = db.SaveChanges();

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

                        return Content("<script language='javascript' type='text/javascript'>window.location = '/Encuesta3/Index27?user=" + Usuario + " ';</script>");

                    }
                    else
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Ocurrio un error!');window.location = '/Admin/CatalogoEmpresa';</script>");

                    }

                }
            }

        }

        public ActionResult Index27(string user)
        {
            if (user != null)
            {
                ViewBag.user = user.ToString();

                using (var db = new csstdura_encuestaEntities())
                {
                    //hacemos un select a nuestra tabla con los campos que queremos mostrar

                    string nombreEmpleado = db.Database.SqlQuery<string>("select usua_nombre from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();
                    int id_usuario = db.Database.SqlQuery<int>("select usua_id from encuesta_usuarios where usua_n_usuario = '" + user + "'").FirstOrDefault();

                    ViewBag.nombreEmpleado = nombreEmpleado;
                    ViewBag.id_usuario = id_usuario;
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
                //return RedirectToAction("Reporting", "ReportManagement", new { area = "Admin" })
            }



        }

        public ActionResult Agregar27()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar27(encuesta_mostrarPreguntas2CLS Oencuesta_mostrarPreguntasCLS)
        {

            return RedirectToAction("Index", "Login");
            //return Content("<script language='javascript' type='text/javascript'>alert('Gracias por contestar la encuesta.');window.location = '/Encuesta3/gracias?user=" + Usuario + " ';</script>");

        }

    }
}