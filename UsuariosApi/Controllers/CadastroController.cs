using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CadastroController : ControllerBase
    {
        private CadastroService _CadastroService;

        public CadastroController(CadastroService cadastroService)
        {
            _CadastroService = cadastroService;
        }

        [HttpPost]
        public IActionResult CadastrarUsuario(CreateUsuarioDto createDto)
        {
            Result resultado = _CadastroService.CadastrarUsuario(createDto);
            if (resultado.IsFailed)
                return StatusCode(500);

            return Ok();
            
        }
    }
}
