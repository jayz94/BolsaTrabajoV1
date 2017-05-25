using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BolsaTrabajoV1.Models
{
    public class MenusAsignadosViewModel
    {
        public int IDMENU { get; set; }
        public string NOMBREMENU { get; set; }
        public bool asignado { get; set; }
    }
}