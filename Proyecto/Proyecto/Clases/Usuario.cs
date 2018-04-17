﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Librería_de_Clases
{
    public class Usuario
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "El Nombre del Usuario es Requerido")]
        public string Nombre { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El Apellido del Usuario es Requerida")]
        public string Apellido { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La Edad del Usuario es Requerida")]
        public int Edad { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El Nombre de Usuario es Requerido")]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La Contraseña de Usuario es Requerida")]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La Contraseña de Usuario es Requerida")]
        public bool Logeado { get; set; }

        public Usuario(string Nombre, string Apellido, int Edad, string Username, string Password)
        {
            this.Nombre = Nombre;
            this.Apellido = Apellido;
            this.Edad = Edad;
            this.Username = Username;
            this.Password = Password;
            this.Logeado = true;
        }


        public Usuario()
        {
            this.Nombre = null;
            this.Apellido = null;
            this.Edad = 0;
            this.Username = null;
            this.Password = null;
            this.Logeado = false;
        }
    }
}
