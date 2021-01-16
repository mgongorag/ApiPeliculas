using AapiPeliculas.Models;
using AapiPeliculas.Models.Dtos;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPeliculas.PeliculasMapper
{
    //Se vincula el DTO con el modelo Categoria con Mapper
    public class PeliculasMappers : Profile
    {
        public PeliculasMappers()
        {
            CreateMap<Categoria, CategoriaDto>().ReverseMap();
        }
    }
}
