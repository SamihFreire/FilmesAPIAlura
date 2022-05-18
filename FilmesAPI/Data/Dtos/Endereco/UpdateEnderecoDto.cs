using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Data.Dtos
{
    public class UpdateEnderecoDto
    {
        public string Lorgadouro { get; set; }

        public string Bairro { get; set; }

        public int Numero { get; set; }
    }
}
