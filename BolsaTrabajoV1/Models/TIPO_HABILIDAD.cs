
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
    
public partial class TIPO_HABILIDAD
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public TIPO_HABILIDAD()
    {

        this.HABILIDAD = new HashSet<HABILIDAD>();

    }


    public int IDTIPOHABILIDAD { get; set; }

    public Nullable<short> IDAREACONOCIMIENTO { get; set; }

    public string NOMBRETIPOHABILIDAD { get; set; }



    public virtual AREA_CONOCIMIENTO AREA_CONOCIMIENTO { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<HABILIDAD> HABILIDAD { get; set; }

}

}
