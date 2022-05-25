using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Services
{
    public class EnderecoService
    {
        AppDbContext _context;
        IMapper _mapper;

        public EnderecoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadEnderecoDto AdicionaEndereco(CreateEnderecoDto enderecoDto)
        {
            Endereco endereco = _mapper.Map<Endereco>(enderecoDto);

            _context.Enderecos.Add(endereco);
            _context.SaveChanges();

            return _mapper.Map<ReadEnderecoDto>(endereco);
        }

        public List<Endereco> RecuperaEnderecos()
        {
            return _mapper.Map<List<Endereco>>(_context.Enderecos);
        }

        public ReadEnderecoDto RecuperaEnderecoPorId(int id)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            if (endereco != null)
            {
                ReadEnderecoDto enderecoDto = _mapper.Map<ReadEnderecoDto>(endereco);

                return _mapper.Map<ReadEnderecoDto>(enderecoDto);
            }
            return null;
        }

        public Result AtualizaEndereco(int id, UpdateEnderecoDto enderecoDto)
        {

            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            if (endereco == null)
            {
                return Result.Fail("Endereço não encontrado");
            }
            _mapper.Map(enderecoDto, endereco);
            _context.SaveChanges();

            return Result.Ok();
        }

        public Result DeletaEndereco(int id)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(endereco => endereco.Id == id);
            if (endereco == null)
            {
                return Result.Fail("Endereco não encontrado");
            }
            _context.Remove(endereco);
            _context.SaveChanges();

            return Result.Ok();
        }
    }
}
