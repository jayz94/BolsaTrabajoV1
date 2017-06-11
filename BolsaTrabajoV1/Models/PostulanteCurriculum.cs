using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BolsaTrabajoV1.Models
{
    public class PostulanteCurriculum
    {
        public String nombre { get; set; }
        public String genero { get; set; }
        public int edad { get; set; }
        public String institucion { get; set; }
        public String habilidad { get; set; }
        public String idioma { get; set; }
        public String titulo { get; set; }
        public int idpostulante { get; set; }
        public String municipio { get; set; }
    }
}