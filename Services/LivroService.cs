using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LivrariaAPI.Data;
using LivrariaAPI.Dtos;
using LivrariaAPI.Interfaces;
using LivrariaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LivrariaAPI.Services
{
    public class LivroService : ILivroService
    {
        private readonly LivrariaDbContext _context;
        public LivroService(LivrariaDbContext context)
        {
            _context = context;
        }
        public async Task<Response<Livro>> BuscarLivroPorId(int idLivro)
        {
            Response<Livro> resposta = new Response<Livro>();

            try
            {
                var livro = await _context.Livros.Include(autor => autor.Autor).FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);

                if (livro == null)
                {
                    resposta.Mensagem = "Nenhum livro com esse ID foi encontrado";
                    resposta.Status = HttpStatusCode.NotFound;
                    return resposta;
                }

                resposta.Dados = livro;
                resposta.Mensagem = "Livro encontrado";
                resposta.Status = HttpStatusCode.OK;
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = HttpStatusCode.BadRequest;
                return resposta;
            }
        }

        public async Task<Response<List<Livro>>> BuscarLivroPorIdAutor(int idAutor)
        {
            Response<List<Livro>> resposta = new Response<List<Livro>>();

            try
            {
                //include para acessar todas as propriedades do autor
                var livro = await _context.Livros.Include(autor => autor.Autor).Where(livroBanco => livroBanco.Autor.Id == idAutor).ToListAsync();

                if (livro == null)
                {
                    resposta.Mensagem = "Nao há registro de nenhum autor com esse ID";
                    resposta.Status = HttpStatusCode.NotFound;
                    return resposta;
                }

                resposta.Dados = livro;
                resposta.Mensagem = "Todos os livros do autor ID passado foram listados";
                resposta.Status = HttpStatusCode.OK;
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = HttpStatusCode.BadRequest;
                return resposta;
            }
        }

        public async Task<Response<List<Livro>>> CriarLivro(LivroCriacaoDto livroCriacaoDto)
        {
            Response<List<Livro>> resposta = new Response<List<Livro>>();

            try
            {
                //autor tem que existir para que seja criado um livro
                var autor = await _context.Autores.FirstOrDefaultAsync(autorBanco => autorBanco.Id == livroCriacaoDto.Autor.Id);

                if (autor == null)
                {
                    resposta.Mensagem = "Nenhum registro de autor com esse ID foi encontrado";
                    resposta.Status = HttpStatusCode.NotFound;
                    return resposta;
                }

                var novoLivro = new Livro()
                {
                    Titulo = livroCriacaoDto.Titulo,
                    Autor = autor
                };

                _context.Add(novoLivro);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Livros.Include(autor => autor.Autor).ToListAsync();
                resposta.Mensagem = "Livro criado com sucesso";
                resposta.Status = HttpStatusCode.Created;
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = HttpStatusCode.BadRequest;
                return resposta;
            }
        }

        public async Task<Response<List<Livro>>> EditarLivro(LivroEdicaoDto livroEdicaoDto)
        {
            Response<List<Livro>> resposta = new Response<List<Livro>>();

            try
            {
                //editar livro
                var livro = await _context.Livros.Include(autor => autor.Autor).FirstOrDefaultAsync(livroBanco => livroBanco.Id == livroEdicaoDto.Id);

                //editar autor do livro
                var autor = await _context.Autores.FirstOrDefaultAsync(autorBanco => autorBanco.Id == livroEdicaoDto.Autor.Id);

                if (livro == null)
                {
                    resposta.Mensagem = "Nenhum registro de livro com esse ID foi encontrado";
                    resposta.Status = HttpStatusCode.NotFound;
                    return resposta;
                }

                if (autor == null)
                {
                    resposta.Mensagem = "Nenhum registro de autor com esse ID foi encontrado";
                    resposta.Status = HttpStatusCode.NotFound;
                    return resposta;
                }

                livro.Titulo = livroEdicaoDto.Titulo;
                livro.Autor = autor;

                _context.Update(livro);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Livros.Include(autor => autor.Autor).ToListAsync();
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = HttpStatusCode.BadRequest;
                return resposta;
            }
        }

        public async Task<Response<List<Livro>>> ExcluirLivro(int idLivro)
        {
            Response<List<Livro>> resposta = new Response<List<Livro>>();

            try
            {
                var livro = await _context.Livros.Include(autor => autor.Autor).FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);

                if (livro == null)
                {
                    resposta.Mensagem = "Não há nenhum resgitro de um livro com esse ID";
                    resposta.Status = HttpStatusCode.NotFound;
                    return resposta;
                }

                _context.Remove(livro);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Livros.Where(livroBanco => livroBanco.Id != idLivro).ToListAsync();
                resposta.Mensagem = "Livro excluído com sucesso";
                resposta.Status = HttpStatusCode.OK;
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = HttpStatusCode.BadRequest;
                return resposta;
            }
        }

        public async Task<Response<List<Livro>>> ListarLivros()
        {
            Response<List<Livro>> resposta = new Response<List<Livro>>();

            try
            {
                var livros = await _context.Livros.Include(autor => autor.Autor).ToListAsync();

                resposta.Dados = livros;
                resposta.Mensagem = "Todos os livros cadastrados foram listados!";
                resposta.Status = HttpStatusCode.OK;
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = HttpStatusCode.BadRequest;
                return resposta;
            }

        }
    }
}