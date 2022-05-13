using FilmesAPI.Data;
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

        private FilmeContext _context;

        public FilmeController(FilmeContext context)
        {
            _context = context;
        }

        [HttpPost] //Verbo de criação
        public IActionResult AdionaFilme([FromBody] Filme filme) // [FROMBODY] indica que o filme vem atraves do corpo da requisição
        {
            _context.Filmes.Add(filme);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaFilmesPorId), new {id = filme.Id}, filme);  //O CreatedAction retorna que pode ser recuperado pelo metodo RecuperaFilmesPorId, com id passado e o recurso filmes
        }

        [HttpGet] //Verbo de recebimiento
        public IEnumerable<Filme> RecuperaFilmes()
        {
            return _context.Filmes;
        }


        [HttpGet("{id}")] //Representa que o ID vai ser passado na URL da requisição, ex.: localhost:5000/controller/id onde controller é o primeiro nome da classe
        public IActionResult RecuperaFilmesPorId( int id) //O tipo IActionResult possibilita retornar os status de Ok, NotFound....
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            //return filmes.FirstOrDefault(x => x.Id == id);

            if(filme != null)
            {
                return Ok(filme);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaFilme(int id, [FromBody] Filme filmeNovo)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if(filme == null)
            {
                return NotFound();
            }

            filme.Titulo  = filmeNovo.Titulo;
            filme.Genero  = filmeNovo.Genero;
            filme.Duracao = filmeNovo.Duracao;
            filme.Diretor = filmeNovo.Diretor;

            _context.SaveChanges();

            return NoContent(); // BOA PRATICA QUANDO ATULIZADO DADOS RETORNAR NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaFilme(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            if(filme == null)
            {
                return NotFound();
            }
            _context.Remove(filme);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
