using BNP.Movimentacoes.Aplicacao.DTOs;
using BNP.Movimentacoes.Dominio.Entidades;

namespace BNP.Movimentacoes.Aplicacao.Interfaces
{
    public interface IMovimentoManualService
    {
        Task<List<MovimentoManualDto>> ObterTodosMovimentosManuaisAsync();
        Task<List<Produto>> ObterTodosProdutosAsync();
        Task<List<ProdutoCosif>> ObterCosifPorProdutoAsync(string codProduto);
        Task CriarMovimentoManualAsync(MovimentoManual novoMovimento);
    }
}
