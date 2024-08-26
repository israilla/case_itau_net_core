using CaseItau.Infraestrutura.Dados;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CaseItau.API.Configuracoes
{
    public static class ConfiguracoesRepositorio
    {
        public static void AddRepositorioBaseConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Contexto>(options =>
                options.UseSqlite(configuration.GetConnectionString("ConexaoCaseItau")));
        }
    }
}
