using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        [Display(Name = "User Name: ")]
        public string? UserName { get; set; }

        [Display(Name = "Contraseña: ")]
        [DataType(DataType.Password)]
        public string? Contrasenia { get; set; }

        [Display(Name = "Nombre: ")]
        public string? Nombre { get; set; }

        [Display(Name = "Apellido Paterno: ")]
        public string? ApellidoPaterno { get; set; }

        [Display(Name = "Apellido Materno: ")]
        public string? ApellidoMaterno { get; set; }

        [Display(Name = "Email: ")]
        public string? Email { get; set; }

        [Display(Name = "Sexo: ")]
        public string? Sexo { get; set; }

        [Display(Name = "Telefono: ")]
        public string? Telefono { get; set; }

        [Display(Name = "Celular: ")]
        public string? Celular { get; set; }

        [Display(Name = "Fecha de Nacimiento: ")]
        public string? FechaNacimiento { get; set; }

        [Display(Name = "Estatus: ")]
        public byte Estatus { get; set; }

        [Display(Name = "CURP: ")]
        public string? CURP { get; set; }

        [Display(Name = "Imagen: ")]
        public string? Imagen { get; set; }

        [Display(Name = "Rol: ")]
        public ML.Rol? Rol { get; set; }
        public ML.Direccion? Direccion { get; set; }

        public List<object>? Usuarios { get; set; }
    }
}
