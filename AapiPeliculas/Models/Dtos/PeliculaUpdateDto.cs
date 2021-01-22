﻿using AapiPeliculas.Models;
using AapiPeliculas.Models.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static ApiPeliculas.Models.Pelicula;

namespace ApiPeliculas.Models.Dtos
{
    public class PeliculaUpdateDto
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El i es obligatorio")]
        public string RutaImagen { get; set; }
        [Required(ErrorMessage = "La descripcion es obligatorio")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "La duracion es obligatoria")]
        public string Duracion { get; set; }

        public  TipoClasificacion Clasificacion{ get;set; }

        public int categoriaId { get; set; }

        public Categoria Categoria { get; set; }
    }
}
