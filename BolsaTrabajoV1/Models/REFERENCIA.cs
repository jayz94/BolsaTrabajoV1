
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
    
public partial class REFERENCIA
{

    public int IDREFERENCIA { get; set; }

    public int IDTIPOREFERENCIA { get; set; }

    public int IDCURRICULUM { get; set; }

    public int IDPOSTULANTE { get; set; }

    public string DESCRIPCIONREFERENCIA { get; set; }

    public string TELEFONOREFERENCIA { get; set; }

    public string PUESTOREFERENCIA { get; set; }

    public string PERSONAREFERENCIA { get; set; }

    public string EMPRESAORIGENREFERENCIA { get; set; }



    public virtual CURRICULUM CURRICULUM { get; set; }

    public virtual TIPOREFERENCIA TIPOREFERENCIA { get; set; }

}

}
