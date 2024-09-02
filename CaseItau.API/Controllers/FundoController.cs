using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CaseItau.Dominio.Interfaces.Servicos;
using CaseItau.Dominio.Dto;
using System.Linq;

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
        public async Task<ActionResult<List<FundoDto>>> ObterFundos()
        {
            var fundos = _servicoFundo.ObterTodos().ToList();
            return Ok(fundos);
        }

        [HttpGet("{codigo}", Name = "ObterFundosPorCodigo")]
        public async Task<ActionResult<IEnumerable<FundoDto>>> ObterFundosPorCodigo(string codigo)
        {
            var fundo = await _servicoFundo.ObterFundoPorCodigo(codigo);

            if (fundo != null)
                return Ok(fundo);
            else
                return BadRequest("Fundo não encontrado");
        }

        [HttpPost]
        public async Task<IActionResult> IncluirFundo([FromBody] FundoDto fundo)
        {
            if (fundo == null)
                return BadRequest("Por favor, informar os dados para adicionar um novo fundo.");

            var fundoExiste =  await _servicoFundo.ObterFundoPorCnpj(fundo.Cnpj);

            if (fundoExiste != null) 
                return Conflict("O CNPJ informado já possui um fundo registrado.");

            await _servicoFundo.IncluirFundo(fundo);

            return Ok("Fundo adicionado com sucesso!");
        }

        [HttpPut]
        public async Task<IActionResult> AlterarFundo([FromBody] FundoDto fundoPesquisa)
        {
            if (fundoPesquisa == null)
            {
                return BadRequest("Os dados do Fundo estão inválidos.");
            }

            var fundo = await _servicoFundo.ObterFundoPorCodigo(fundoPesquisa.Codigo);

            if (fundo == null)
                return NotFound();

            await _servicoFundo.AlterarFundo(fundoPesquisa);

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
        public async Task<IActionResult> MovimentarPatrimonio(string codigo, [FromQuery] decimal valor)
        {
            var fundo = await _servicoFundo.ObterFundoPorCodigo(codigo);

            if (fundo == null)
                return NotFound("Fundo não encontrado.");

            await _servicoFundo.AtualizarPatrimonio(codigo, valor);

            return Ok("Patrimônio atualizado com sucesso.");
        }
    }
}
