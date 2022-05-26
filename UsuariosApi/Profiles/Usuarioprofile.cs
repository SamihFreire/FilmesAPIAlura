using AutoMapper;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Models;

namespace UsuariosApi.Profiles
{
    public class Usuarioprofile : Profile
    {
        public Usuarioprofile()
        {
            CreateMap<CreateUsuarioDto, Usuario>();
        }
    }
}
