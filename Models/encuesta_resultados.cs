//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EncuestasV2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class encuesta_resultados
    {
        public int resu_id { get; set; }
        public Nullable<int> resu_emp_id { get; set; }
        public Nullable<int> resu_encu_id { get; set; }
        public Nullable<int> resu_denc_id { get; set; }
        public Nullable<int> resu_usua_id { get; set; }
        public string resu_resultado { get; set; }
        public Nullable<System.DateTime> resu_fecha { get; set; }
    
        public virtual encuesta_det_encuesta encuesta_det_encuesta { get; set; }
        public virtual encuesta_empresa encuesta_empresa { get; set; }
        public virtual encuesta_encuesta encuesta_encuesta { get; set; }
        public virtual encuesta_usuarios encuesta_usuarios { get; set; }
    }
}
