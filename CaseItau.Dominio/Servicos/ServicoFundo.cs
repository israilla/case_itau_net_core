using CaseItau.Dominio.Dto;
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
        public IEnumerable<FundoDto> ObterTodos()
        {
             var fundos = _repositorioFundo.ObterTodos();
            
            return fundos.Select(f => new FundoDto
            {
                Codigo = f.Codigo,
                Nome = f.Nome,
                Patrimonio = f.Patrimonio,
                CodigoTipo = f.Codigo_Tipo,
                NomeTipo = f.Tipo_Fundo.Nome
            }).ToList();
        }
        public async Task<FundoDto> ObterFundoPorCodigo(string codigo)
        {
             var fundo = await _repositorioFundo.ObterFundoPorCodigo(codigo);

            if (fundo == null) return null;

            return new FundoDto
            {
                Codigo = fundo.Codigo,
                Nome = fundo.Nome,
                Patrimonio = fundo.Patrimonio,
                CodigoTipo = fundo.Codigo_Tipo,
                NomeTipo = fundo.Tipo_Fundo.Nome,
                Cnpj = fundo.Cnpj,
            };
        }
        public async Task IncluirFundo(FundoDto fundo)
        {
            var novoFundo = new Fundo
            {
                Codigo = fundo.Codigo,
                Nome = fundo.Nome,
                Patrimonio = fundo.Patrimonio,
                Codigo_Tipo = fundo.CodigoTipo,
                Cnpj = fundo.Cnpj
            };

            await _repositorioFundo.IncluirFundo(novoFundo);
        }
        public async Task AlterarFundo(FundoDto fundo)
        {
            var fundoAlterado = new Fundo
            {
                Codigo = fundo.Codigo,
                Nome = fundo.Nome,
                Patrimonio = fundo.Patrimonio,
                Codigo_Tipo = fundo.CodigoTipo,
                Cnpj = fundo.Cnpj
            };

            await _repositorioFundo.AlterarFundo(fundoAlterado);
        }
        public async Task ExcluirFundo(string codigo)
        {
            await _repositorioFundo.ExcluirFundo(codigo);
        }
        public async Task AtualizarPatrimonio(string codigo, decimal valor)
        {
            await _repositorioFundo.AtualizarPatrimonio(codigo, valor);
        }
        public async Task<Fundo> ObterFundoPorCnpj(string cnpj)
        {
            var fundo = await _repositorioFundo.ObterFundoPorCnpj(cnpj);

            return fundo;
        }
    }
}
