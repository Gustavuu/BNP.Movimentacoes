namespace BNP.Movimentacoes.Dominio.Entidades
{
    public class ProdutoCosif
    {
        public required string CodCosif { get; set; }
        public required string CodProduto { get; set; }
        public string? CodClassificacao { get; set; }
        public string? StaStatus { get; set; }
        public virtual Produto? Produto { get; set; }
    }
}