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
    
    public partial class ViewAplicacion
    {
        public int IDPLAZA { get; set; }
        public string NOMBRECARGO { get; set; }
        public string DESCRIPCIONPLAZA { get; set; }
        public Nullable<float> SALARIO { get; set; }
        public int CODIGOEMPRESA { get; set; }
        public string NOMBREEMPRESA { get; set; }
        public Nullable<System.DateTime> FECHAAPLICACION { get; set; }
        public int IDPOSTULANTE { get; set; }
        public string POSTULANTE { get; set; }
        public string DESCRIPCIONGENERO { get; set; }
    }
}