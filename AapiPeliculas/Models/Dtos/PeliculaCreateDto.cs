using AapiPeliculas.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPeliculas.Models.Dtos
{
    public class PeliculaCreateDto
    {
        
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El i es obligatorio")]
        public string RutaImagen { get; set; }

        public IFormFile Foto { get; set; }

        [Required(ErrorMessage = "La descripcion es obligatorio")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "La duracion es obligatoria")]
        public string Duracion { get; set; }

        public enum TipoClasificacion { Siete, Trece, Dieciseis, Dieciosho }

        public int categoriaId { get; set; }
    }
}
