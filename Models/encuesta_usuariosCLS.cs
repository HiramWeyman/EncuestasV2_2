using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace EncuestasV2.Models
{
    public class encuesta_usuariosCLS
    {

        [Key]
        public int usua_id { get; set; }

        [Display(Name = "Nombre Completo")]

        public string usua_nombre { get; set; }

        [Display(Name = "Empresa")]

        public int usua_empresa { get; set; }
        public DateTime usua_f_aplica { get; set; }
        public string usua_tipo { get; set; }
        public string usua_estatus { get; set; }

        [Display(Name = "Nombre de usuario")]
        [Required]
        public string usua_n_usuario { get; set; }

        [Display(Name = "Password")]
        [Required]
        [MinLength(6)]
        public string usua_p_usuario { get; set; }
        public string usua_u_alta { get; set; }

        public DateTime usua_f_alta { get; set; }
        public string usua_u_cancela { get; set; }

        public Nullable<DateTime> usua_f_cancela { get; set; }

        [Display(Name = "Sexo")]
        public int usua_genero { get; set; }

        [Display(Name = "Edad")]

        public int usua_edad { get; set; }

        [Display(Name = "Estado Civil")]

        public int usua_edo_civil { get; set; }

        [Display(Name = "Sin formación")]

        public int usua_sin_forma { get; set; }

        [Display(Name = "Primaria")]

        public int usua_primaria { get; set; }

        [Display(Name = "Secundaria")]

        public int usua_secundaria { get; set; }

        [Display(Name = "Preparaoria")]

        public int usua_preparatoria { get; set; }

        [Display(Name = "Técnico o Superior")]

        public int usua_tecnico { get; set; }

        [Display(Name = "Licenciatura")]

        public int usua_licenciatura { get; set; }

        [Display(Name = "Maestría")]

        public int usua_maestria { get; set; }

        [Display(Name = "Doctorado")]

        public int usua_doctorado { get; set; }

        [Display(Name = "Tipo de puesto")]

        public int usua_tipo_puesto { get; set; }

        [Display(Name = "Tipo de contratación")]

        public int usua_tipo_contratacion { get; set; }

        [Display(Name = "Tipo de personal")]

        public int usua_tipo_personal { get; set; }

        [Display(Name = "Tipo de jornada")]

        public int usua_tipo_jornada { get; set; }

        [Display(Name = "Rotación de turno")]

        public int usua_rotacion_turno { get; set; }

        [Display(Name = "Tiempo en el puesto")]

        public int usua_tiempo_puesto { get; set; }

        [Display(Name = "Experiencia laboral")]

        public int usua_exp_laboral { get; set; }

        public string usua_presento { get; set; }

        ///propiedades adicionales////
        public string empleado_empresa { get; set; }
        public string empleado_genero { get; set; }
        public string empleado_edad { get; set; }
        public string empleado_edocivil { get; set; }
        public string empleado_sinformacion { get; set; }
        public string empleado_primaria { get; set; }
        public string empleado_secundaria { get; set; }
        public string empleado_preparatoria { get; set; }
        public string empleado_tecnico { get; set; }
        public string empleado_licenciatura { get; set; }
        public string empleado_maestria { get; set; }
        public string empleado_doctorado { get; set; }
        public string empleado_tipopuesto { get; set; }
        public string empleado_tipocontata { get; set; }
        public string empleado_tipopersonal { get; set; }
        public string empleado_tipojornada { get; set; }
        public string empleado_rotacion { get; set; }
        public string empleado_tiempopuesto { get; set; }
        public string empleado_explab { get; set; }

    }
}
