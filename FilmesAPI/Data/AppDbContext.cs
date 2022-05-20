using FilmesAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) // PASSA O PARAMETRO opt PARA O CONSTRUTOR DA CLASSE BASE(DbContext)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder) //EXPLICITANDO NO MOMENTO DE CRIAÇÃO DO MODELO VAI SER FEITAS ALGUMAS DEFINIÇÕES
        {
            builder.Entity<Endereco>()              //CONSTRUINDO A ENTIDADE ENDERECO 
                .HasOne(endereco => endereco.Cinema)//DEFININDO QUE ENDENREÇO REFENCIA CINEMA
                .WithOne(cinema => cinema.Endereco)//DEFININDO QUE CINEMA REFERNCIA ENDEREÇO
                .HasForeignKey<Cinema>(cinema => cinema.EnderecoId);//DEFININDO QUE A CHAVE ESTRANGEIRA SE ECONTRA EM CINEMA, ONDE ESTA FK É EnderecoId

            builder.Entity<Cinema>()
                .HasOne(cinema => cinema.Gerente)
                .WithMany(gerente => gerente.Cinemas) //ESTABELECENDO REALÇÃO 1 PARA MUITOS 1:N
                .HasForeignKey(cinema => cinema.GerenteId);

            builder.Entity<Sesssao>()
                .HasOne(sessao => sessao.Filme)
                .WithMany(Filme => Filme.Sessoes)
                .HasForeignKey(sessao => sessao.FilmeId);
                                                                //CRIANDO RELAÇÃO N:N ENTRE FILME E CINEMA POR MEIO DA TABELA SESSAO
            builder.Entity<Sesssao>()
                .HasOne(sessao => sessao.Cinema)
                .WithMany(cinema => cinema.Sessoes)
                .HasForeignKey(sessao => sessao.CinemaId);
        }


        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Gerente> Gerente { get; set; }
        public DbSet<Sesssao> Sessao { get; set; }

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
