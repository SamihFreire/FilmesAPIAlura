using FilmesAPI.Data;
using FilmesAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {            
            services.AddDbContext<AppDbContext>(option => option.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("FilmeConnection"))); //CONFIGURANDO O CAMINHO DA STRING DE CONEX�O / UseLazyLoadingProxies() responsavel na consulta das informa��es que existentes na rela��o de uma tabela com outra/ � NECESSARIO DEFINIR O M�TODO COMO VIRTUAL PARA Q O LAZY REALLIZE O OVERRIDE
            
            
            services.AddScoped<FilmeService, FilmeService>(); //INSTANCIADO O FilmeService para o proprio FilmeService
            
            //INJE��O DE DEPENDENCIAS
            //NECESSARIO ADICIONAR O services.AddScoped<ControllerService, ControllerService>(), PARA IDENTIFICAR A COMUNICA��O ENTRE CLASSE CONTROLLER E SERVICES
            services.AddScoped<CinemaService, CinemaService>();
            services.AddScoped<EnderecoService, EnderecoService>();
            services.AddScoped<SessaoService, SessaoService>();
            
            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //UTILIZANDO O DOMINIO DA APLICA��O COM AutoMapper (Onde este realiza uma convers�o autom�tica de um tipo para o outro)(FilmeProfile.cs foi configurado a CreateMap());


            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
