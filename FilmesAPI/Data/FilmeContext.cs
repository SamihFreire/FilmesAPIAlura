using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Data
{
    public class FilmeContext : DbContext
    {
        public FilmeContext(DbContextOptions<FilmeContext> opt) : base(opt) // PASSA O PARAMETRO opt PARA O CONSTRUTOR DA CLASSE BASE(DbContext)
        {
            
        }

        public DbSet<Filme> Filmes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)  // DEFININDO ONDE SE ENCONTRA E QUAL A STRING DE CONEXÃO
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("FilmeConnection"));

        }
    }
}
