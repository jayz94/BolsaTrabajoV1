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
    
    public partial class USUARIO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USUARIO()
        {
            this.POSTULANTE = new HashSet<POSTULANTE>();
        }
    
        public int IDUSUARIO { get; set; }
        public Nullable<int> CODIGOEMPRESA { get; set; }
        public Nullable<short> IDROL { get; set; }
        public string NOMBREUSUARIO { get; set; }
        public string PASSWORD { get; set; }
        public Nullable<bool> ACTIVO { get; set; }
        public string COLOR { get; set; }
        public string IDIOMA { get; set; }
        public Nullable<bool> BLOQUEADO { get; set; }
        public Nullable<int> INTENTOS { get; set; }
        public string CORREO { get; set; }
    
        public virtual EMPRESA EMPRESA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<POSTULANTE> POSTULANTE { get; set; }
        public virtual ROL ROL { get; set; }
    }
}
