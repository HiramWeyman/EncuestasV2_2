using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EncuestasV2.Models
{
    public class encuesta_mostrarPreguntasCLS
    {
        public int denc_id { get; set; }
        public string denc_descrip { get; set; }

        public string denc_valor_1 { get; set; }

        public string denc_valor_2 { get; set; }

        public int resu_emp_id { get; set; }

        public int encu_id { get; set; }

        public int denc_parte { get; set; }

    }
}