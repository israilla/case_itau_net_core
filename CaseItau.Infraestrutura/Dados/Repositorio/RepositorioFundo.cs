using CaseItau.Dominio.Entidades;
using CaseItau.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CaseItau.Infraestrutura.Dados.Repositorio
{
    public class RepositorioFundo : IRepositorioFundo
    {
        private readonly Contexto _contexto;

        public RepositorioFundo(Contexto contexto)
        {
            _contexto = contexto;
        }

        public IEnumerable<Fundo> ObterTodos()
        {
            return _contexto.Fundos
            .Include(f => f.TipoFundo)
            .ToList();
        }
    }
}
