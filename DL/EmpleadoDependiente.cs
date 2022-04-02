using System;
using System.Collections.Generic;

namespace DL
{
    public partial class EmpleadoDependiente
    {
        public int IdEmpleadoDependiente { get; set; }
        public int IdEmpleado { get; set; }
        public int IdDependiente { get; set; }

        public virtual Dependiente IdDependienteNavigation { get; set; } = null!;
        public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;
    }
}
