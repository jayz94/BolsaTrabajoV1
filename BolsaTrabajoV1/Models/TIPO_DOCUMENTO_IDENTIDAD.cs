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
    
    public partial class TIPO_DOCUMENTO_IDENTIDAD
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TIPO_DOCUMENTO_IDENTIDAD()
        {
            this.POSTULANTE_TIPO_DOCUMENTO_MM = new HashSet<POSTULANTE_TIPO_DOCUMENTO_MM>();
        }
    
        public int IDTIPODOCUMENTOIDENTIDAD { get; set; }
        public string NOMBREDOCUMENTOIDENTIDAD { get; set; }
        public Nullable<short> CANTIDADCARACTERES { get; set; }
        public string MASCARA { get; set; }
        public Nullable<bool> EXTRANJERO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<POSTULANTE_TIPO_DOCUMENTO_MM> POSTULANTE_TIPO_DOCUMENTO_MM { get; set; }
    }
}
