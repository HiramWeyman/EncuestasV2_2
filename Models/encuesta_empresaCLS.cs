using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace EncuestasV2.Models
{
    public class encuesta_empresaCLS
    {
        [Key]
        [Display(Name = "ID")]
        public int emp_id { get; set; }

        [Display(Name = "Nombre de la Empresa o Institución")]
        public string emp_descrip { get; set; }
        [Display(Name = "Estatus")]
        public string emp_estatus { get; set; }
        public string emp_u_alta { get; set; }
        public DateTime? emp_f_alta { get; set; }
        public string emp_u_cancela { get; set; }
        public DateTime? emp_f_cancela { get; set; }

        [Display(Name = "Número de Trabajadores")]
        [Required]
        public string emp_no_trabajadores { get; set; }
        [Display(Name = "Dirección de la Empresa o Institución")]
        public string emp_direccion { get; set; }

        [Display(Name = "Telefono")]
        public string emp_telefono { get; set; }

        [Display(Name = "Persona de contacto")]
        public string emp_person_contac { get; set; }
        [Display(Name = "Correo")]
        public string emp_correo { get; set; }
        [Display(Name = "C.P.")]
        public string emp_cp { get; set; }
    }
}