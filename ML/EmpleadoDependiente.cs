using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class EmpleadoDependiente
    {
        public int IdEmpleadoDependiente { get; set; }
        public ML.Empleado Empleado { get; set; }
        public ML.Dependiente Dependiente { get; set; }
    }
}
