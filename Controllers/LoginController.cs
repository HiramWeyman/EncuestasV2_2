using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EncuestasV2.Models;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;

namespace EncuestasV2.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

      
        public string Login(encuesta_usuariosCLS oUsuarios) 
        {
            string  mensaje = "";
 
            //Error
            if(!ModelState.IsValid) 
            {
                var query = (from state in ModelState.Values
                             from error in state.Errors
                             select error.ErrorMessage).ToList();

                mensaje += "<ul class='list-group'> </ul>";
                foreach (var item in query) {
                    mensaje += "<li class='list-group-item'>"+item+"</li>";
                }
                mensaje += "</ul>";
            }
            //Bien
            else
            {
                string nombre_usuario = oUsuarios.usua_n_usuario;
                string password = oUsuarios.usua_p_usuario;

                
                //Cifrando el password
                SHA256Managed sha = new SHA256Managed();
                byte[] byteContra = Encoding.Default.GetBytes(password);
                byte[] byteContraCifrado = sha.ComputeHash(byteContra);
                string contraCifrada = BitConverter.ToString(byteContraCifrado).Replace("-", "");
              
                using (var db = new csstdura_encuestaEntities())
                {

                    int numeroVeces = db.encuesta_usuarios.Where(p => p.usua_n_usuario == nombre_usuario
                                      && p.usua_p_usuario == contraCifrada && p.usua_presento=="N").Count();
      
                    mensaje = numeroVeces.ToString();
                    if (mensaje == "0")
                    {
                        mensaje = "Usuario o contraseña Incorrecto";
                    }
                    else {
                        string usuario = db.Database.SqlQuery<string>("Select usua_n_usuario from encuesta_usuarios where usua_n_usuario=@usuario and usua_p_usuario=@password", new SqlParameter("@usuario", nombre_usuario), new SqlParameter("@password", contraCifrada))
                      .FirstOrDefault();
                        Session["Usuario"] = usuario;
                        Console.WriteLine(usuario);
                    }

                }

            }

                return mensaje;
        }

        public string LoginAdmin(encuesta_usuariosCLS oUsuarios)
        {
            string mensaje = "";

            //Error
            if (!ModelState.IsValid)
            {
                var query = (from state in ModelState.Values
                             from error in state.Errors
                             select error.ErrorMessage).ToList();

                mensaje += "<ul class='list-group'> </ul>";
                foreach (var item in query)
                {
                    mensaje += "<li class='list-group-item'>" + item + "</li>";
                }
                mensaje += "</ul>";
            }
            //Bien
            else
            {
                string nombre_usuario = oUsuarios.usua_n_usuario;
                string password = oUsuarios.usua_p_usuario;


                //Cifrando el password
                SHA256Managed sha = new SHA256Managed();
                byte[] byteContra = Encoding.Default.GetBytes(password);
                byte[] byteContraCifrado = sha.ComputeHash(byteContra);
                string contraCifrada = BitConverter.ToString(byteContraCifrado).Replace("-", "");

                using (var db = new csstdura_encuestaEntities())
                {

                    int numeroVeces = db.encuesta_usuarios.Where(p => p.usua_n_usuario == nombre_usuario
                                      && p.usua_p_usuario == contraCifrada && p.usua_tipo=="A").Count();

                    mensaje = numeroVeces.ToString();
                    if (mensaje == "0")
                    {
                        mensaje = "Usuario o contraseña Incorrecto";
                    }
                    else
                    {
                        //List<encuesta_usuariosCLS> usuario = db.encuesta_usuarios.Where(p => p.usua_n_usuario == nombre_usuario
                        //                && p.usua_p_usuario == contraCifrada && p.usua_tipo == "A").Select(p => new encuesta_usuariosCLS { usua_n_usuario = p.usua_n_usuario }).ToList();

                        //var results = db.Database.ExecuteSqlCommand("Select usua_n_usuario from encuesta_usuarios where usua_n_usuario="+ nombre_usuario+ "and usua_p_usuario="+ contraCifrada+"and usua_tipo ='A'");
                        string tipo = "A";
                        string usuario = db.Database.SqlQuery<string>("Select usua_n_usuario from encuesta_usuarios where usua_n_usuario=@usuario and usua_p_usuario=@password and usua_tipo=@tipo", new SqlParameter("@usuario", nombre_usuario), new SqlParameter("@password", contraCifrada), new SqlParameter("@tipo", tipo))
                           .FirstOrDefault();
                        Session["UsuarioAdmin"] = usuario;
                        Console.WriteLine(usuario);
                    }

                }

            }

            return mensaje;

        }

        public ActionResult cerrarSession() {
            Session["Usuario"] = null;
            return RedirectToAction("Index", "Login");
        }
        public ActionResult cerrarSessionAdmin()
        {
            Session["Usuario"] = null;
            return RedirectToAction("Admin", "Usuarios");
        }
    }
}