using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Empleado
    {
        public int IdEmpleado { get; set; }
        public string Rfc { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string ApellidoMaterno { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public DateTime? FechaNacimiento { get; set; }
        public string Nss { get; set; } = null!;
        public DateTime FechaIngreso { get; set; }
        public string? Foto { get; set; }
        public int IdEmpresa { get; set; }
        public int IdPoliza { get; set; }

        public virtual Empresa IdEmpresaNavigation { get; set; } = null!;
        public virtual Poliza IdPolizaNavigation { get; set; } = null!;

        public string? EmpresaNombre { get; set; } = null;
        public string? PolizaNombre { get; set; } = null;
        public byte? IdSubPoliza { get; set; }
        public string? NumeroPoliza { get; set; } = null;
        public int? IdUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; } = null;
        public DateTime? FechaModificacion { get; set; } = null;

    }
}
