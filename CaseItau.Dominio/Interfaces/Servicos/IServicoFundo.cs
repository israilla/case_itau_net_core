using CaseItau.Dominio.Dto;
using CaseItau.Dominio.Entidades;

namespace CaseItau.Dominio.Interfaces.Servicos
{
    public interface IServicoFundo
    {
        IEnumerable<FundoDto> ObterTodos();
        Task<FundoDto> ObterFundoPorCodigo(string codigo);
        Task IncluirFundo(FundoDto fundo);
        Task AlterarFundo(FundoDto fundo);
        Task ExcluirFundo(string codigo);
        Task AtualizarPatrimonio(string codigo, decimal valor);
        Task<Fundo> ObterFundoPorCnpj(string cnpj);
    }
}
