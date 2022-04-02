using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Dependiente
    {
        public Dependiente()
        {
            EmpleadoDependientes = new HashSet<EmpleadoDependiente>();
        }

        public int IdDependiente { get; set; }
        public int IdEmpleado { get; set; }
        public string? Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? EstadoCivil { get; set; }
        public string? Genero { get; set; }
        public string? Telefono { get; set; }
        public string? Rfc { get; set; }
        public byte IdDependienteTipo { get; set; }

        public virtual DependienteTipo IdDependienteTipoNavigation { get; set; } = null!;
        public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;
        public virtual ICollection<EmpleadoDependiente> EmpleadoDependientes { get; set; }

        public string DependienteTipoNombre { get; set; } = null;
    }
}
