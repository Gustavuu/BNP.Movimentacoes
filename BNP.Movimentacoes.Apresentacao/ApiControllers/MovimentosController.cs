using BNP.Movimentacoes.Aplicacao.DTOs;
using BNP.Movimentacoes.Aplicacao.Interfaces;
using BNP.Movimentacoes.Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace BNP.Movimentacoes.Apresentacao.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimentosController : ControllerBase
    {
        private readonly IMovimentoManualService _servico;

        public MovimentosController(IMovimentoManualService servico)
        {
            _servico = servico;
        }

        [HttpGet]
        public async Task<IActionResult> ObterMovimentos()
        {
            var movimentos = await _servico.ObterTodosMovimentosManuaisAsync();
            return Ok(movimentos);
        }

        [HttpGet("produtos")]
        public async Task<IActionResult> ObterProdutos()
        {
            var produtos = await _servico.ObterTodosProdutosAsync();
            return Ok(produtos);
        }

        [HttpGet("produtos/{codProduto}/cosif")]
        public async Task<IActionResult> ObterCosif(string codProduto)
        {
            var cosifs = await _servico.ObterCosifPorProdutoAsync(codProduto);
            return Ok(cosifs);
        }

        [HttpPost]
        public async Task<IActionResult> CriarMovimento([FromBody] CriarMovimentoDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var movimentoCriado = await _servico.CriarMovimentoManualAsync(dto);

            // Retorna o objeto completo que foi criado no banco
            return CreatedAtAction(nameof(ObterMovimentos), new { }, movimentoCriado);
        }
    }
}
