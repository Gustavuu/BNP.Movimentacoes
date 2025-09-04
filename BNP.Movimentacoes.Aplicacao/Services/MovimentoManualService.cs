using BNP.Movimentacoes.Aplicacao.DTOs;
using BNP.Movimentacoes.Aplicacao.Interfaces;
using BNP.Movimentacoes.Dominio.Entidades;

namespace BNP.Movimentacoes.Aplicacao.Services
{
    public class MovimentoManualService : IMovimentoManualService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMovimentoManualRepository _movimentoManualRepository;

        public MovimentoManualService(IProdutoRepository produtoRepository, IMovimentoManualRepository movimentoManualRepository)
        {
            _produtoRepository = produtoRepository;
            _movimentoManualRepository = movimentoManualRepository;
        }

        public async Task<List<MovimentoManualDto>> ObterTodosMovimentosManuaisAsync()
        {
            // A implementação virá depois, na camada de Infraestrutura
            throw new NotImplementedException();
        }

        public async Task<List<Produto>> ObterTodosProdutosAsync()
        {
            return await _produtoRepository.ObterTodosProdutosAsync();
        }

        public async Task<List<ProdutoCosif>> ObterCosifPorProdutoAsync(string codProduto)
        {
            return await _produtoRepository.ObterCosifPorProdutoAsync(codProduto);
        }

        public async Task CriarMovimentoManualAsync(MovimentoManual novoMovimento)
        {
            // Lógica para gerar o próximo número de lançamento
            var ultimoLancamento = await _movimentoManualRepository.ObterUltimoLancamentoNoMesAnoAsync(novoMovimento.DatMes, novoMovimento.DatAno);
            novoMovimento.NumLancamento = ultimoLancamento + 1;

            // Preencher dados fixos conforme o requisito
            novoMovimento.DatMovimento = DateTime.Now;
            novoMovimento.CodUsuario = "TESTE";

            await _movimentoManualRepository.AdicionarMovimentoManualAsync(novoMovimento);
        }
    }
}
