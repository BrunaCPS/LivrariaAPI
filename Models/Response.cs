using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LivrariaAPI.Models
{
    public class Response<T>
    {
        public T? Dados { get; set; } //Pode ser nulo
        public string Mensagem { get; set; } = string.Empty;
        public HttpStatusCode Status { get; set; }
    }
}