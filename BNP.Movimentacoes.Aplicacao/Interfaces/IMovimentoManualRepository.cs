using BNP.Movimentacoes.Dominio.Entidades;

namespace BNP.Movimentacoes.Aplicacao.Interfaces
{
    public interface IMovimentoManualRepository
    {
        Task<List<MovimentoManual>> ObterTodosMovimentosManuaisAsync();
        Task<decimal> ObterUltimoLancamentoNoMesAnoAsync(decimal mes, decimal ano);
        Task AdicionarMovimentoManualAsync(MovimentoManual movimento);
    }
}
