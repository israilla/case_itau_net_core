using CaseItau.API.Configuracoes;
using CaseItau.Dominio.Interfaces.Repositorios;
using CaseItau.Dominio.Interfaces.Servicos;
using CaseItau.Dominio.Servicos;
using CaseItau.Infraestrutura.Dados.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CaseItau.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(Configuration["Security:Policy:Cors:Nome"], policy =>
                {
                    policy.WithOrigins(Configuration["Security:Policy:Cors:OrigemRequisicao"])
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            services.AddRepositorioBaseConfig(Configuration);
            services.AddInjecaoDependenciaConfig(Configuration);
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
            });;

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "API de Fundos",
                    Version = "v1"
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(Configuration["Security:Policy:Cors:Nome"]);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Fundos v1");
            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
