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
        [Required(ErrorMessage ="El nombre de usuario es obligatorio.")]
        public string UserName { get; set; }

        [Display(Name = "Contraseña: ")]
        [DataType(DataType.Password)]
        [Required]
        public string Contrasenia { get; set; }

        [Display(Name = "Nombre: ")]
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; }

        [Display(Name = "Apellido Paterno: ")]
        [Required(ErrorMessage = "El apellido paterno del usuario es obligatorio.")]
        public string ApellidoPaterno { get; set; }

        [Display(Name = "Apellido Materno: ")]
        [Required(ErrorMessage = "El apellido materno del usuario es obligatorio.")]
        public string ApellidoMaterno { get; set; }

        [Display(Name = "Email: ")]
        [Required(ErrorMessage = "El email es obligatorio.")]
        public string Email { get; set; }

        [Display(Name = "Sexo: ")]
        [Required(ErrorMessage = "El sexo del usuario es obligatorio.")]
        public string Sexo { get; set; }

        [Display(Name = "Telefono: ")]
        [Required(ErrorMessage = "El teléfono del usuario es obligatorio.")]
        public string Telefono { get; set; }

        [Display(Name = "Celular: ")]
        [Required(ErrorMessage = "Elcelular del usuario es obligatorio.")]
        public string Celular { get; set; }

        [Display(Name = "Fecha de Nacimiento: ")]
        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        public string FechaNacimiento { get; set; }

        [Display(Name = "Estatus: ")]
        [Required(ErrorMessage = "El estatus es obligatorio.")]
        public byte Estatus { get; set; }

        [Display(Name = "CURP: ")]
        [Required(ErrorMessage = "El CURP del usuario es obligatorio.")]
        public string CURP { get; set; }

        [Display(Name = "Imagen: ")]
        [Required(ErrorMessage = "La foto del usuario es obligatoria.")]
        public byte[] Imagen { get; set; }

        [Display(Name = "Rol: ")]
        [Required(ErrorMessage = "El rol del usuario es obligatorio.")]
        public ML.Rol Rol { get; set; }
        public ML.Direccion Direccion { get; set; }

        public List<object> Usuarios { get; set; }
    }
}
