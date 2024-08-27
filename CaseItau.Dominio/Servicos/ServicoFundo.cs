using CaseItau.Dominio.Entidades;
using CaseItau.Dominio.Interfaces.Repositorios;
using CaseItau.Dominio.Interfaces.Servicos;

namespace CaseItau.Dominio.Servicos
{
    public class ServicoFundo : IServicoFundo
    {
        private readonly IRepositorioFundo _repositorioFundo;

        public ServicoFundo(IRepositorioFundo repositorioFundo)
        {
                _repositorioFundo = repositorioFundo;   
        }
        public IEnumerable<Fundo> ObterTodos()
        {
            return _repositorioFundo.ObterTodos();
        }
        public Task<Fundo> ObterFundoPorCodigo(string codigo)
        {
            return _repositorioFundo.ObterFundoPorCodigo(codigo);
        }
        public async Task IncluirFundo(Fundo fundo)
        {
            await _repositorioFundo.IncluirFundo(fundo);
        }
        public async Task AlterarFundo(Fundo fundo)
        {
            await _repositorioFundo.AlterarFundo(fundo);
        }
        public async Task ExcluirFundo(string codigo)
        {
            await _repositorioFundo.ExcluirFundo(codigo);
        }
        public async Task AtualizarPatrimonio(string codigo, decimal valor)
        {
            await _repositorioFundo.AtualizarPatrimonio(codigo, valor);
        }

    }
}
