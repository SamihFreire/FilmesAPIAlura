using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FilmesAPI.Controllers
{ 
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private static List<Filme> filmes = new List<Filme>();

        [HttpPost] //Verbo de criação
        public void AdionaFilme([FromBody] Filme filme) // [FROMBODY] indica que o filme vem atraves do corpo da requisição
        {
            filmes.Add(filme);
        }
    }
}
