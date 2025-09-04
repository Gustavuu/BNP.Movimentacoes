using BNP.Movimentacoes.Aplicacao.DTOs;
using BNP.Movimentacoes.Aplicacao.Interfaces;
using BNP.Movimentacoes.Aplicacao.Services;
using BNP.Movimentacoes.Dominio.Entidades;
using Moq;
using Shouldly;

namespace BNP.Movimentacoes.Testes.Aplicacao.Services
{
    public class MovimentoManualServiceTests
    {
        private readonly Mock<IProdutoRepository> _mockProdutoRepo;
        private readonly Mock<IMovimentoManualRepository> _mockMovimentoRepo;
        private readonly MovimentoManualService _servico;

        public MovimentoManualServiceTests()
        {
            // Mocks para os repositórios.
            _mockProdutoRepo = new Mock<IProdutoRepository>();
            _mockMovimentoRepo = new Mock<IMovimentoManualRepository>();

            // Injetando os mocks.
            _servico = new MovimentoManualService(_mockProdutoRepo.Object, _mockMovimentoRepo.Object);
        }

        [Fact]
        public async Task CriarMovimentoManualAsync_Deve_CalcularProximoLancamentoCorretamente()
        {
            // Arrange
            var mes = 10;
            var ano = 2025;
            var ultimoLancamentoExistente = 4L; // L para indicar que é um long

            var dto = new CriarMovimentoDto
            {
                DatMes = mes,
                DatAno = ano,
                CodProduto = "PROD1",
                CodCosif = "COS1",
                ValValor = 150.75m,
                DesDescricao = "Teste"
            };

            // Obter o último lançamento, deve retornar o valor '4'.
            _mockMovimentoRepo
                .Setup(r => r.ObterUltimoLancamentoNoMesAnoAsync(mes, ano))
                .ReturnsAsync(ultimoLancamentoExistente);

            // Act
            await _servico.CriarMovimentoManualAsync(dto);

            // Assert
            // Verifica se o método Adicionar foi chamado no repositório.
            // Verifica se o objeto 'MovimentoManual' teve propriedade NumLancamento calculada corretamente como 5 (4 + 1).
            _mockMovimentoRepo.Verify(r => r.AdicionarMovimentoManualAsync(
                It.Is<MovimentoManual>(m => m.NumLancamento == ultimoLancamentoExistente + 1)
            ), Times.Once);
        }

        [Fact]
        public async Task ObterTodosMovimentosManuaisAsync_Deve_MapearDadosParaDtoCorretamente()
        {
            // Arrange
            var listaDeMovimentos = new List<MovimentoManual>
            {
                new MovimentoManual
                {
                    DatMes = 9,
                    DatAno = 2025,
                    NumLancamento = 1,
                    CodProduto = "PROD1",
                    CodCosif = "COS1",
                    DesDescricao = "Desc Teste 1",
                    ValValor = 100,
                    DatMovimento = DateTime.Now,
                    CodUsuario = "TESTE",
                    Produto = new Produto { CodProduto = "PROD1", DesProduto = "Produto Teste 1" }
                }
            };

            // Retorna lista Mock
            _mockMovimentoRepo
                .Setup(r => r.ObterTodosMovimentosManuaisAsync())
                .ReturnsAsync(listaDeMovimentos);

            // Act
            var resultadoDto = await _servico.ObterTodosMovimentosManuaisAsync();

            // Assert
            resultadoDto.ShouldNotBeNull();
            resultadoDto.Count.ShouldBe(1);
            resultadoDto[0].NomeProduto.ShouldBe("Produto Teste 1"); // Verifica se o mapeamento do nome do produto funcionou.
            resultadoDto[0].Valor.ShouldBe(100);
        }
    }
}