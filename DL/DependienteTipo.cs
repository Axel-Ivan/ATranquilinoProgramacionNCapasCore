using System;
using System.Collections.Generic;

namespace DL
{
    public partial class DependienteTipo
    {
        public DependienteTipo()
        {
            Dependientes = new HashSet<Dependiente>();
        }

        public byte IdDependienteTipo { get; set; }
        public string? Nombre { get; set; }

        public virtual ICollection<Dependiente> Dependientes { get; set; }
    }
}
