namespace BNP.Movimentacoes.Aplicacao.DTOs
{
    public class MovimentoManualDto
    {
        public decimal Mes { get; set; }
        public decimal Ano { get; set; }
        public string CodProduto { get; set; }
        public string? NomeProduto { get; set; }
        public decimal NumLancamento { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
    }
}
