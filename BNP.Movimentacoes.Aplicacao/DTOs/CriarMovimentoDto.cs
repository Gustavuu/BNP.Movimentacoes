using System.ComponentModel.DataAnnotations;

namespace BNP.Movimentacoes.Aplicacao.DTOs
{
    public class CriarMovimentoDto
    {
        [Required]
        [Range(1, 12, ErrorMessage = "O Mês deve ser um valor entre 1 e 12.")]
        public int DatMes { get; set; }
        [Required]
        [Range(1900, 2025, ErrorMessage = "O Ano deve ser um valor entre 1900 e 2025.")]
        public int DatAno { get; set; }
        [Required]
        public string CodProduto { get; set; }
        [Required]
        public string CodCosif { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "O Valor deve ser maior que zero.")]
        public decimal ValValor { get; set; }
        [Required]
        public string DesDescricao { get; set; }
    }
}
