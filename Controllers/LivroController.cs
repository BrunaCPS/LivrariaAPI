using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LivrariaAPI.Dtos;
using LivrariaAPI.Interfaces;
using LivrariaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LivrariaAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroService _livroService;

        public LivroController(ILivroService livroService)
        {
            _livroService = livroService;
        }

        [HttpGet("ListarLivros")]
        public async Task<ActionResult<Response<List<Livro>>>> ListarLivros()
        {
            var livros = await _livroService.ListarLivros();
            return Ok(livros);
        }

        [HttpGet("BuscarLivroPorId/{idLivro}")]
        public async Task<ActionResult<Response<Livro>>> BuscarLivroPorId(int idLivro)
        {
            var livro = await _livroService.BuscarLivroPorId(idLivro);
            return Ok(livro);
        }

        [HttpGet("BuscarLivroPorIdAutor/{idAutor}")]
        public async Task<ActionResult<Response<Livro>>> BuscarLivroPorIdLivro(int idAutor)
        {
            var livro = await _livroService.BuscarLivroPorIdAutor(idAutor);
            return Ok(livro);
        }

        [HttpPost("CriarLivro")]
        public async Task<ActionResult<List<Response<Livro>>>> CriarLivro(LivroCriacaoDto livroCriacaoDto)
        {
            var novoLivro = await _livroService.CriarLivro(livroCriacaoDto);
            return Ok(novoLivro);
        }


        [HttpPut("EditarLivro")]
        public async Task<ActionResult<Response<List<Livro>>>> EditarLivro(LivroEdicaoDto livroEdicaoDto)
        {
            var livros = await _livroService.EditarLivro(livroEdicaoDto);
            return Ok(livros);
        }

        [HttpDelete("ExcluirLivro/{idLivro}")]
        public async Task<ActionResult<Response<List<Livro>>>> ExcluirLivro(int idLivro)
        {
            var livros = await _livroService.ExcluirLivro(idLivro);
            return Ok(livros);
        }
    }
}