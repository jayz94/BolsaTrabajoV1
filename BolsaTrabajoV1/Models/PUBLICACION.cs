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
    
    public partial class PUBLICACION
    {
        public int IDPUBLICACION { get; set; }
        public int IDCURRICULUM { get; set; }
        public int IDPOSTULANTE { get; set; }
        public string TITULOPUBLICACION { get; set; }
        public string EDITORIAL { get; set; }
        public string REVISTA { get; set; }
        public System.DateTime FECHAPUBLICACION { get; set; }
        public string ISBN { get; set; }
        public Nullable<bool> LIBRO { get; set; }
        public string NUMEROEDICION { get; set; }
    
        public virtual CURRICULUM CURRICULUM { get; set; }
    }
}
