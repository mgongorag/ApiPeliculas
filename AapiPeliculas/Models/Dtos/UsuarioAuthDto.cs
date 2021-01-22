using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPeliculas.Models.Dtos
{
    public class UsuarioAuthDto
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El usuario es obligatorio")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "La password es obligatorio")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "La contraseña debe de tener un minimo de 8 caracteres")]
        public string Password { get; set; }
    }
}
