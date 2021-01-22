using AapiPeliculas.Models;
using AapiPeliculas.Models.Dtos;
using ApiPeliculas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPeliculas.Repository.IRepository
{
    public interface IUsuarioRepository
    {
        ICollection<Usuario> GetUsuarios();
        Categoria GetUsuario(int UsuarioId);
        bool ExisteUsuario(String usuario);
       
        Usuario Registro(Usuario usuario, string password);
        Usuario Login(string usuario, string password);

        bool Guardar();

    }
}
