﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Data.Dtos.Endereco
{
    public class CreateEnderecoDto
    {
        public string Lorgadouro { get; set; }

        public string Bairro { get; set; }

        public int Numero { get; set; }
    }
}