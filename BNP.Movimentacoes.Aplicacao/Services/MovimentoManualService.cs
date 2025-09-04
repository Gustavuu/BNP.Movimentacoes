using BNP.Movimentacoes.Aplicacao.DTOs;
using BNP.Movimentacoes.Aplicacao.Interfaces;
using BNP.Movimentacoes.Dominio.Entidades;

namespace BNP.Movimentacoes.Aplicacao.Services
{
    public class MovimentoManualService : IMovimentoManualService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMovimentoManualRepository _movimentoManualRepository;

        public MovimentoManualService(IProdutoRepository produtoRepository, IMovimentoManualRepository movimentoManualRepository)
        {
            _produtoRepository = produtoRepository;
            _movimentoManualRepository = movimentoManualRepository;
        }

        public async Task<List<MovimentoManualDto>> ObterTodosMovimentosManuaisAsync()
        {
            var movimentos = await _movimentoManualRepository.ObterTodosMovimentosManuaisAsync();

            // Mapeia a lista de Entidades para uma lista de DTOs
            var movimentosDto = movimentos.Select(m => new MovimentoManualDto
            {
                Mes = m.DatMes,
                Ano = m.DatAno,
                CodProduto = m.CodProduto,
                NomeProduto = m.Produto?.DesProduto,
                NumLancamento = m.NumLancamento,
                Descricao = m.DesDescricao,
                Valor = m.ValValor
            }).ToList();

            return movimentosDto;
        }

        public async Task<List<Produto>> ObterTodosProdutosAsync()
        {
            return await _produtoRepository.ObterTodosProdutosAsync();
        }

        public async Task<List<ProdutoCosif>> ObterCosifPorProdutoAsync(string codProduto)
        {
            return await _produtoRepository.ObterCosifPorProdutoAsync(codProduto);
        }

        public async Task<MovimentoManual> CriarMovimentoManualAsync(CriarMovimentoDto dto)
        {
            // 1. Mapeia o DTO para a Entidade de Domínio
            var novoMovimento = new MovimentoManual
            {
                DatMes = dto.DatMes,
                DatAno = dto.DatAno,
                CodProduto = dto.CodProduto,
                CodCosif = dto.CodCosif,
                ValValor = dto.ValValor,
                DesDescricao = dto.DesDescricao,
                // Valores gerados pelo servidor:
                DatMovimento = DateTime.Now,
                CodUsuario = "TESTE",
                NumLancamento = 0 // Placeholder, será calculado a seguir
            };

            // 2. Lógica de negócio para gerar o próximo número de lançamento
            var ultimoLancamento = await _movimentoManualRepository.ObterUltimoLancamentoNoMesAnoAsync(novoMovimento.DatMes, novoMovimento.DatAno);
            novoMovimento.NumLancamento = ultimoLancamento + 1;

            // 3. Adiciona o movimento completo ao repositório
            await _movimentoManualRepository.AdicionarMovimentoManualAsync(novoMovimento);

            // Retorna a entidade completa criada
            return novoMovimento;
        }
    }
}
