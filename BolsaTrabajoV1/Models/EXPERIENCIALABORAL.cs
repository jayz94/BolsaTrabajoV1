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
    
    public partial class EXPERIENCIALABORAL
    {
        public int IDEXPERIENCIALABORAL { get; set; }
        public int IDCURRICULUM { get; set; }
        public int IDPOSTULANTE { get; set; }
        public Nullable<int> IDCARGO { get; set; }
        public string INSTITUCION { get; set; }
        public string TITULOPROYECTO { get; set; }
        public System.DateTime FECHAINICIO { get; set; }
        public System.DateTime FECHAFIN { get; set; }
        public string NUMEROCONTACTOORG { get; set; }
    
        public virtual CARGO CARGO { get; set; }
        public virtual CURRICULUM CURRICULUM { get; set; }
    }
}
