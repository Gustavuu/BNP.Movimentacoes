using BNP.Movimentacoes.Aplicacao.Interfaces;
using BNP.Movimentacoes.Dominio.Entidades;
using BNP.Movimentacoes.Infraestrutura.Contexto;
using Microsoft.EntityFrameworkCore;

namespace BNP.Movimentacoes.Infraestrutura.Repositorios
{
    public class MovimentoManualRepository : IMovimentoManualRepository
    {
        private readonly MovimentacoesDbContext _contexto;

        public MovimentoManualRepository(MovimentacoesDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<MovimentoManual>> ObterTodosMovimentosManuaisAsync()
        {
            return await _contexto.MovimentosManuais
                .Include(m => m.Produto)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<long> ObterUltimoLancamentoNoMesAnoAsync(int mes, int ano)
        {
            // Busca o maior NUM_LANCAMENTO para o mês e ano especificados.
            // Se não houver nenhum lançamento (DefaultIfEmpty), retorna 0.
            var ultimoLancamento = await _contexto.MovimentosManuais
                .Where(m => m.DatMes == mes && m.DatAno == ano)
                .MaxAsync(m => (long?)m.NumLancamento) ?? 0;

            return ultimoLancamento;
        }

        public async Task AdicionarMovimentoManualAsync(MovimentoManual movimento)
        {
            await _contexto.MovimentosManuais.AddAsync(movimento);
            await _contexto.SaveChangesAsync();
        }
    }
}
