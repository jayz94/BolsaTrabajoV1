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
    
    public partial class TIPO_PARTICIPACION
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TIPO_PARTICIPACION()
        {
            this.PARTICIPACION_PROFESIONAL = new HashSet<PARTICIPACION_PROFESIONAL>();
        }
    
        public int IDTIPOPARTICIPACION { get; set; }
        public string NOMBRETIPOPARTICIPACION { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PARTICIPACION_PROFESIONAL> PARTICIPACION_PROFESIONAL { get; set; }
    }
}
