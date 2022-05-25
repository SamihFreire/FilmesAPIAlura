using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using FilmesAPI.Services;
using FluentResults;
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
        private FilmeService _filmeService;
        public FilmeController(FilmeService filmeService)
        {
            _filmeService = filmeService;
        }

        [HttpPost] //Verbo de criação
        public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto) // [FROMBODY] indica que o filme vem atraves do corpo da requisição
        {
            ReadFilmeDto readDto = _filmeService.AdicionaFilme(filmeDto);

            return CreatedAtAction(nameof(RecuperaFilmesPorId), new {id = readDto.Id}, readDto);  //O CreatedAction retorna que pode ser recuperado pelo metodo RecuperaFilmesPorId, com id passado e o recurso filmes
        }

        [HttpGet] //Verbo de recebimiento
        public IActionResult RecuperaFilmes([FromQuery] int ? classificacaoEtaria = null) //recebendo um Query Parameters na requisição pelo postMan ex.: https://localhost:5001/filme?classificacaoEtaria=6
        {
            List<ReadFilmeDto> readDto = _filmeService.RecuperaFilmes(classificacaoEtaria);
            if(readDto == null)
            {
                return NotFound();                            
            }        
            return Ok(readDto);
                        
            
            //return _context.Filmes;
        }


        [HttpGet("{id}")] //Representa que o ID vai ser passado na URL da requisição, ex.: localhost:5000/controller/id onde controller é o primeiro nome da classe
        public IActionResult RecuperaFilmesPorId( int id) //O tipo IActionResult possibilita retornar os status de Ok, NotFound....
        {
            ReadFilmeDto readDto = _filmeService.RecuperaFilmesPorId(id);
            if(readDto != null)
            {
                return Ok(readDto);
            }
            return NotFound();
        }

        [HttpPut("{id}")] //VERBO DE ATUALIZAÇÃO
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
        {
            Result resultado = _filmeService.AtualizaFilme(id, filmeDto); //Utilizando o pacote FluenteResult
             
            if(resultado.IsFailed)
            {
                return NotFound();
            }
            return NoContent(); // BOA PRATICA QUANDO ATULIZAR DADOS, RETORNAR NoContent();
            
            /*  filme.Titulo  = filmeDto.Titulo;
                filme.Genero  = filmeDto.Genero;
                filme.Duracao = filmeDto.Duracao;   //PASSANDO OS DADOS ATUALIZADOS DO FILME - ONDE FOI ATUALIZADO PELO USO DO AutoMapper FEITO ACIMA
                filme.Diretor = filmeDto.Diretor;
            */
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaFilme(int id)
        {
            Result resultado = _filmeService.DeletaFilme(id);
            if (resultado.IsFailed)
            {
                return NotFound();
            }                   
            return NoContent();
        }
    }
}
