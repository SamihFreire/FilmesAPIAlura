﻿using System;
using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models
{
    public class Sesssao
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public virtual Cinema Cinema { get; set; }
        public virtual Filme Filme { get; set; }
        public int FilmeId { get; set; }
        public int CinemaId{ get; set; }
        public DateTime HoraDeEncerramento{ get; set; }
    }
}
