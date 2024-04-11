using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LivrariaAPI.Dtos;
using LivrariaAPI.Models;

namespace LivrariaAPI.Interfaces
{
    public interface ILivroService
    {
        Task<Response<List<Livro>>> ListarLivros();
        Task<Response<Livro>> BuscarLivroPorId(int idLivro);
        Task<Response<List<Livro>>> BuscarLivroPorIdAutor(int idAutor);
        Task<Response<List<Livro>>> CriarLivro(LivroCriacaoDto livroCriacaoDto);
        Task<Response<List<Livro>>> EditarLivro(LivroEdicaoDto livroEdicaoDto);
        Task<Response<List<Livro>>> ExcluirLivro(int idLivro);
    }
}