using BNP.Movimentacoes.Aplicacao.Interfaces;
using BNP.Movimentacoes.Dominio.Entidades;
using BNP.Movimentacoes.Infraestrutura.Contexto;
using Microsoft.EntityFrameworkCore;

namespace BNP.Movimentacoes.Infraestrutura.Repositorios
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly MovimentacoesDbContext _contexto;

        public ProdutoRepository(MovimentacoesDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Produto>> ObterTodosProdutosAsync()
        {
            // Usamos AsNoTracking() como uma otimização de performance,
            // pois esta é uma consulta de apenas leitura.
            return await _contexto.Produtos
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<ProdutoCosif>> ObterCosifPorProdutoAsync(string codProduto)
        {
            return await _contexto.ProdutosCosif
                .Include(pc => pc.Produto)
                .AsNoTracking()
                .Where(c => c.CodProduto == codProduto)
                .ToListAsync();
        }
    }
}
