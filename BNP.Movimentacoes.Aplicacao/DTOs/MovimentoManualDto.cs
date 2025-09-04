namespace BNP.Movimentacoes.Aplicacao.DTOs
{
    public class MovimentoManualDto
    {
        public int Mes { get; set; }
        public int Ano { get; set; }
        public string CodProduto { get; set; }
        public string? NomeProduto { get; set; }
        public long NumLancamento { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
    }
}
