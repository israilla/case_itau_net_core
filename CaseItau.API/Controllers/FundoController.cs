using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CaseItau.Dominio.Entidades;
using CaseItau.Dominio.Interfaces.Servicos;

namespace CaseItau.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FundoController : ControllerBase
    {
        private readonly IServicoFundo _servicoFundo;

        public FundoController(IServicoFundo servicoFundo)
        {
            _servicoFundo = servicoFundo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fundo>>> ObterFundos()
        {
            var fundos = _servicoFundo.ObterTodos();
            return Ok(fundos);
        }

        [HttpGet("{codigo}", Name = "ObterFundosPorCodigo")]
        public async Task<ActionResult<IEnumerable<Fundo>>> ObterFundosPorCodigo(string codigo)
        {
            var fundo = await _servicoFundo.ObterFundoPorCodigo(codigo);

            if (fundo != null)
                return Ok(fundo);
            else
                throw new Exception("Fundo não encontrado");
        }

        [HttpPost]
        public async Task<IActionResult> IncluirFundo([FromBody] Fundo fundo)
        {
            if (fundo == null)
                return BadRequest("Por favor, informar os dados para adicionar um novo fundo.");

            await _servicoFundo.IncluirFundo(fundo);

            return Ok("Fundo adicionado com sucesso!");
        }

        [HttpPut("{codigo}")]
        public async Task<IActionResult> AlterarFundo(string codigo, [FromBody] Fundo fundoPesquisa)
        {
            if (fundoPesquisa == null || codigo != fundoPesquisa.Codigo)
            {
                return BadRequest("Os dados do Fundo estão inválidos.");
            }

            var fundo = await _servicoFundo.ObterFundoPorCodigo(codigo);

            if (fundo == null)
                return NotFound("Fundo não encontrado.");

            fundo.Nome = fundoPesquisa.Nome;
            fundo.Cnpj = fundoPesquisa.Cnpj;
            fundo.Codigo_Tipo = fundoPesquisa.Codigo_Tipo;

            await _servicoFundo.AlterarFundo(fundo);

            return Ok("Fundo alterado com sucesso!.");
        }

        [HttpDelete("{codigo}")]
        public async Task<IActionResult> ExcluirFundo(string codigo)
        {
            var fundo =  await _servicoFundo.ObterFundoPorCodigo(codigo);
            if (fundo == null)
                return NotFound("Fundo não encontrado.");

            await _servicoFundo.ExcluirFundo(codigo);

            return Ok("Fundo excluído com sucesso.");
        }

        [HttpPut("{codigo}/patrimonio")]
        public async Task<IActionResult> MovimentarPatrimonio(string codigo, [FromBody] decimal valor)
        {
            if (valor < 0)
                return BadRequest("O valor não pode ser negativo.");

            var fundo = await _servicoFundo.ObterFundoPorCodigo(codigo);

            if (fundo == null)
                return NotFound("Fundo não encontrado.");

            await _servicoFundo.AtualizarPatrimonio(codigo, valor);

            return Ok("Patrimônio atualizado com sucesso.");
        }
    }
}
