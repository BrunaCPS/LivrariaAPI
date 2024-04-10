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
    public class AutorController : ControllerBase
    {
        private readonly IAutorService _autorService;

        public AutorController(IAutorService autorService)
        {
            _autorService = autorService;
        }

        [HttpGet("ListarAutores")]
        public async Task<ActionResult<Response<List<Autor>>>> ListarAutores()
        {
            var autores = await _autorService.ListarAutores();
            return Ok(autores);
        }

        [HttpGet("BuscarAutorPorId/{idAutor}")]
        public async Task<ActionResult<Response<Autor>>> BuscarAutorPorId(int idAutor)
        {
            var autor = await _autorService.BuscarAutorPorId(idAutor);
            return Ok(autor);
        }

        [HttpGet("BuscarAutorPorIdLivro/{idLivro}")]
        public async Task<ActionResult<Response<Autor>>> BuscarAutorPorIdLivro(int idLivro)
        {
            var autor = await _autorService.BuscarAutorPorIdLivro(idLivro);
            return Ok(autor);
        }

        [HttpPost("CriarAutor")]
        public async Task<ActionResult<List<Response<Autor>>>> CriarAutor(AutorCriacaoDto autorCriacaoDto)
        {
            var novoAutor = await _autorService.CriarAutor(autorCriacaoDto);
            return Ok(novoAutor);
        }


        [HttpPut("EditarAutor")]
        public async Task<ActionResult<Response<List<Autor>>>> EditarAutor(AutorEdicaoDto autorEdicaoDto)
        {
            var autores = await _autorService.EditarAutor(autorEdicaoDto);
            return Ok(autores);
        }

        [HttpDelete("ExcluirAutor/{idAutor}")]
        public async Task<ActionResult<Response<List<Autor>>>> ExcluirAutor(int idAutor)
        {
            var autores = await _autorService.ExcluirAutor(idAutor);
            return Ok(autores);
        }
    }
}