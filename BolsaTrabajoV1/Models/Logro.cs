//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BolsaTrabajoV1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class LOGRO
    {
        public int IDLOGRO { get; set; }
        public int IDCURRICULUM { get; set; }
        public int IDPOSTULANTE { get; set; }
        public string TITULOLOGRO { get; set; }
        public Nullable<System.DateTime> FECHALOGRO { get; set; }
        public string OTORGANTELOGRO { get; set; }
        public string DESCRIPCIONLOGRO { get; set; }
        public Nullable<bool> PREMIO { get; set; }
        public Nullable<bool> LABORSOCIAL { get; set; }
    
        public virtual CURRICULUM CURRICULUM { get; set; }
    }
}
