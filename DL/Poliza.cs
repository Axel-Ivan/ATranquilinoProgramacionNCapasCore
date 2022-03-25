using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Poliza
    {
        public Poliza()
        {
            Empleados = new HashSet<Empleado>();
        }

        public int IdPoliza { get; set; }
        public string? Nombre { get; set; }
        public byte IdSubPoliza { get; set; }
        public string NumeroPoliza { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int IdUsuario { get; set; }

        public virtual SubPoliza IdSubPolizaNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
        public virtual ICollection<Empleado> Empleados { get; set; }
    }
}
