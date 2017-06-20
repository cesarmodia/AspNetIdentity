using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetIdentity.ViewModels
{
    public class CreateViewModel
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Email { get; set; }
        public string Telefono { get; set; }
        [Required]
        public string Clave { get; set; }
    }
}