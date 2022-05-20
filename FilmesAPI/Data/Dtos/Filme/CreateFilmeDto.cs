using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Data.Dtos
{
    public class CreateFilmeDto
    {

        [Required(ErrorMessage = "O campo título é obrigatório")] //Obriga o json recebido tenha o campo Titulo preenchido
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O campo diretor é obrigatorio")]
        public string Diretor { get; set; }

        [StringLength(30, ErrorMessage = "O gênero nao pode passar de 30 caracteres")]
        public string Genero { get; set; }

        [Range(1, 600, ErrorMessage = "A duração deve ter no minimo 1min e no máximo 600min")] // Range de duração do filme
        public int Duracao { get; set; }
        public int ClassificacaoEtaria { get; set; }
    }
}
