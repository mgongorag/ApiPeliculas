using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AapiPeliculas.Models;
using AapiPeliculas.Models.Dtos;
using ApiPeliculas.Models;
using ApiPeliculas.Models.Dtos;
using ApiPeliculas.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPeliculas.Controllers
{
    [Route("api/Peliculas")]
    [ApiController]
    public class PeliculasController : Controller
    {
        private readonly IPeliculaRepository _pelRepo;
        private readonly IWebHostEnvironment _hostinEnviroment;
        private readonly IMapper _mapper;
        public PeliculasController(IPeliculaRepository pelRepo, IMapper mapper, IWebHostEnvironment hostinEnviroment)
        {
            _pelRepo = pelRepo;
            _mapper = mapper;
            _hostinEnviroment = hostinEnviroment;
        }

        [HttpGet]
        public IActionResult GetPeliculas()
        {
            var listaPeliculas = _pelRepo.GetPeliculas();

            var listaPeliculasDto = new List<PeliculaDto>();

            foreach (var lista in listaPeliculas)
            {
                listaPeliculasDto.Add(_mapper.Map<PeliculaDto>(lista));
            }

            return Ok(listaPeliculasDto);
        }
        [HttpGet("{peliculaId:int}", Name = "GetPelicula")]
        public IActionResult GetPelicula(int peliculaId)
        {
            var itemPelicula = _pelRepo.GetPelicula(peliculaId);
            if (itemPelicula == null)
            {
                return NotFound();
            }

            var itemPeliculaDto = _mapper.Map<PeliculaDto>(itemPelicula);
            return Ok(itemPeliculaDto);


        }
        [HttpPost]
        public IActionResult CrearPelicula([FromForm] PeliculaCreateDto peliculaDto)
        {
            if (peliculaDto.Nombre == null)
            {
                return BadRequest(ModelState);
            }



            if (_pelRepo.ExistePelicula(peliculaDto.Nombre))
            {
                ModelState.AddModelError("", "La Pelicula ya existe");
                return StatusCode(404, ModelState);
            }



            /**SUBIDA DE ARCHIVOS**/
            var archivo = peliculaDto.Foto;
            string rutaPrincipal = _hostinEnviroment.WebRootPath;
            var archivos = HttpContext.Request.Form.Files;
            if (archivo != null)
            {
                if (archivo.Length > 0)
                {
                    //Nombre para la imagen
                    var nombreFoto = Guid.NewGuid().ToString();

                    //Path donde se va a guardar el archivo
                    var subidas = Path.Combine(rutaPrincipal, @"fotos");
                    var extension = Path.GetExtension(archivos[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreFoto + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }
                    peliculaDto.RutaImagen = @"\fotos\" + nombreFoto.Substring(0, 5) + extension;

                }
            }
           

            var pelicula = _mapper.Map<Pelicula>(peliculaDto);

            if (!_pelRepo.CrearPelicula(pelicula))
            {
                ModelState.AddModelError("", $"Algo salio mal, guardando el registro {pelicula.Nombre}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetPelicula", new { peliculaId = pelicula.Id }, pelicula);
        }

        //Se agrega el parametro que se va a recibir y el metodo que se va a ejecutar
        [HttpPatch("{peliculaId:int}", Name = "ActualizarPelicula")]
        public IActionResult ActualizarPelicula(int peliculaId, [FromBody] PeliculaDto peliculaDto)
        {
            if (peliculaDto == null || peliculaId != peliculaDto.Id)
            {
                return BadRequest(ModelState);
            }

            var pelicula = _mapper.Map<Pelicula>(peliculaDto);

            if (!_pelRepo.ActualizarPelicula(pelicula))
            {
                ModelState.AddModelError("Error", $"Algo salio mal actualizando el registro{pelicula.Nombre}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("ActualizarPelicula", new { peliculaId = pelicula.Id }, pelicula);
        }

        [HttpGet("categoria/{categoriaId:int}")]
        public IActionResult GetPeliculasEnCategoria(int categoriaId)
        {
            var listadoPelicula = _pelRepo.GetPeliculasEnCategoria(categoriaId);
            if(listadoPelicula.Count() == 0)
            {

                return NotFound(ModelState);

            }
            var itemPelicula = new List<PeliculaDto>();
            foreach (var item in listadoPelicula)
            {
                itemPelicula.Add(_mapper.Map<PeliculaDto>(item));
            }
            return Ok(listadoPelicula);
        }
        [HttpGet("buscar")]
        public IActionResult Buscar(String nombre)
        {
            try
            {
                var resultado = _pelRepo.BuscarPelicula(nombre);
                if (resultado.Any())
                {
                    return Ok(resultado);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error, recuperando datos de la aplicacion");
            }
        }


    }
}
