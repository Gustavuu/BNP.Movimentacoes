using BNP.Movimentacoes.Dominio.Entidades;

namespace BNP.Movimentacoes.Aplicacao.Interfaces
{
    public interface IProdutoRepository
    {
        Task<List<Produto>> ObterTodosProdutosAsync();
        Task<List<ProdutoCosif>> ObterCosifPorProdutoAsync(string codProduto);
    }
}
