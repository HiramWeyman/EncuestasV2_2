﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EncuestasV2.Filters;
using EncuestasV2.Models;
using System.Transactions;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace EncuestasV2.Controllers
{
    [AccederAdmin]
    public class AdminController : Controller
    {
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

        //Generar Rportes en Excel
        public FileResult generarExcelEmpleados()
        {

            byte[] buffer;
            using (MemoryStream ms = new MemoryStream())
            {
                //Todo el documento excel
                ExcelPackage ep = new ExcelPackage();
                //Crear una hoja
                ep.Workbook.Worksheets.Add("Reporte de Empleados");
                ExcelWorksheet ew = ep.Workbook.Worksheets[1];

                //Ponemos nombres de las columnas
                ew.Cells[1, 1].Value = "ID";
                ew.Cells[1, 2].Value = "Nombre";
                ew.Cells[1, 3].Value = "Empresa";
                ew.Cells[1, 4].Value = "Fecha de aplicación";
                ew.Cells[1, 5].Value = "Estatus";
                ew.Cells[1, 6].Value = "Nombre de usuario";
                ew.Cells[1, 7].Value = "Fecha de Alta";
                ew.Cells[1, 8].Value = "Fecha de cancelación";
                ew.Cells[1, 9].Value = "Genero";
                ew.Cells[1, 10].Value = "Edad";
                ew.Cells[1, 11].Value = "Estado Civil";
                ew.Cells[1, 12].Value = "Sin formación";
                ew.Cells[1, 13].Value = "Primaria";
                ew.Cells[1, 14].Value = "Secunadaria";
                ew.Cells[1, 15].Value = "Preparatoria";
                ew.Cells[1, 16].Value = "Técnico";
                ew.Cells[1, 17].Value = "Licenciatura";
                ew.Cells[1, 18].Value = "Maestría";
                ew.Cells[1, 19].Value = "Doctorado";
                ew.Cells[1, 20].Value = "Tipo de puesto";
                ew.Cells[1, 21].Value = "Tipo de contratación";
                ew.Cells[1, 22].Value = "Tipo de personal ";
                ew.Cells[1, 23].Value = "Tipo de jornada";
                ew.Cells[1, 24].Value = "Rotación de turno";
                ew.Cells[1, 25].Value = "Tiempo en puesto";
                ew.Cells[1, 26].Value = "Experiencia laboral";
                ew.Cells[1, 27].Value = "Presento Encuesta";

                ew.Column(1).Width = 10;
                ew.Column(2).Width = 30;
                ew.Column(3).Width = 30;
                ew.Column(4).Width = 30;
                ew.Column(5).Width = 10;
                ew.Column(6).Width = 30;
                ew.Column(7).Width = 30;
                ew.Column(8).Width = 30;
                ew.Column(9).Width = 10;
                ew.Column(10).Width = 10;
                ew.Column(11).Width = 10;
                ew.Column(12).Width = 10;
                ew.Column(13).Width = 20;
                ew.Column(14).Width = 20;
                ew.Column(15).Width = 20;
                ew.Column(16).Width = 20;
                ew.Column(17).Width = 20;
                ew.Column(18).Width = 20;
                ew.Column(19).Width = 20;
                ew.Column(20).Width = 20;
                ew.Column(21).Width = 40;
                ew.Column(22).Width = 40;
                ew.Column(23).Width = 50;
                ew.Column(24).Width = 10;
                ew.Column(25).Width = 20;
                ew.Column(26).Width = 20;
                ew.Column(27).Width = 30;

                using (var range = ew.Cells[1, 1, 1, 27])
                {
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Font.Color.SetColor(Color.White);
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkRed);
                }

                List<encuesta_usuariosCLS> listaUser = (List<encuesta_usuariosCLS>)Session["ListaUser"];
                int nroregistros = listaUser.Count();
                for (int i = 0; i < nroregistros; i++)
                {
                    ew.Cells[i + 2, 1].Value = listaUser[i].usua_id;
                    ew.Cells[i + 2, 2].Value = listaUser[i].usua_nombre;
                    ew.Cells[i + 2, 3].Value = listaUser[i].empleado_empresa;
                    ew.Cells[i + 2, 4].Style.Numberformat.Format = "yyyy-mm-dd";
                    ew.Cells[i + 2, 4].Value = listaUser[i].usua_f_aplica;
                    ew.Cells[i + 2, 5].Value = listaUser[i].usua_estatus;
                    ew.Cells[i + 2, 6].Value = listaUser[i].usua_n_usuario;
                    ew.Cells[i + 2, 7].Style.Numberformat.Format = "yyyy-mm-dd";
                    ew.Cells[i + 2, 7].Value = listaUser[i].usua_f_alta;
                    ew.Cells[i + 2, 8].Style.Numberformat.Format = "yyyy-mm-dd";
                    ew.Cells[i + 2, 8].Value = listaUser[i].usua_f_cancela;
                    ew.Cells[i + 2, 9].Value = listaUser[i].empleado_genero;
                    ew.Cells[i + 2, 10].Value = listaUser[i].empleado_edad;
                    ew.Cells[i + 2, 11].Value = listaUser[i].empleado_edocivil;
                    ew.Cells[i + 2, 12].Value = listaUser[i].empleado_sinformacion;
                    ew.Cells[i + 2, 13].Value = listaUser[i].empleado_primaria;
                    ew.Cells[i + 2, 14].Value = listaUser[i].empleado_secundaria;
                    ew.Cells[i + 2, 15].Value = listaUser[i].empleado_preparatoria;
                    ew.Cells[i + 2, 16].Value = listaUser[i].empleado_tecnico;
                    ew.Cells[i + 2, 17].Value = listaUser[i].empleado_licenciatura;
                    ew.Cells[i + 2, 18].Value = listaUser[i].empleado_maestria;
                    ew.Cells[i + 2, 19].Value = listaUser[i].empleado_doctorado;
                    ew.Cells[i + 2, 20].Value = listaUser[i].empleado_tipopuesto;
                    ew.Cells[i + 2, 21].Value = listaUser[i].empleado_tipocontata;
                    ew.Cells[i + 2, 22].Value = listaUser[i].empleado_tipopersonal;
                    ew.Cells[i + 2, 23].Value = listaUser[i].empleado_tipojornada;
                    ew.Cells[i + 2, 24].Value = listaUser[i].empleado_rotacion;
                    ew.Cells[i + 2, 25].Value = listaUser[i].empleado_tiempopuesto;
                    ew.Cells[i + 2, 26].Value = listaUser[i].empleado_explab;
                    ew.Cells[i + 2, 27].Value = listaUser[i].usua_presento;
                }
                ep.SaveAs(ms);
                buffer = ms.ToArray();
            }

            return File(buffer, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

        }
        public FileResult generarExcelByte() 
        {

            byte[] buffer;
            using (MemoryStream ms = new MemoryStream())
            {   
                //Todo el documento excel
                ExcelPackage ep = new ExcelPackage();
                //Crear una hoja
                ep.Workbook.Worksheets.Add("Reporte de Empresas");
                ExcelWorksheet ew = ep.Workbook.Worksheets[1];

                //Ponemos nombres de las columnas
                ew.Cells[1, 1].Value = "ID";
                ew.Cells[1, 2].Value = "Descripción";
                ew.Cells[1, 3].Value = "Estatus";
                ew.Cells[1, 4].Value = "Empleados";
                ew.Cells[1, 5].Value = "Dirección";
                ew.Cells[1, 6].Value = "Telefono";
                ew.Cells[1, 7].Value = "Contacto";
                ew.Cells[1, 8].Value = "Correo";
                ew.Cells[1, 9].Value = "C.P.";

                ew.Column(1).Width = 10;
                ew.Column(2).Width = 50;
                ew.Column(3).Width = 10;
                ew.Column(4).Width = 10;
                ew.Column(5).Width = 50;
                ew.Column(6).Width = 40;
                ew.Column(7).Width = 40;
                ew.Column(8).Width = 40;
                ew.Column(9).Width = 10;

                using (var range = ew.Cells[1,1,1,9]) 
                {
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Font.Color.SetColor(Color.White);
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkRed);
                }

                List<encuesta_empresaCLS> listaEmp = (List<encuesta_empresaCLS>)Session["ListaEmp"];
                int nroregistros = listaEmp.Count();
                for (int i = 0; i < nroregistros; i++)
                {
                    ew.Cells[i + 2, 1].Value = listaEmp[i].emp_id;
                    ew.Cells[i + 2, 2].Value = listaEmp[i].emp_descrip;
                    ew.Cells[i + 2, 3].Value = listaEmp[i].emp_estatus;
                    ew.Cells[i + 2, 4].Value = listaEmp[i].emp_no_trabajadores;
                    ew.Cells[i + 2, 5].Value = listaEmp[i].emp_direccion;
                    ew.Cells[i + 2, 6].Value = listaEmp[i].emp_telefono;
                    ew.Cells[i + 2, 7].Value = listaEmp[i].emp_person_contac;
                    ew.Cells[i + 2, 8].Value = listaEmp[i].emp_correo;
                    ew.Cells[i + 2, 9].Value = listaEmp[i].emp_cp;
                }
                ep.SaveAs(ms);
                buffer=ms.ToArray();
            }

            return File(buffer,"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

         }




        //Generar Reportes en PDF
        public FileResult GenerarPDF() 
        {
            Document doc = new Document(iTextSharp.text.PageSize.A4_LANDSCAPE,5,5,0,0);
            Byte[] buffer;

            using (MemoryStream ms =new MemoryStream()) {

                PdfWriter.GetInstance(doc,ms);
                doc.Open();
                Paragraph title = new Paragraph("Listado de Empresas");
                title.Alignment = Element.ALIGN_CENTER;
                doc.Add(title);

                Paragraph espacio = new Paragraph(" ");
                doc.Add(espacio);

                //Creando la tabla
                PdfPTable tabla = new PdfPTable(8);
                tabla.WidthPercentage = 100f;
                //Asignando los anchos de las columnas
                float[] valores = new float[8] { 10,40,20,20,30,40,40,30};
                tabla.SetWidths(valores);

                //Creando celdas agregando contenido
                PdfPCell celda1 = new PdfPCell(new Phrase("ID"));
                celda1.BackgroundColor = new BaseColor(130,130,130);
                celda1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tabla.AddCell(celda1);
                
                PdfPCell celda2 = new PdfPCell(new Phrase("Descripción"));
                celda2.BackgroundColor = new BaseColor(130, 130, 130);
                celda2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tabla.AddCell(celda2);

                PdfPCell celda3 = new PdfPCell(new Phrase("Estatus"));
                celda3.BackgroundColor = new BaseColor(130, 130, 130);
                celda3.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tabla.AddCell(celda3);

                PdfPCell celda4 = new PdfPCell(new Phrase("Emp"));
                celda4.BackgroundColor = new BaseColor(130, 130, 130);
                celda4.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tabla.AddCell(celda4);

                PdfPCell celda5 = new PdfPCell(new Phrase("Dirección"));
                celda5.BackgroundColor = new BaseColor(130, 130, 130);
                celda5.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tabla.AddCell(celda5);

                PdfPCell celda6 = new PdfPCell(new Phrase("Telefono"));
                celda6.BackgroundColor = new BaseColor(130, 130, 130);
                celda6.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tabla.AddCell(celda6);

                PdfPCell celda7 = new PdfPCell(new Phrase("Contacto"));
                celda7.BackgroundColor = new BaseColor(130, 130, 130);
                celda7.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tabla.AddCell(celda7);

                PdfPCell celda8 = new PdfPCell(new Phrase("Correo"));
                celda8.BackgroundColor = new BaseColor(130, 130, 130);
                celda8.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                tabla.AddCell(celda8);

                //Poniendo datos en la la tabla
                List<encuesta_empresaCLS>listaEmp= (List<encuesta_empresaCLS>)Session["ListaEmp"];
                int nroregistros = listaEmp.Count();
                for (int i = 0; i < nroregistros; i++) {
                    tabla.AddCell(listaEmp[i].emp_id.ToString());
                    tabla.AddCell(listaEmp[i].emp_descrip);
                    tabla.AddCell(listaEmp[i].emp_estatus);
                    tabla.AddCell(listaEmp[i].emp_no_trabajadores);
                    tabla.AddCell(listaEmp[i].emp_direccion);
                    tabla.AddCell(listaEmp[i].emp_telefono);
                    tabla.AddCell(listaEmp[i].emp_person_contac);
                    tabla.AddCell(listaEmp[i].emp_correo);
                }
                //Agregando la tabla al documento
                doc.Add(tabla);
                doc.Close();

                buffer = ms.ToArray();
            
            }
                return File(buffer,"application/pdf");
        
        }










        //Catalogos
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

        public void listarCombos()
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
        }
        // GET: Admin
        [AccederAdmin]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Empleados(encuesta_usuariosCLS empleados_)
        {
            int id_empresa = empleados_.usua_empresa;
            int id_genero = empleados_.usua_genero;
            int id_edo_civ = empleados_.usua_edo_civil;
            int id_puesto = empleados_.usua_tipo_puesto;
            int id_contrata = empleados_.usua_tipo_contratacion;
            int id_personal = empleados_.usua_tipo_personal;
            int id_jornada = empleados_.usua_tipo_jornada;
            int id_tiempo = empleados_.usua_tiempo_puesto;
            int id_expLab = empleados_.usua_exp_laboral;

            listarCombos();

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

            List<encuesta_usuariosCLS> listaEmpleado = null;
            List<encuesta_usuariosCLS> listaRpta = null;
            using (var db = new csstdura_encuestaEntities())
            {
                listaEmpleado = (from empleado in db.encuesta_usuarios
                                 join empresa in db.encuesta_empresa
                                 on empleado.usua_empresa equals empresa.emp_id
                                 join genero in db.encuesta_sexo
                                 on empleado.usua_genero equals genero.sexo_id
                                 join edad_emp in db.encuesta_edades
                                 on empleado.usua_edad equals edad_emp.edad_id
                                 join edo in db.encuesta_edocivil
                                 on empleado.usua_edo_civil equals edo.edocivil_id
                                 join op in db.encuaesta_opciones
                                 on empleado.usua_sin_forma equals op.opcion_id
                                 join primaria in db.encuesta_procesoedu
                                 on empleado.usua_primaria equals primaria.procesoedu_id
                                 join secundaria in db.encuesta_procesoedu
                                 on empleado.usua_secundaria equals secundaria.procesoedu_id
                                 join prepa in db.encuesta_procesoedu
                                 on empleado.usua_preparatoria equals prepa.procesoedu_id
                                 join tecnico in db.encuesta_procesoedu
                                 on empleado.usua_tecnico equals tecnico.procesoedu_id
                                 join lic in db.encuesta_procesoedu
                                 on empleado.usua_licenciatura equals lic.procesoedu_id
                                 join maestria in db.encuesta_procesoedu
                                 on empleado.usua_maestria equals maestria.procesoedu_id
                                 join doc in db.encuesta_procesoedu
                                 on empleado.usua_doctorado equals doc.procesoedu_id
                                 join tipopuesto in db.encuesta_tipopuesto
                                 on empleado.usua_tipo_puesto equals tipopuesto.tipopuesto_id
                                 join tipocont in db.encuesta_tipocontrata
                                 on empleado.usua_tipo_contratacion equals tipocont.tipocont_id
                                 join tipopersonal in db.encuesta_tipopersonal
                                 on empleado.usua_tipo_personal equals tipopersonal.tipoperson_id
                                 join tipojornada in db.encuesta_tipojornada
                                 on empleado.usua_tipo_jornada equals tipojornada.tipojornada_id
                                 join rota in db.encuaesta_rotacion
                                 on empleado.usua_rotacion_turno equals rota.rotacionturno_id
                                 join tiempo in db.encuesta_tiempopuesto
                                 on empleado.usua_tiempo_puesto equals tiempo.tiempopue_id
                                 join exp in db.encuesta_explab
                                 on empleado.usua_exp_laboral equals exp.explab_id
                                 select new encuesta_usuariosCLS
                                 {
                                     usua_id = empleado.usua_id,
                                     usua_nombre = empleado.usua_nombre,
                                     usua_f_aplica = (DateTime)empleado.usua_f_aplica,
                                     usua_estatus = empleado.usua_estatus,
                                     usua_n_usuario = empleado.usua_n_usuario,
                                     usua_p_usuario = empleado.usua_p_usuario,
                                     usua_f_alta = (DateTime)empleado.usua_f_alta,
                                     usua_f_cancela = empleado.usua_f_cancela,
                                     usua_empresa = (int)empleado.usua_empresa,
                                     usua_genero = (int)empleado.usua_genero,
                                     usua_edad = (int)empleado.usua_edad,
                                     usua_edo_civil = (int)empleado.usua_edo_civil,
                                     usua_presento = empleado.usua_presento,
                                     empleado_empresa = empresa.emp_descrip,
                                     empleado_genero = genero.sexo_desc,
                                     empleado_edad = edad_emp.edad_desc,
                                     empleado_edocivil = edo.edocivil_desc,
                                     empleado_sinformacion = op.opcion_desc,
                                     empleado_primaria = primaria.procesoedu_desc,
                                     empleado_secundaria = secundaria.procesoedu_desc,
                                     empleado_preparatoria = prepa.procesoedu_desc,
                                     empleado_tecnico = tecnico.procesoedu_desc,
                                     empleado_licenciatura = lic.procesoedu_desc,
                                     empleado_maestria = maestria.procesoedu_desc,
                                     empleado_doctorado = doc.procesoedu_desc,
                                     empleado_tipopuesto = tipopuesto.tipopuesto_desc,
                                     empleado_tipocontata = tipocont.tipocont_desc,
                                     empleado_tipopersonal = tipopersonal.tipoperson_desc,
                                     empleado_tipojornada = tipojornada.tipojornada_desc,
                                     empleado_rotacion = rota.rotacionturno_desc,
                                     empleado_tiempopuesto = tiempo.tiempopue_desc,
                                     empleado_explab = exp.explab_desc

                                 }).ToList();
                Session["ListaUser"] = listaEmpleado;
                if (empleados_.usua_id == 0 && empleados_.usua_empresa == 0 && empleados_.usua_genero == 0 && empleados_.usua_edad == 0 && empleados_.usua_edo_civil == 0
                    && empleados_.usua_tipo_puesto == 0 && empleados_.usua_tipo_contratacion == 0 && empleados_.usua_tipo_personal == 0
                    && empleados_.usua_tipo_jornada == 0 && empleados_.usua_tiempo_puesto == 0 && empleados_.usua_exp_laboral == 0)
                {

                    listaRpta = listaEmpleado;
                    Session["ListaUser"] = listaEmpleado;
                }
                else
                {
                    if (empleados_.usua_empresa != 0)
                    {
                        listaEmpleado = listaEmpleado.Where(p => p.usua_empresa.Equals(empleados_.usua_empresa)).ToList();
                        Session["ListaUser"] = listaEmpleado;
                    }

                    if (empleados_.usua_genero != 0)
                    {
                        listaEmpleado = listaEmpleado.Where(p => p.usua_genero.ToString().Contains(empleados_.usua_genero.ToString())).ToList();
                        Session["ListaUser"] = listaEmpleado;
                    }

                    if (empleados_.usua_edad != 0)
                    {
                        listaEmpleado = listaEmpleado.Where(p => p.usua_edad.ToString().Contains(empleados_.usua_edad.ToString())).ToList();
                        Session["ListaUser"] = listaEmpleado;
                    }

                    if (empleados_.usua_edo_civil != 0)
                    {
                        listaEmpleado = listaEmpleado.Where(p => p.usua_edo_civil.ToString().Contains(empleados_.usua_edo_civil.ToString())).ToList();
                        Session["ListaUser"] = listaEmpleado;
                    }

                    if (empleados_.usua_tipo_puesto != 0)
                    {
                        listaEmpleado = listaEmpleado.Where(p => p.usua_tipo_puesto.ToString().Contains(empleados_.usua_tipo_puesto.ToString())).ToList();
                        Session["ListaUser"] = listaEmpleado;
                    }

                    if (empleados_.usua_tipo_contratacion != 0)
                    {
                        listaEmpleado = listaEmpleado.Where(p => p.usua_tipo_contratacion.ToString().Contains(empleados_.usua_tipo_contratacion.ToString())).ToList();
                        Session["ListaUser"] = listaEmpleado;
                    }

                    if (empleados_.usua_tipo_personal != 0)
                    {
                        listaEmpleado = listaEmpleado.Where(p => p.usua_tipo_personal.ToString().Contains(empleados_.usua_tipo_personal.ToString())).ToList();
                        Session["ListaUser"] = listaEmpleado;
                    }

                    if (empleados_.usua_tipo_jornada != 0)
                    {
                        listaEmpleado = listaEmpleado.Where(p => p.usua_tipo_jornada.ToString().Contains(empleados_.usua_tipo_jornada.ToString())).ToList();
                        Session["ListaUser"] = listaEmpleado;
                    }

                    if (empleados_.usua_tiempo_puesto != 0)
                    {
                        listaEmpleado = listaEmpleado.Where(p => p.usua_tiempo_puesto.ToString().Contains(empleados_.usua_tiempo_puesto.ToString())).ToList();
                        Session["ListaUser"] = listaEmpleado;
                    }

                    if (empleados_.usua_exp_laboral != 0)
                    {
                        listaEmpleado = listaEmpleado.Where(p => p.usua_exp_laboral.ToString().Contains(empleados_.usua_exp_laboral.ToString())).ToList();
                        Session["ListaUser"] = listaEmpleado;
                    }
                    Session["ListaUser"] = listaEmpleado;
                    listaRpta = listaEmpleado;

                }


            }
            return View(listaRpta);
        }
        [AccederAdmin]
        public ActionResult CatalogoEmpresa()
        {
            //var test = Session["Usuario"].ToString();
            if (Session["Usuario"] != null)
            {
                ViewBag.user = Session["Usuario"].ToString();
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }

        }
        [HttpPost]
        public ActionResult InsertCatalogoEmp(encuesta_empresaCLS Oencuesta_empresaCLS)
        {
            int res = 0;
            using (var db = new csstdura_encuestaEntities())
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {
                        encuesta_empresa empresa = new encuesta_empresa();
                        empresa.emp_descrip = Oencuesta_empresaCLS.emp_descrip;
                        empresa.emp_estatus = "A";
                        empresa.emp_u_alta = Oencuesta_empresaCLS.emp_u_alta;
                        empresa.emp_f_alta = DateTime.Now;
                        empresa.emp_u_cancela = "";
                        empresa.emp_f_cancela = null;
                        empresa.emp_no_trabajadores = Oencuesta_empresaCLS.emp_no_trabajadores;
                        empresa.emp_direccion = Oencuesta_empresaCLS.emp_direccion;
                        empresa.emp_telefono = Oencuesta_empresaCLS.emp_telefono;
                        empresa.emp_person_contac = Oencuesta_empresaCLS.emp_person_contac;
                        empresa.emp_correo = Oencuesta_empresaCLS.emp_correo;
                        empresa.emp_cp = Oencuesta_empresaCLS.emp_cp;
                        db.encuesta_empresa.Add(empresa);
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

                        return Content("<script language='javascript' type='text/javascript'>alert('Registro exitoso!');window.location = '/Admin/CatalogoEmpresa';</script>");

                    }
                    else
                    {

                        return Content("<script language='javascript' type='text/javascript'>alert('Ocurrio un error!');window.location = '/Admin/CatalogoEmpresa';</script>");

                    }

                }
            }

        }
        public ActionResult ListarEmpresa(encuesta_empresaCLS oEmpresa)
        {

            List<encuesta_empresaCLS> listaEmpresa = null;
            string nombre_empresa = oEmpresa.emp_descrip;
            using (var db = new csstdura_encuestaEntities())
            {
                if (oEmpresa.emp_descrip == null)
                {
                    listaEmpresa = (from empresa in db.encuesta_empresa
                                    select new encuesta_empresaCLS
                                    {
                                        emp_id = empresa.emp_id,
                                        emp_descrip = empresa.emp_descrip,
                                        emp_estatus = empresa.emp_estatus,
                                        emp_u_alta = empresa.emp_u_alta,
                                        emp_f_alta = (DateTime)empresa.emp_f_alta,
                                        emp_u_cancela = empresa.emp_u_cancela,
                                        emp_f_cancela = (DateTime)empresa.emp_f_cancela,
                                        emp_no_trabajadores = empresa.emp_no_trabajadores,
                                        emp_direccion = empresa.emp_direccion,
                                        emp_telefono = empresa.emp_telefono,
                                        emp_person_contac = empresa.emp_person_contac,
                                        emp_correo = empresa.emp_correo,
                                        emp_cp = empresa.emp_cp
                                    }).ToList();
                    Session["ListaEmp"] = listaEmpresa;
                }
                else
                {

                    listaEmpresa = (from empresa in db.encuesta_empresa
                                    where empresa.emp_descrip.Contains(nombre_empresa)
                                    select new encuesta_empresaCLS
                                    {
                                        emp_id = empresa.emp_id,
                                        emp_descrip = empresa.emp_descrip,
                                        emp_estatus = empresa.emp_estatus,
                                        emp_u_alta = empresa.emp_u_alta,
                                        emp_f_alta = (DateTime)empresa.emp_f_alta,
                                        emp_u_cancela = empresa.emp_u_cancela,
                                        emp_f_cancela = (DateTime)empresa.emp_f_cancela,
                                        emp_no_trabajadores = empresa.emp_no_trabajadores,
                                        emp_direccion = empresa.emp_direccion,
                                        emp_telefono = empresa.emp_telefono,
                                        emp_person_contac = empresa.emp_person_contac,
                                        emp_correo = empresa.emp_correo,
                                        emp_cp = empresa.emp_cp
                                    }).ToList();
                    Session["ListaEmp"] = listaEmpresa;
                }

            }
            return View(listaEmpresa);
        }
        public ActionResult EditarEmpresa(int id)
        {
            encuesta_empresaCLS Oencuesta_empresaCLS = new encuesta_empresaCLS();

            using (var db = new csstdura_encuestaEntities())
            {
                encuesta_empresa Oencuesta_empresa = db.encuesta_empresa.Where(p => p.emp_id.Equals(id)).First();
                Oencuesta_empresaCLS.emp_id = Oencuesta_empresa.emp_id;
                Oencuesta_empresaCLS.emp_descrip = Oencuesta_empresa.emp_descrip;
                Oencuesta_empresaCLS.emp_estatus = Oencuesta_empresa.emp_estatus;
                Oencuesta_empresaCLS.emp_no_trabajadores = Oencuesta_empresa.emp_no_trabajadores;
                Oencuesta_empresaCLS.emp_direccion = Oencuesta_empresa.emp_direccion;
                Oencuesta_empresaCLS.emp_telefono = Oencuesta_empresa.emp_telefono;
                Oencuesta_empresaCLS.emp_person_contac = Oencuesta_empresa.emp_person_contac;
                Oencuesta_empresaCLS.emp_correo = Oencuesta_empresa.emp_correo;
                Oencuesta_empresaCLS.emp_cp = Oencuesta_empresa.emp_cp;
            }
            return View(Oencuesta_empresaCLS);

        }
        [HttpPost]
        public ActionResult EditarEmpresa(encuesta_empresaCLS Oencuesta_EmpresaCLS)
        {
            if (!ModelState.IsValid)
            {
                return View(Oencuesta_EmpresaCLS);
            }
            int id_empresa = Oencuesta_EmpresaCLS.emp_id;
            using (var db = new csstdura_encuestaEntities())
            {
                encuesta_empresa Oencuesta_empresa = db.encuesta_empresa.Where(p => p.emp_id.Equals(id_empresa)).First();
                Oencuesta_empresa.emp_descrip = Oencuesta_EmpresaCLS.emp_descrip;
                Oencuesta_empresa.emp_estatus = Oencuesta_EmpresaCLS.emp_estatus;
                Oencuesta_empresa.emp_no_trabajadores = Oencuesta_EmpresaCLS.emp_no_trabajadores;
                Oencuesta_empresa.emp_direccion = Oencuesta_EmpresaCLS.emp_direccion;
                Oencuesta_empresa.emp_telefono = Oencuesta_EmpresaCLS.emp_telefono;
                Oencuesta_empresa.emp_person_contac = Oencuesta_EmpresaCLS.emp_person_contac;
                Oencuesta_empresa.emp_correo = Oencuesta_EmpresaCLS.emp_correo;
                Oencuesta_empresa.emp_cp = Oencuesta_EmpresaCLS.emp_cp;
                db.SaveChanges();

            }
            return RedirectToAction("ListarEmpresa");
        }

        public ActionResult EliminarEmpresa(int id)
        {
            encuesta_empresaCLS Oencuesta_empresaCLS = new encuesta_empresaCLS();

            using (var db = new csstdura_encuestaEntities())
            {
                encuesta_empresa Oencuesta_empresa = db.encuesta_empresa.Where(p => p.emp_id.Equals(id)).First();
                Oencuesta_empresaCLS.emp_id = Oencuesta_empresa.emp_id;
                Oencuesta_empresaCLS.emp_descrip = Oencuesta_empresa.emp_descrip;
                Oencuesta_empresaCLS.emp_estatus = Oencuesta_empresa.emp_estatus;
                Oencuesta_empresaCLS.emp_no_trabajadores = Oencuesta_empresa.emp_no_trabajadores;
                Oencuesta_empresaCLS.emp_direccion = Oencuesta_empresa.emp_direccion;
                Oencuesta_empresaCLS.emp_telefono = Oencuesta_empresa.emp_telefono;
                Oencuesta_empresaCLS.emp_person_contac = Oencuesta_empresa.emp_person_contac;
                Oencuesta_empresaCLS.emp_correo = Oencuesta_empresa.emp_correo;
                Oencuesta_empresaCLS.emp_cp = Oencuesta_empresa.emp_cp;
            }
            return View(Oencuesta_empresaCLS);

        }

        [HttpPost]
        public ActionResult EliminarEmpresa(encuesta_empresaCLS Oencuesta_EmpresaCLS)
        {
            if (!ModelState.IsValid)
            {
                return View(Oencuesta_EmpresaCLS);
            }
            int id_empresa = Oencuesta_EmpresaCLS.emp_id;
            using (var db = new csstdura_encuestaEntities())
            {
                encuesta_empresa Oencuesta_empresa = db.encuesta_empresa.Where(p => p.emp_id.Equals(id_empresa)).First();
                Oencuesta_empresa.emp_estatus = "B";
                Oencuesta_empresa.emp_f_cancela = DateTime.Now;

                db.SaveChanges();

            }
            return RedirectToAction("ListarEmpresa");
        }

        public ActionResult EditarUsuarios(int id)
        {
            listarCombos();
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

            encuesta_usuariosCLS Oencuesta_usuarioCLS = new encuesta_usuariosCLS();
            //List<encuesta_usuariosCLS> oUsuarios = null;
            using (var db = new csstdura_encuestaEntities())
            {

                encuesta_usuarios oUsuarios = db.encuesta_usuarios.Where(p => p.usua_id.Equals(id)).First();



                Oencuesta_usuarioCLS.usua_id = oUsuarios.usua_id;
                Oencuesta_usuarioCLS.usua_nombre = oUsuarios.usua_nombre;
                Oencuesta_usuarioCLS.usua_empresa = (int)oUsuarios.usua_empresa;
                //Oencuesta_usuarioCLS.usua_tipo = oUsuarios.usua_tipo;
                Oencuesta_usuarioCLS.usua_n_usuario = oUsuarios.usua_n_usuario;
                Oencuesta_usuarioCLS.usua_genero = (int)oUsuarios.usua_genero;
                Oencuesta_usuarioCLS.usua_edad = (int)oUsuarios.usua_edad;
                Oencuesta_usuarioCLS.usua_edo_civil = (int)oUsuarios.usua_edo_civil;
                Oencuesta_usuarioCLS.usua_sin_forma = (int)oUsuarios.usua_sin_forma;
                Oencuesta_usuarioCLS.usua_primaria = (int)oUsuarios.usua_primaria;
                Oencuesta_usuarioCLS.usua_secundaria = (int)oUsuarios.usua_secundaria;
                Oencuesta_usuarioCLS.usua_preparatoria = (int)oUsuarios.usua_preparatoria;
                Oencuesta_usuarioCLS.usua_tecnico = (int)oUsuarios.usua_tecnico;
                Oencuesta_usuarioCLS.usua_licenciatura = (int)oUsuarios.usua_licenciatura;
                Oencuesta_usuarioCLS.usua_maestria = (int)oUsuarios.usua_maestria;
                Oencuesta_usuarioCLS.usua_doctorado = (int)oUsuarios.usua_doctorado;
                Oencuesta_usuarioCLS.usua_tipo_puesto = (int)oUsuarios.usua_tipo_puesto;
                Oencuesta_usuarioCLS.usua_tipo_contratacion = (int)oUsuarios.usua_tipo_contratacion;
                Oencuesta_usuarioCLS.usua_tipo_personal = (int)oUsuarios.usua_tipo_personal;
                Oencuesta_usuarioCLS.usua_tipo_jornada = (int)oUsuarios.usua_tipo_jornada;
                Oencuesta_usuarioCLS.usua_rotacion_turno = (int)oUsuarios.usua_rotacion_turno;
                Oencuesta_usuarioCLS.usua_tiempo_puesto = (int)oUsuarios.usua_tiempo_puesto;
                Oencuesta_usuarioCLS.usua_exp_laboral = (int)oUsuarios.usua_exp_laboral;
            }
            return View(Oencuesta_usuarioCLS);

        }

        [HttpPost]
        public ActionResult EditarUsuarios(encuesta_usuariosCLS Oencuesta_usuariosCLS)
        {
            if (!ModelState.IsValid)
            {
                return View(Oencuesta_usuariosCLS);
            }
            int id = Oencuesta_usuariosCLS.usua_id;
            using (var db = new csstdura_encuestaEntities())
            {
                //encuesta_usuarios Oencuesta_usuario = db.encuesta_usuarios.Where(p => p.usua_id.Equals(id_usuario)).FirstOrDefault();
                encuesta_usuarios Oencuesta_usuario = db.encuesta_usuarios.Where(p => p.usua_id.Equals(id)).First();

                Oencuesta_usuario.usua_nombre = Oencuesta_usuariosCLS.usua_nombre;
                Oencuesta_usuario.usua_empresa = Oencuesta_usuariosCLS.usua_empresa;
                Oencuesta_usuario.usua_n_usuario = Oencuesta_usuariosCLS.usua_n_usuario;

                //Cifrando el password
                SHA256Managed sha = new SHA256Managed();
                byte[] byteContra = Encoding.Default.GetBytes(Oencuesta_usuariosCLS.usua_p_usuario);
                byte[] byteContraCifrado = sha.ComputeHash(byteContra);
                string contraCifrada = BitConverter.ToString(byteContraCifrado).Replace("-", "");
                Oencuesta_usuario.usua_p_usuario = contraCifrada;

                Oencuesta_usuario.usua_genero = Oencuesta_usuariosCLS.usua_genero;
                Oencuesta_usuario.usua_edad = Oencuesta_usuariosCLS.usua_edad;
                Oencuesta_usuario.usua_edo_civil = Oencuesta_usuariosCLS.usua_edo_civil;
                Oencuesta_usuario.usua_sin_forma = Oencuesta_usuariosCLS.usua_sin_forma;
                Oencuesta_usuario.usua_primaria = Oencuesta_usuariosCLS.usua_primaria;
                Oencuesta_usuario.usua_secundaria = Oencuesta_usuariosCLS.usua_secundaria;
                Oencuesta_usuario.usua_preparatoria = Oencuesta_usuariosCLS.usua_preparatoria;
                Oencuesta_usuario.usua_tecnico = Oencuesta_usuariosCLS.usua_tecnico;
                Oencuesta_usuario.usua_licenciatura = Oencuesta_usuariosCLS.usua_licenciatura;
                Oencuesta_usuario.usua_maestria = Oencuesta_usuariosCLS.usua_maestria;
                Oencuesta_usuario.usua_doctorado = Oencuesta_usuariosCLS.usua_doctorado;
                Oencuesta_usuario.usua_tipo_puesto = Oencuesta_usuariosCLS.usua_tipo_puesto;
                Oencuesta_usuario.usua_tipo_contratacion = Oencuesta_usuariosCLS.usua_tipo_contratacion;
                Oencuesta_usuario.usua_tipo_personal = Oencuesta_usuariosCLS.usua_tipo_personal;
                Oencuesta_usuario.usua_tipo_jornada = Oencuesta_usuariosCLS.usua_tipo_jornada;
                Oencuesta_usuario.usua_rotacion_turno = Oencuesta_usuariosCLS.usua_rotacion_turno;
                Oencuesta_usuario.usua_tiempo_puesto = Oencuesta_usuariosCLS.usua_tiempo_puesto;
                Oencuesta_usuario.usua_exp_laboral = Oencuesta_usuariosCLS.usua_exp_laboral;
                db.SaveChanges();

            }
            return RedirectToAction("Empleados");
        }

        public ActionResult EliminarUsuarios(int id)
        {
            listarCombos();
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

            encuesta_usuariosCLS Oencuesta_usuarioCLS = new encuesta_usuariosCLS();

            using (var db = new csstdura_encuestaEntities())
            {


                encuesta_usuarios oUsuarios = db.encuesta_usuarios.Where(p => p.usua_id.Equals(id)).First();



                Oencuesta_usuarioCLS.usua_id = oUsuarios.usua_id;
                Oencuesta_usuarioCLS.usua_nombre = oUsuarios.usua_nombre;
                Oencuesta_usuarioCLS.usua_empresa = (int)oUsuarios.usua_empresa;
                //Oencuesta_usuarioCLS.usua_tipo = oUsuarios.usua_tipo;
                Oencuesta_usuarioCLS.usua_n_usuario = oUsuarios.usua_n_usuario;
                Oencuesta_usuarioCLS.usua_genero = (int)oUsuarios.usua_genero;
                Oencuesta_usuarioCLS.usua_edad = (int)oUsuarios.usua_edad;
                Oencuesta_usuarioCLS.usua_edo_civil = (int)oUsuarios.usua_edo_civil;
                Oencuesta_usuarioCLS.usua_sin_forma = (int)oUsuarios.usua_sin_forma;
                Oencuesta_usuarioCLS.usua_primaria = (int)oUsuarios.usua_primaria;
                Oencuesta_usuarioCLS.usua_secundaria = (int)oUsuarios.usua_secundaria;
                Oencuesta_usuarioCLS.usua_preparatoria = (int)oUsuarios.usua_preparatoria;
                Oencuesta_usuarioCLS.usua_tecnico = (int)oUsuarios.usua_tecnico;
                Oencuesta_usuarioCLS.usua_licenciatura = (int)oUsuarios.usua_licenciatura;
                Oencuesta_usuarioCLS.usua_maestria = (int)oUsuarios.usua_maestria;
                Oencuesta_usuarioCLS.usua_doctorado = (int)oUsuarios.usua_doctorado;
                Oencuesta_usuarioCLS.usua_tipo_puesto = (int)oUsuarios.usua_tipo_puesto;
                Oencuesta_usuarioCLS.usua_tipo_contratacion = (int)oUsuarios.usua_tipo_contratacion;
                Oencuesta_usuarioCLS.usua_tipo_personal = (int)oUsuarios.usua_tipo_personal;
                Oencuesta_usuarioCLS.usua_tipo_jornada = (int)oUsuarios.usua_tipo_jornada;
                Oencuesta_usuarioCLS.usua_rotacion_turno = (int)oUsuarios.usua_rotacion_turno;
                Oencuesta_usuarioCLS.usua_tiempo_puesto = (int)oUsuarios.usua_tiempo_puesto;
                Oencuesta_usuarioCLS.usua_exp_laboral = (int)oUsuarios.usua_exp_laboral;
            }
            return View(Oencuesta_usuarioCLS);

        }

        public ActionResult ListarEncuesta(encuesta_usuariosCLS empleados_)
        {

            int id_empresa = empleados_.usua_empresa;
           
            listarCombos();

            ViewBag.listaEmpresa = listaEmpresa;

            List<encuesta_usuariosCLS> listaEmpleado = null;
            List<encuesta_usuariosCLS> listaRpta = null;
            using (var db = new csstdura_encuestaEntities())
            {
                listaEmpleado = (from empleado in db.encuesta_usuarios
                                 where empleado.usua_estatus == "ACTIVO"
                                 join empresa in db.encuesta_empresa
                                 on empleado.usua_empresa equals empresa.emp_id
                                 //from empleados in db.encuesta_usuarios
                                 join resultado in db.encuesta_resultados
                                 on empleado.usua_id equals resultado.resu_usua_id
                                 select new encuesta_usuariosCLS
                                 {
                                     usua_id = empleado.usua_id,
                                     usua_nombre = empleado.usua_nombre,
                                     usua_estatus = empleado.usua_estatus,
                                     usua_n_usuario = empleado.usua_n_usuario,
                                     usua_p_usuario = empleado.usua_p_usuario,
                                     usua_empresa = (int)empleado.usua_empresa,
                                     empleado_empresa = empresa.emp_descrip,

                                 }).Distinct().ToList();
                
                    if (empleados_.usua_empresa != 0)
                    {
                        listaEmpleado = listaEmpleado.Where(p => p.usua_empresa.Equals(empleados_.usua_empresa)).ToList();
                    }

                    listaRpta = listaEmpleado;

            }
            return View(listaRpta);
        }

        public ActionResult VerResultadoUsuario(int id)
        {
            ViewBag.id_usuario = id;
            List<encuesta_mostrarPreguntas2CLS> list;
            using (var db = new csstdura_encuestaEntities())
            {
                list = (from resultados in db.encuesta_resultados
                        join det_encuesta in db.encuesta_det_encuesta
                        on resultados.resu_denc_id equals det_encuesta.denc_id
                        where resultados.resu_usua_id == id
                        && det_encuesta.denc_parte == 1
                        select new encuesta_mostrarPreguntas2CLS
                        {
                            resu_usua_id = id,
                            denc_descrip = det_encuesta.denc_descrip,
                            resu_resultado = resultados.resu_resultado,
                            denc_parte = det_encuesta.denc_parte,
                        }).ToList();
                string nombreEmpleado = db.Database.SqlQuery<string>("select usua_nombre from encuesta_usuarios where usua_id = '" + id + "'").FirstOrDefault();
                ViewBag.nombreEmpleado = nombreEmpleado;
            }
            Console.WriteLine("Hello World");
            return View(list);

        }

        public ActionResult VerResultadoUsuario2(int id)
        {
            ViewBag.id_usuario = id;
            List<encuesta_mostrarPreguntas2CLS> list;
            using (var db = new csstdura_encuestaEntities())
            {
                list = (from resultados in db.encuesta_resultados
                        join det_encuesta in db.encuesta_det_encuesta
                        on resultados.resu_denc_id equals det_encuesta.denc_id
                        where resultados.resu_usua_id == id
                        && det_encuesta.denc_parte == 2
                        select new encuesta_mostrarPreguntas2CLS
                        {
                            resu_usua_id = id,
                            denc_descrip = det_encuesta.denc_descrip,
                            resu_resultado = resultados.resu_resultado,
                            denc_parte = det_encuesta.denc_parte,
                        }).ToList();

            }
            Console.WriteLine("Hello World");
            return View(list);

        }

        public ActionResult VerResultadoUsuario3(int id)
        {
            ViewBag.id_usuario = id;
            List<encuesta_mostrarPreguntas2CLS> list;
            using (var db = new csstdura_encuestaEntities())
            {
                list = (from resultados in db.encuesta_resultados
                        join det_encuesta in db.encuesta_det_encuesta
                        on resultados.resu_denc_id equals det_encuesta.denc_id
                        where resultados.resu_usua_id == id
                        && det_encuesta.denc_parte == 3
                        select new encuesta_mostrarPreguntas2CLS
                        {
                            resu_usua_id = id,
                            denc_descrip = det_encuesta.denc_descrip,
                            resu_resultado = resultados.resu_resultado,
                            denc_parte = det_encuesta.denc_parte,
                        }).ToList();

            }
            Console.WriteLine("Hello World");
            return View(list);

        }

        public ActionResult VerResultadoUsuario4(int id)
        {
            ViewBag.id_usuario = id;
            List<encuesta_mostrarPreguntas2CLS> list;
            using (var db = new csstdura_encuestaEntities())
            {
                list = (from resultados in db.encuesta_resultados
                        join det_encuesta in db.encuesta_det_encuesta
                        on resultados.resu_denc_id equals det_encuesta.denc_id
                        where resultados.resu_usua_id == id
                        && det_encuesta.denc_parte == 4
                        select new encuesta_mostrarPreguntas2CLS
                        {
                            resu_usua_id = id,
                            denc_descrip = det_encuesta.denc_descrip,
                            resu_resultado = resultados.resu_resultado,
                            denc_parte = det_encuesta.denc_parte,
                        }).ToList();

            }
            Console.WriteLine("Hello World");
            return View(list);

        }
    }
}