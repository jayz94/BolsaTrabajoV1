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
    
    public partial class ESTADO_CIVIL_POSTULANTE_MM
    {
        public int IDPOSTULANTE { get; set; }
        public short IDESTADOCIVIL { get; set; }
        public Nullable<System.DateTime> FECHACAMBIO { get; set; }
        public Nullable<short> ACTIVO { get; set; }
    
        public virtual ESTADO_CIVIL ESTADO_CIVIL { get; set; }
        public virtual POSTULANTE POSTULANTE { get; set; }
    }
}
