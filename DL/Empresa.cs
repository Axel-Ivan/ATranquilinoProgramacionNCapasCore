using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Empresa
    {
        public Empresa()
        {
            Empleados = new HashSet<Empleado>();
        }

        public int IdEmpresa { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Telefono { get; set; }
        public string Email { get; set; } = null!;
        public string DireccionWeb { get; set; } = null!;
        public byte[]? Logo { get; set; }

        public virtual ICollection<Empleado> Empleados { get; set; }
    }
}
