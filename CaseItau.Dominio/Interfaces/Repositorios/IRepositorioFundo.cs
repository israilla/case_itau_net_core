using CaseItau.Dominio.Entidades;

namespace CaseItau.Dominio.Interfaces.Repositorios
{
    public interface IRepositorioFundo
    {
        IEnumerable<Fundo> ObterTodos();
        Task<Fundo> ObterFundoPorCodigo(string codigo);
        Task IncluirFundo(Fundo fundo);
        Task AlterarFundo(Fundo fundo);
        Task ExcluirFundo(string codigo);
        Task AtualizarPatrimonio(string codigo, decimal valor);
    }
}
