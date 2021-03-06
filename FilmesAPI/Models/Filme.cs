using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FilmesAPI.Models
{
    public class Filme
    {
        [Key]
        [Required] //DEFINE COMO UM CAMPO OBRIGATÓRIO 
        public int Id { get; set; }

        [Required(ErrorMessage ="O campo título é obrigatório")] //Obriga o json recebido tenha o campo Titulo preenchido
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O campo diretor é obrigatorio")]
        public string Diretor { get; set; }
        
        [StringLength(30, ErrorMessage = "O gênero nao pode passar de 30 caracteres")]
        public string Genero { get; set; }

        [Range(1,600, ErrorMessage = "A duração deve ter no minimo 1min e no máximo 600min")] // Range de duração do filme
        public int Duracao { get; set; }
        public int ClassificacaoEtaria { get; set; }
        [JsonIgnore]
        public virtual List<Sessao> Sessoes{ get; set; }
    }
}
