using CaseItau.Dominio.Entidades;

namespace CaseItau.Dominio.Interfaces
{
    public interface IRepositorioFundo
    {
        IEnumerable<Fundo> ObterTodos();
    }
}
