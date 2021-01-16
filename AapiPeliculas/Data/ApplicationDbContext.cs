using AapiPeliculas.Models;
using ApiPeliculas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AapiPeliculas.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        //DBSet sirve para que se cree la tabla en las migraciones
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Pelicula> Pelicula{ get; set; }

    }
}
