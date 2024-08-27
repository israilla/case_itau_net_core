using CaseItau.Dominio.Entidades;
using CaseItau.Dominio.Interfaces.Repositorios;
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
           return _contexto.Fundo
            .Include(f => f.Tipo_Fundo)
            .ToList();
        }
        public async Task<Fundo>ObterFundoPorCodigo(string codigo)
        {
            return _contexto.Fundo
             .AsNoTracking()
             .Include(tp => tp.Tipo_Fundo)
             .Where(f => f.Codigo == codigo).FirstOrDefault();
        }
        public async Task IncluirFundo(Fundo fundo)
        {
            await _contexto.Fundo.AddAsync(fundo);
            await _contexto.SaveChangesAsync();
        }
        public async Task AlterarFundo(Fundo fundo)
        {
            _contexto.Fundo.Update(fundo);
            await _contexto.SaveChangesAsync();
        }
        public async Task ExcluirFundo(string codigo)
        {
            var fundo = await _contexto.Fundo.FindAsync(codigo);
            if (fundo != null)
            {
                _contexto.Fundo.Remove(fundo);
                await _contexto.SaveChangesAsync();
            }
        }
        public async Task AtualizarPatrimonio(string codigo, decimal valor)
        {
            var fundo = await _contexto.Fundo.FindAsync(codigo);
            if (fundo != null)
            {
                fundo.Patrimonio = (fundo.Patrimonio ?? 0) + valor;
                _contexto.Fundo.Update(fundo);
                await _contexto.SaveChangesAsync();
            }
        }
        public async Task<Fundo> ObterFundoPorCnpj(string cnpj)
        {
            return _contexto.Fundo
             .Include(tp => tp.Tipo_Fundo)
             .Where(f => f.Cnpj == cnpj).FirstOrDefault();
        }
    }
}
