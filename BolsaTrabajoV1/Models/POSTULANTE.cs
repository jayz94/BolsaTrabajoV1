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
    
    public partial class POSTULANTE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public POSTULANTE()
        {
            this.CONTACTO = new HashSet<CONTACTO>();
            this.CURRICULUM = new HashSet<CURRICULUM>();
            this.ESTADO_CIVIL_POSTULANTE_MM = new HashSet<ESTADO_CIVIL_POSTULANTE_MM>();
            this.POSTULANTE_TIPO_DOCUMENTO_MM = new HashSet<POSTULANTE_TIPO_DOCUMENTO_MM>();
            this.POSTULANTE_PLAZA = new HashSet<POSTULANTE_PLAZA>();
            this.RESPUESTA = new HashSet<RESPUESTA>();
            this.TELEFONO = new HashSet<TELEFONO>();
        }
    
        public int IDPOSTULANTE { get; set; }
        public int IDUSUARIO { get; set; }
        public Nullable<int> IDMUNICIPIO { get; set; }
        public Nullable<int> IDGENERO { get; set; }
        public string NOMBREPOSTULANTE { get; set; }
        public string APELLIDOPOSTULANTE { get; set; }
        public Nullable<System.DateTime> FECHANACIMIENTO { get; set; }
        public string DIRECCION { get; set; }
        public string URLCURRICULUM { get; set; }
        public byte[] IMAGENPOSTULANTE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONTACTO> CONTACTO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CURRICULUM> CURRICULUM { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ESTADO_CIVIL_POSTULANTE_MM> ESTADO_CIVIL_POSTULANTE_MM { get; set; }
        public virtual GENERO GENERO { get; set; }
        public virtual MUNICIPIO MUNICIPIO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<POSTULANTE_TIPO_DOCUMENTO_MM> POSTULANTE_TIPO_DOCUMENTO_MM { get; set; }
        public virtual USUARIO USUARIO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<POSTULANTE_PLAZA> POSTULANTE_PLAZA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RESPUESTA> RESPUESTA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TELEFONO> TELEFONO { get; set; }
    }
}
