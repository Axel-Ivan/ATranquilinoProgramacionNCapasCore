using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Usuario
    {
        public Usuario()
        {
            Aseguradoras = new HashSet<Aseguradora>();
            Direccions = new HashSet<Direccion>();
            Polizas = new HashSet<Poliza>();
        }

        public int IdUsuario { get; set; }
        public string UserName { get; set; } = null!;
        public string Contrasenia { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string Email { get; set; } = null!;
        public string Sexo { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string? Celular { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public bool Estatus { get; set; }
        public string? Curp { get; set; }
        public string? Imagen { get; set; }
        public byte IdRol { get; set; }

        public virtual Rol IdRolNavigation { get; set; } = null!;
        public virtual ICollection<Aseguradora> Aseguradoras { get; set; }
        public virtual ICollection<Direccion> Direccions { get; set; }
        public virtual ICollection<Poliza> Polizas { get; set; }

        public string? RolNombre { get; set; } = null;
        public int? IdDireccion { get; set; }
        public string? Calle { get; set; } = null;
        public string? NumeroInterior { get; set; } = null;
        public string? NumeroExterior { get; set; } = null;
        public int? IdColonia { get; set; }
        public string? ColoniaNombre { get; set; } = null;
        public string? CodigoPostal { get; set; } = null;
        public int? IdMunicipio { get; set; }
        public string? MunicipioNombre { get; set; } = null;
        public int? IdEstado { get; set; }
        public string? EstadoNombre { get; set; } = null;
        public int? IdPais { get; set; }
        public string? PaisNombre { get; set; } = null;
    }
}
