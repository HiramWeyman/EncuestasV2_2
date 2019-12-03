using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EncuestasV2.Models;
using System.Security.Cryptography;
using System.Text;
using System.Transactions;

namespace EncuestasV2.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Admin()
        {
            return View();
        }
        List<SelectListItem> listaEmpresa;
        List<SelectListItem> listaSexo;
        List<SelectListItem> listaEdad;
        List<SelectListItem> listaEdoCivil;
        List<SelectListItem> listaOpciones;
        List<SelectListItem> listaProceso;
        List<SelectListItem> listaPuesto;
        List<SelectListItem> listaContrata;
        List<SelectListItem> listaPersonal;
        List<SelectListItem> listaJornada;
        List<SelectListItem> listaRotacion;
        List<SelectListItem> listaTiempo;
        List<SelectListItem> listaExpLab;

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
        private void llenarSexo()
        {
            using (var db = new csstdura_encuestaEntities())
            {
                listaSexo = (from sexo in db.encuesta_sexo
                             select new SelectListItem
                             {
                                 Value = sexo.sexo_id.ToString(),
                                 Text = sexo.sexo_desc,
                                 Selected = false

                             }).ToList();
                listaSexo.Insert(0, new SelectListItem { Text = "Seleccione", Value = "" });
            }
        }

        private void llenarEdad()
        {
            using (var db = new csstdura_encuestaEntities())
            {
                listaEdad = (from edad in db.encuesta_edades
                             select new SelectListItem
                             {
                                 Value = edad.edad_id.ToString(),
                                 Text = edad.edad_desc,
                                 Selected = false

                             }).ToList();
                listaEdad.Insert(0, new SelectListItem { Text = "Seleccione", Value = "" });
            }
        }

        private void llenarEdoCivil()
        {
            using (var db = new csstdura_encuestaEntities())
            {
                listaEdoCivil = (from edo in db.encuesta_edocivil
                                 select new SelectListItem
                                 {
                                     Value = edo.edocivil_id.ToString(),
                                     Text = edo.edocivil_desc,
                                     Selected = false

                                 }).ToList();
                listaEdoCivil.Insert(0, new SelectListItem { Text = "Seleccione", Value = "" });
            }
        }

        private void llenarOpciones()
        {
            using (var db = new csstdura_encuestaEntities())
            {
                listaOpciones = (from op in db.encuaesta_opciones
                                 select new SelectListItem
                                 {
                                     Value = op.opcion_id.ToString(),
                                     Text = op.opcion_desc,
                                     Selected = false

                                 }).ToList();
                listaOpciones.Insert(0, new SelectListItem { Text = "Seleccione", Value = "" });
            }
        }

        private void llenarProcesoEdu()
        {
            using (var db = new csstdura_encuestaEntities())
            {
                listaProceso = (from proc in db.encuesta_procesoedu
                                select new SelectListItem
                                {
                                    Value = proc.procesoedu_id.ToString(),
                                    Text = proc.procesoedu_desc,
                                    Selected = false

                                }).ToList();
                listaProceso.Insert(0, new SelectListItem { Text = "Seleccione", Value = "" });
            }
        }

        private void llenarTipoPuesto()
        {
            using (var db = new csstdura_encuestaEntities())
            {
                listaPuesto = (from puesto in db.encuesta_tipopuesto
                               select new SelectListItem
                               {
                                   Value = puesto.tipopuesto_id.ToString(),
                                   Text = puesto.tipopuesto_desc,
                                   Selected = false

                               }).ToList();
                listaPuesto.Insert(0, new SelectListItem { Text = "Seleccione", Value = "" });
            }
        }

        private void llenarTipoContratacion()
        {
            using (var db = new csstdura_encuestaEntities())
            {
                listaContrata = (from contra in db.encuesta_tipocontrata
                                 select new SelectListItem
                                 {
                                     Value = contra.tipocont_id.ToString(),
                                     Text = contra.tipocont_desc,
                                     Selected = false

                                 }).ToList();
                listaContrata.Insert(0, new SelectListItem { Text = "Seleccione", Value = "" });
            }
        }

        private void llenarTipoPersonal()
        {
            using (var db = new csstdura_encuestaEntities())
            {
                listaPersonal = (from personal in db.encuesta_tipopersonal
                                 select new SelectListItem
                                 {
                                     Value = personal.tipoperson_id.ToString(),
                                     Text = personal.tipoperson_desc,
                                     Selected = false

                                 }).ToList();
                listaPersonal.Insert(0, new SelectListItem { Text = "Seleccione", Value = "" });
            }
        }

        private void llenarTipoJornada()
        {
            using (var db = new csstdura_encuestaEntities())
            {
                listaJornada = (from jornada in db.encuesta_tipojornada
                                select new SelectListItem
                                {
                                    Value = jornada.tipojornada_id.ToString(),
                                    Text = jornada.tipojornada_desc,
                                    Selected = false

                                }).ToList();
                listaJornada.Insert(0, new SelectListItem { Text = "Seleccione", Value = "" });
            }
        }

        private void llenarRotacionTurno()
        {
            using (var db = new csstdura_encuestaEntities())
            {
                listaRotacion = (from rotacion in db.encuaesta_rotacion
                                 select new SelectListItem
                                 {
                                     Value = rotacion.rotacionturno_id.ToString(),
                                     Text = rotacion.rotacionturno_desc,
                                     Selected = false

                                 }).ToList();
                listaRotacion.Insert(0, new SelectListItem { Text = "Seleccione", Value = "" });
            }
        }
        private void llenarTiempoEmp()
        {
            using (var db = new csstdura_encuestaEntities())
            {
                listaTiempo = (from tiempo in db.encuesta_tiempopuesto
                               select new SelectListItem
                               {
                                   Value = tiempo.tiempopue_id.ToString(),
                                   Text = tiempo.tiempopue_desc,
                                   Selected = false

                               }).ToList();
                listaTiempo.Insert(0, new SelectListItem { Text = "Seleccione", Value = "" });
            }
        }

        private void llenarExpLab()
        {
            using (var db = new csstdura_encuestaEntities())
            {
                listaExpLab = (from exp in db.encuesta_explab
                               select new SelectListItem
                               {
                                   Value = exp.explab_id.ToString(),
                                   Text = exp.explab_desc,
                                   Selected = false

                               }).ToList();
                listaExpLab.Insert(0, new SelectListItem { Text = "Seleccione", Value = "" });
            }
        }
        public ActionResult Agregar()
        {
            llenarEmpresa();
            llenarSexo();
            llenarEdad();
            llenarEdoCivil();
            llenarOpciones();
            llenarProcesoEdu();
            llenarTipoPuesto();
            llenarTipoContratacion();
            llenarTipoPersonal();
            llenarTipoJornada();
            llenarRotacionTurno();
            llenarTiempoEmp();
            llenarExpLab();
            ViewBag.listaEmpresa = listaEmpresa;
            ViewBag.listaSexo = listaSexo;
            ViewBag.listaEdad = listaEdad;
            ViewBag.listaEdoCivil = listaEdoCivil;
            ViewBag.listaOpciones = listaOpciones;
            ViewBag.listaProceso = listaProceso;
            ViewBag.listaPuesto = listaPuesto;
            ViewBag.listaContrata = listaContrata;
            ViewBag.listaPersonal = listaPersonal;
            ViewBag.listaJornada = listaJornada;
            ViewBag.listaRotacion = listaRotacion;
            ViewBag.listaTiempo = listaTiempo;
            ViewBag.listaExpLab = listaExpLab;
            return View();
        }

        [HttpPost]
        public ActionResult Agregar(encuesta_usuariosCLS Oencuesta_usuariosCLS)
        {
            using (var db = new csstdura_encuestaEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    if (!ModelState.IsValid)
                    {
                        llenarEmpresa();
                        llenarSexo();
                        llenarEdad();
                        llenarEdoCivil();
                        llenarOpciones();
                        llenarProcesoEdu();
                        llenarTipoPuesto();
                        llenarTipoContratacion();
                        llenarTipoPersonal();
                        llenarTipoJornada();
                        llenarRotacionTurno();
                        llenarTiempoEmp();
                        llenarExpLab();
                        ViewBag.listaSexo = listaSexo;
                        ViewBag.listaEdad = listaEdad;
                        ViewBag.listaEdoCivil = listaEdoCivil;
                        ViewBag.listaOpciones = listaOpciones;
                        ViewBag.listaProceso = listaProceso;
                        ViewBag.listaPuesto = listaPuesto;
                        ViewBag.listaContrata = listaContrata;
                        ViewBag.listaPersonal = listaPersonal;
                        ViewBag.listaJornada = listaJornada;
                        ViewBag.listaRotacion = listaRotacion;
                        ViewBag.listaTiempo = listaTiempo;
                        ViewBag.listaExpLab = listaExpLab;
                        return View(Oencuesta_usuariosCLS);
                    }
                    //Usando clase de entity framework
                    encuesta_usuarios usuarios = new encuesta_usuarios();
                    usuarios.usua_nombre = Oencuesta_usuariosCLS.usua_nombre;
                    usuarios.usua_empresa = Oencuesta_usuariosCLS.usua_empresa;
                    usuarios.usua_f_aplica = DateTime.Now;
                    usuarios.usua_tipo = "U";
                    usuarios.usua_estatus = "ACTIVO";
                    usuarios.usua_n_usuario = Oencuesta_usuariosCLS.usua_n_usuario;

                    //Cifrando el password
                    SHA256Managed sha = new SHA256Managed();
                    byte[] byteContra = Encoding.Default.GetBytes(Oencuesta_usuariosCLS.usua_p_usuario);
                    byte[] byteContraCifrado = sha.ComputeHash(byteContra);
                    string contraCifrada = BitConverter.ToString(byteContraCifrado).Replace("-", "");
                    usuarios.usua_p_usuario = contraCifrada;

                    //usuarios.usua_p_usuario = Oencuesta_usuariosCLS.usua_p_usuario;
                    usuarios.usua_u_alta = "";
                    usuarios.usua_f_alta = DateTime.Now;
                    usuarios.usua_u_cancela = "";
                    usuarios.usua_f_cancela = null;
                    usuarios.usua_genero = Oencuesta_usuariosCLS.usua_genero;
                    usuarios.usua_edad = Oencuesta_usuariosCLS.usua_edad;
                    usuarios.usua_edo_civil = Oencuesta_usuariosCLS.usua_edo_civil;
                    usuarios.usua_sin_forma = Oencuesta_usuariosCLS.usua_sin_forma;
                    usuarios.usua_primaria = Oencuesta_usuariosCLS.usua_primaria;
                    usuarios.usua_secundaria = Oencuesta_usuariosCLS.usua_secundaria;
                    usuarios.usua_preparatoria = Oencuesta_usuariosCLS.usua_preparatoria;
                    usuarios.usua_tecnico = Oencuesta_usuariosCLS.usua_tecnico;
                    usuarios.usua_licenciatura = Oencuesta_usuariosCLS.usua_licenciatura;
                    usuarios.usua_maestria = Oencuesta_usuariosCLS.usua_maestria;
                    usuarios.usua_doctorado = Oencuesta_usuariosCLS.usua_doctorado;
                    usuarios.usua_tipo_puesto = Oencuesta_usuariosCLS.usua_tipo_puesto;
                    usuarios.usua_tipo_contratacion = Oencuesta_usuariosCLS.usua_tipo_contratacion;
                    usuarios.usua_tipo_personal = Oencuesta_usuariosCLS.usua_tipo_personal;
                    usuarios.usua_tipo_jornada = Oencuesta_usuariosCLS.usua_tipo_jornada;
                    usuarios.usua_rotacion_turno = Oencuesta_usuariosCLS.usua_rotacion_turno;
                    usuarios.usua_tiempo_puesto = Oencuesta_usuariosCLS.usua_tiempo_puesto;
                    usuarios.usua_exp_laboral = Oencuesta_usuariosCLS.usua_exp_laboral;
                    db.encuesta_usuarios.Add(usuarios);
                    int res = db.SaveChanges();
                    transaction.Complete();
                    if (res == 1)
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Registro exitoso!');window.location = '/Login/Index';</script>");

                    }
                    else
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Ocurrio un error!');window.location = '/Usuarios/Agregar';</script>");

                    }


                }


            }


        }
    }
}