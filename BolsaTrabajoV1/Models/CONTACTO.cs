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
    
    public partial class CONTACTO
    {
        public int IDCONTACTO { get; set; }
        public int IDPOSTULANTE { get; set; }
        public short IDTIPOCONTACTO { get; set; }
        public string VINCULO { get; set; }
    
        public virtual POSTULANTE POSTULANTE { get; set; }
        public virtual TIPO_CONTACTO TIPO_CONTACTO { get; set; }
    }
}