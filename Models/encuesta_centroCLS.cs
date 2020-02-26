using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace EncuestasV2.Models
{
    public class encuesta_centroCLS
    {
        [Key]
        [Display(Name = "ID")]
        public int centro_id { get; set; }
        [Display(Name = "Nombre de Centro de Trabajo")]
        public string centro_desc { get; set; }
        [Display(Name = "ID Empresa")]
        public Nullable<int> centro_empresa { get; set; }
        [Display(Name = "Nombre de la Empresa")]
        public string centro_empresa_desc { get; set; }
        [Display(Name = "ID Departamento")]
        public Nullable<int> centro_depto { get; set; }
        [Display(Name = "Nombre de Departamento")]
        public string centro_depto_desc { get; set; }
    }
}