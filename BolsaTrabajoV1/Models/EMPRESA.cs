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
    
    public partial class EMPRESA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EMPRESA()
        {
            this.EXAMEN = new HashSet<EXAMEN>();
            this.PLAZA = new HashSet<PLAZA>();
            this.USUARIO = new HashSet<USUARIO>();
        }
    
        public int CODIGOEMPRESA { get; set; }
        public Nullable<int> IDMUNICIPIO { get; set; }
        public Nullable<int> IDGIRO { get; set; }
        public string NOMBREEMPRESA { get; set; }
        public string DESCRIPCION { get; set; }
        public string ABREVIATURA { get; set; }
        public string NIT { get; set; }
        public string CORREOELECTRONICO { get; set; }
        public string TELEFONO { get; set; }
        public byte[] IMAGENEMPRESA { get; set; }
    
        public virtual GIRO GIRO { get; set; }
        public virtual MUNICIPIO MUNICIPIO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXAMEN> EXAMEN { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PLAZA> PLAZA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<USUARIO> USUARIO { get; set; }
    }
}
