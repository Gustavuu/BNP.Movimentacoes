using BNP.Movimentacoes.Dominio.Entidades;

namespace BNP.Movimentacoes.Aplicacao.Interfaces
{
    public interface IMovimentoManualRepository
    {
        Task<List<MovimentoManual>> ObterTodosMovimentosManuaisAsync();
        Task<long> ObterUltimoLancamentoNoMesAnoAsync(int mes, int ano);
        Task AdicionarMovimentoManualAsync(MovimentoManual movimento);
    }
}
