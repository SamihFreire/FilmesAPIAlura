using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
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
        private IMapper _mapper;

        public FilmeController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper  = mapper;
        }

        [HttpPost] //Verbo de criação
        public IActionResult AdionaFilme([FromBody] CreateFilmeDto filmeDto) // [FROMBODY] indica que o filme vem atraves do corpo da requisição
        {
            Filme filme = _mapper.Map<Filme>(filmeDto); //UTILIZANDO AUTOMAPPER PARA CONVERTER filmeDto para o tipo Filme

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
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            //return filmes.FirstOrDefault(x => x.Id == id);

            if(filme != null)
            {
                ReadFilmeDto filmeDto = _mapper.Map<ReadFilmeDto>(filme);

                return Ok(filmeDto);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if(filme == null)
            {
                return NotFound();
            }

            _mapper.Map(filmeDto, filme); //CONVRETENDO 2 OBJETOS ENTRE SI ( Sobrescrevendo as informações de filmeDto para o filme)

            /*  filme.Titulo  = filmeDto.Titulo;
                filme.Genero  = filmeDto.Genero;
                filme.Duracao = filmeDto.Duracao;   //PASSANDO OS DADOS ATUALIZADOS DO FILME - ONDE FOI ATUALIZADO PELO USO DO AutoMapper FEITO ACIMA
                filme.Diretor = filmeDto.Diretor;
            */

            _context.SaveChanges();

            return NoContent(); // BOA PRATICA QUANDO ATULIZAR DADOS, RETORNAR NoContent();
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
