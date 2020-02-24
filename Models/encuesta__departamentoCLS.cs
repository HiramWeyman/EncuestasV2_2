using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EncuestasV2.Models
{
    public class encuesta__departamentoCLS
    {
        [Key]
        [Display(Name = "ID")]
        public int dep_id { get; set; }
        [Display(Name = "Nombre de Departamento")]
        public string dep_desc { get; set; }
        [Display(Name = "ID Empresa")]
        public int dep_empresa { get; set; }
        [Display(Name = "Nombre de Empresa")]
        public string dep_empresa_desc { get; set; }

    }
}
