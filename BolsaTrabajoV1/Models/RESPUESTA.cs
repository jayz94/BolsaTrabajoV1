//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BolsaTrabajoV1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class RESPUESTA
    {
        public int IDOPCIONPREGUNTA { get; set; }
        public int IDPOSTULANTE { get; set; }
        public string TEXTORESPUESTA { get; set; }
    
        public virtual OPCION_PREGUNTA OPCION_PREGUNTA { get; set; }
        public virtual POSTULANTE POSTULANTE { get; set; }
    }
}
