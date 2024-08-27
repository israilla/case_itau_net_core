using CaseItau.Dominio.Interfaces.Repositorios;
using CaseItau.Dominio.Interfaces.Servicos;
using CaseItau.Dominio.Servicos;
using CaseItau.Infraestrutura.Dados.Repositorio;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CaseItau.API.Configuracoes
{
    public static class ConfiguracoesInjecaoDependencia
    {
        public static void AddInjecaoDependenciaConfig(this IServiceCollection servicos, IConfiguration configuracoes)
        {
            servicos.AddScoped<IRepositorioFundo, RepositorioFundo>();
            servicos.AddScoped<IServicoFundo, ServicoFundo>();
        }
    }
}
