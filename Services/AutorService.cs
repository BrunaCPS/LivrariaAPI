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
    public class AutorService : IAutorService
    {
        private readonly LivrariaDbContext _context;

        public AutorService(LivrariaDbContext context)
        {
            _context = context;
        }
        public async Task<Response<Autor>> BuscarAutorPorId(int idAutor)
        {
            //criar objeto do tipo Response
            Response<Autor> resposta = new Response<Autor>();

            //implementar try catch para tratar erros
            try
            {
                var autor = await _context.Autores.FirstOrDefaultAsync(autorBanco => autorBanco.Id == idAutor);

                if (autor == null)
                {
                    resposta.Mensagem = "Nenhum autor com esse ID foi encontrado";
                    resposta.Status = HttpStatusCode.NotFound;
                    return resposta;
                }

                resposta.Dados = autor;
                resposta.Mensagem = "Autor encontrado";
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

        public async Task<Response<Autor>> BuscarAutorPorIdLivro(int idLivro)
        {
            //criar objeto resposta
            Response<Autor> resposta = new Response<Autor>();

            try
            {
                //Include() entra no Livro e quando chega na propriedade AutorId, pega todas as outras propriedades (Nome, Sobrenome)
                var livro = await _context.Livros
                .Include(a => a.Autor)
                .FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);

                if (livro == null)
                {
                    resposta.Mensagem = "Não foi encontrado nenhum autor com o ID desse livro";
                    resposta.Status = HttpStatusCode.NotFound;
                    return resposta;
                }

                resposta.Dados = livro.Autor;
                resposta.Mensagem = "Autor buscado pelo Id do livro foi encontrado";
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

        public async Task<Response<List<Autor>>> CriarAutor(AutorCriacaoDto autorCriacaoDto)
        {
            Response<List<Autor>> resposta = new Response<List<Autor>>();

            try
            {
                //criar novo objeto do tipo Autor
                var novoAutor = new Autor()
                {
                    Nome = autorCriacaoDto.Nome,
                    Sobrenome = autorCriacaoDto.Sobrenome
                };
                //acrescentar e salvar no banco
                _context.Add(novoAutor);
                await _context.SaveChangesAsync();

                //exibir lista de todos os autores + o novo
                resposta.Dados = await _context.Autores.ToListAsync();
                resposta.Mensagem = "Autor criado com sucesso";
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

        public async Task<Response<List<Autor>>> EditarAutor(AutorEdicaoDto autorEdicaoDto)
        {
            Response<List<Autor>> resposta = new Response<List<Autor>>();

            try
            {
                var autorExistente = await _context.Autores.FirstOrDefaultAsync(autorBanco => autorBanco.Id == autorEdicaoDto.Id);

                if (autorExistente == null)
                {
                    resposta.Mensagem = "Autor não foi encontrado";
                    resposta.Status = HttpStatusCode.NotFound;
                    return resposta;
                }

                autorExistente.Nome = autorEdicaoDto.Nome;
                autorExistente.Sobrenome = autorEdicaoDto.Sobrenome;

                _context.Update(autorExistente);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Autores.ToListAsync();
                resposta.Mensagem = "Autor editado com sucesso";
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

        public async Task<Response<List<Autor>>> ExcluirAutor(int idAutor)
        {
            Response<List<Autor>> resposta = new Response<List<Autor>>();

            try
            {
                var autorExistente = await _context.Autores.FirstOrDefaultAsync(autor => autor.Id == idAutor);

                if (autorExistente == null)
                {
                    resposta.Mensagem = "Não há nenhum registro de um autor com esse ID";
                    resposta.Status = HttpStatusCode.NotFound;
                    return resposta;
                }
                _context.Remove(autorExistente);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Autores.ToListAsync();
                resposta.Mensagem = "Autor excluído com sucesso";
                resposta.Status = HttpStatusCode.OK;
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = HttpStatusCode.NotFound;
                return resposta;

            }
        }

        public async Task<Response<List<Autor>>> ListarAutores()
        {
            //criar objeto do tipo Response
            Response<List<Autor>> resposta = new Response<List<Autor>>();

            //implementar try catch para tratar erros
            try
            {
                var autores = await _context.Autores.ToListAsync();

                resposta.Mensagem = "Todos os autores cadastrados foram listados!";
                resposta.Status = HttpStatusCode.OK;
                resposta.Dados = autores;
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