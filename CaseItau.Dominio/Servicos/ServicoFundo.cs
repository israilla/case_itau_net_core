using CaseItau.Dominio.Entidades;
using CaseItau.Dominio.Interfaces;

namespace CaseItau.Dominio.Servicos
{
    public class ServicoFundo
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
    }
}
