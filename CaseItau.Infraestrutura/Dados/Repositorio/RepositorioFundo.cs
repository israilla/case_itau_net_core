using CaseItau.Dominio.Entidades;
using CaseItau.Dominio.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace CaseItau.Infraestrutura.Dados.Repositorio
{
    public class RepositorioFundo : IRepositorioFundo
    {
        private readonly Contexto _contexto;
        static readonly ThreadLocal<Random> aleatorio = new ThreadLocal<Random>(() => new Random((int)DateTime.Now.Ticks));

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
             .Where(f => f.Codigo == codigo.ToUpper()).FirstOrDefault();
        }
        public async Task IncluirFundo(Fundo fundo)
        {
            fundo.Codigo = GerarCodigoUnico();
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
        public string GerarValorAlteatorio(int tamanho = 15)
        {
            StringBuilder sb = new StringBuilder(tamanho);
            for (int i = 0; i < tamanho; i++)
            {
                sb.Append(aleatorio.Value.Next(0, 10));
            }

            return sb.ToString();
        }
        public string GerarCodigoUnico()
        {
            string prefixo = "ITAUF";
            string valor = GerarValorAlteatorio();

            return $"{prefixo}-{valor:D4}";
        }
    }
}
