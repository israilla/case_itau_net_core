using CaseItau.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseItau.Dominio.Interfaces.Servicos
{
    public interface IServicoFundo
    {
        IEnumerable<Fundo> ObterTodos();
        Task<Fundo> ObterFundoPorCodigo(string codigo);
        Task IncluirFundo(Fundo fundo);
        Task AlterarFundo(Fundo fundo);
        Task ExcluirFundo(string codigo);
        Task AtualizarPatrimonio(string codigo, decimal valor);
    }
}
