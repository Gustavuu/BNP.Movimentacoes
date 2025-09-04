namespace BNP.Movimentacoes.Dominio.Entidades
{
    public class MovimentoManual
    {
        public required int DatMes { get; set; }
        public required int DatAno { get; set; }
        public required long NumLancamento { get; set; }
        public required string CodProduto { get; set; }
        public required string CodCosif { get; set; }
        public required string DesDescricao { get; set; }
        public required DateTime DatMovimento { get; set; }
        public required string CodUsuario { get; set; }
        public required decimal ValValor { get; set; }
        public virtual Produto? Produto { get; set; }
        public virtual ProdutoCosif? ProdutoCosif { get; set; }
    }
}