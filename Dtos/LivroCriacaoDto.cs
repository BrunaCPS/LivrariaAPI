using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LivrariaAPI.Models;

namespace LivrariaAPI.Dtos
{
    public class LivroCriacaoDto
    {
        public string Titulo { get; set; }
        public AutorVinculo Autor { get; set; }
    }
}