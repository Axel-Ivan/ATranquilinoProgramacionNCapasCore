﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace ML
{
    public class Aseguradora
    {
        public int IdAseguradora { get; set; }

        [Display(Name = "Nombre: ")]
        public string? Nombre { get; set; }

        public string? FechaCreacion { get; set; }
        public string? FechaModificacion { get; set; }
        public ML.Usuario? Usuario { get; set; }

        [Display(Name = "Logo: ")]
        public string? Imagen { get; set; }
        public List<object>? Aseguradoras { get; set; }
    }
}
