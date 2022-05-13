using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace FilmesAPI.Controllers
{ 
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private static List <Filme> filmes = new List<Filme>();

        private static int id = 1;

        [HttpPost] //Verbo de criação
        public IActionResult AdionaFilme([FromBody] Filme filme) // [FROMBODY] indica que o filme vem atraves do corpo da requisição
        {
            filme.Id = id++;
            filmes.Add(filme);
            return CreatedAtAction(nameof(RecuperaFilmesPorId), new {id = filme.Id}, filme);  //O CreatedAction retorna que pode ser recuperado pelo metodo RecuperaFilmesPorId, com id passado e o recurso filmes
        }

        [HttpGet] //Verbo de recebimiento
        public IActionResult RecuperaFilmes()
        {
            if(filmes != null)
            {
                return Ok(filmes);
            }
            return NotFound();
        }


        [HttpGet("{id}")] //Representa que o ID vai ser passado na URL da requisição, ex.: localhost:5000/controller/id onde controller é o primeiro nome da classe
        public IActionResult RecuperaFilmesPorId( int id) //O tipo IActionResult possibilita retornar os status de Ok, NotFound....
        {
            var filme = filmes.FirstOrDefault(x => x.Id == id);
            //return filmes.FirstOrDefault(x => x.Id == id);

            if(filme == null)
            {
                return NotFound();
            }
            return Ok(filme);
        }
    }
}
