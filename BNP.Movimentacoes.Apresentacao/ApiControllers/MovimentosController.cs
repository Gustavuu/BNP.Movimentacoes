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

        /// <summary>
        /// Busca a lista completa de todos os movimentos manuais cadastrados.
        /// </summary>
        /// <returns>Uma lista de movimentos manuais.</returns>
        /// <response code="200">Retorna a lista de movimentos.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<MovimentoManualDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterMovimentos()
        {
            var movimentos = await _servico.ObterTodosMovimentosManuaisAsync();
            return Ok(movimentos);
        }

        /// <summary>
        /// Busca a lista de todos os produtos para preenchimento do combo na tela.
        /// </summary>
        /// <returns>Uma lista de produtos.</returns>
        /// <response code="200">Retorna a lista de produtos.</response>
        [HttpGet("produtos")]
        [ProducesResponseType(typeof(List<Produto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterProdutos()
        {
            var produtos = await _servico.ObterTodosProdutosAsync();
            return Ok(produtos);
        }

        /// <summary>
        /// Busca a lista de COSIFs associados a um produto específico.
        /// </summary>
        /// <param name="codProduto">O código do produto para o qual se deseja buscar os COSIFs.</param>
        /// <returns>Uma lista de produtos COSIF.</returns>
        /// <response code="200">Retorna a lista de COSIFs.</response>
        [HttpGet("produtos/{codProduto}/cosif")]
        [ProducesResponseType(typeof(List<ProdutoCosif>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterCosif(string codProduto)
        {
            var cosifs = await _servico.ObterCosifPorProdutoAsync(codProduto);
            return Ok(cosifs);
        }

        /// <summary>
        /// Cria um novo movimento manual no sistema.
        /// </summary>
        /// <param name="dto">Os dados do movimento manual a ser criado, fornecidos pelo usuário.</param>
        /// <returns>O movimento manual recém-criado com os dados gerados pelo servidor.</returns>
        /// <response code="201">Retorna o movimento recém-criado.</response>
        /// <response code="400">Se os dados fornecidos forem inválidos.</response>
        [HttpPost]
        [ProducesResponseType(typeof(MovimentoManual), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
