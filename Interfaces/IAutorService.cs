using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LivrariaAPI.Dtos;
using LivrariaAPI.Models;

namespace LivrariaAPI.Interfaces
{
    public interface IAutorService
    {
        Task<Response<List<Autor>>> ListarAutores();
        Task<Response<Autor>> BuscarAutorPorId(int idAutor);
        Task<Response<Autor>> BuscarAutorPorIdLivro(int idLivro);
        Task<Response<List<Autor>>> CriarAutor(AutorCriacaoDto autorCriacaoDto);
        Task<Response<List<Autor>>> EditarAutor(AutorEdicaoDto autorEdicaoDto);
        Task<Response<List<Autor>>> ExcluirAutor(int idAutor);
    }
}