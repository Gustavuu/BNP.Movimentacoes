using BNP.Movimentacoes.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace BNP.Movimentacoes.Infraestrutura.Contexto
{
    public class MovimentacoesDbContext : DbContext
    {
        public MovimentacoesDbContext(DbContextOptions<MovimentacoesDbContext> options) : base(options)
        {
        }

        // DbSets para cada entidade
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<ProdutoCosif> ProdutosCosif { get; set; }
        public DbSet<MovimentoManual> MovimentosManuais { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da entidade PRODUTO
            modelBuilder.Entity<Produto>(entity =>
            {
                entity.ToTable("PRODUTO"); // Nome da tabela
                entity.HasKey(p => p.CodProduto); // Chave primária
                entity.Property(p => p.CodProduto).HasColumnName("COD_PRODUTO").HasColumnType("char(4)").IsRequired();
                entity.Property(p => p.DesProduto).HasColumnName("DES_PRODUTO").HasColumnType("varchar(30)");
                entity.Property(p => p.StaStatus).HasColumnName("STA_STATUS").HasColumnType("char(1)");
            });

            // Configuração da entidade PRODUTO_COSIF
            modelBuilder.Entity<ProdutoCosif>(entity =>
            {
                entity.ToTable("PRODUTO_COSIF");
                entity.HasKey(pc => new { pc.CodProduto, pc.CodCosif }); // Chave primária composta
                entity.Property(pc => pc.CodProduto).HasColumnName("COD_PRODUTO").HasColumnType("char(4)").IsRequired();
                entity.Property(pc => pc.CodCosif).HasColumnName("COD_COSIF").HasColumnType("char(11)").IsRequired();
                entity.Property(pc => pc.CodClassificacao).HasColumnName("COD_CLASSIFICACAO").HasColumnType("char(6)");
                entity.Property(pc => pc.StaStatus).HasColumnName("STA_STATUS").HasColumnType("char(1)");

                // Configuração da chave estrangeira para PRODUTO
                entity.HasOne(pc => pc.Produto)
                      .WithMany() // Um produto pode ter muitos Cosifs
                      .HasForeignKey(pc => pc.CodProduto);
            });

            // Configuração da entidade MOVIMENTO_MANUAL
            modelBuilder.Entity<MovimentoManual>(entity =>
            {
                entity.ToTable("MOVIMENTO_MANUAL");
                entity.HasKey(mm => new { mm.DatMes, mm.DatAno, mm.NumLancamento });
                entity.Property(mm => mm.DatMes).HasColumnName("DAT_MES").HasColumnType("numeric(2, 0)").IsRequired();
                entity.Property(mm => mm.DatAno).HasColumnName("DAT_ANO").HasColumnType("numeric(4, 0)").IsRequired();
                entity.Property(mm => mm.NumLancamento).HasColumnName("NUM_LANCAMENTO").HasColumnType("numeric(18, 0)").IsRequired();
                entity.Property(mm => mm.CodProduto).HasColumnName("COD_PRODUTO").HasColumnType("char(4)").IsRequired();
                entity.Property(mm => mm.CodCosif).HasColumnName("COD_COSIF").HasColumnType("char(11)").IsRequired();
                entity.Property(mm => mm.DesDescricao).HasColumnName("DES_DESCRICAO").HasColumnType("varchar(50)").IsRequired();
                entity.Property(mm => mm.DatMovimento).HasColumnName("DAT_MOVIMENTO").HasColumnType("datetime").IsRequired();
                entity.Property(mm => mm.CodUsuario).HasColumnName("COD_USUARIO").HasColumnType("varchar(15)").IsRequired();
                entity.Property(mm => mm.ValValor).HasColumnName("VAL_VALOR").HasColumnType("numeric(18, 2)").IsRequired();

                // Configuração das chaves estrangeiras com a regra de deleção explícita
                entity.HasOne(mm => mm.Produto)
                      .WithMany()
                      .HasForeignKey(mm => mm.CodProduto)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(mm => mm.ProdutoCosif)
                      .WithMany()
                      .HasForeignKey(mm => new { mm.CodProduto, mm.CodCosif })
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // --- ADICIONANDO DADOS INICIAIS (DATA SEEDING) ---

            modelBuilder.Entity<Produto>().HasData(
                new Produto { CodProduto = "0001", DesProduto = "Tesouro Selic", StaStatus = "A" },
                new Produto { CodProduto = "0002", DesProduto = "CDB BNP", StaStatus = "A" },
                new Produto { CodProduto = "0003", DesProduto = "LCI BNP", StaStatus = "I" } // Exemplo de um inativo
            );

            modelBuilder.Entity<ProdutoCosif>().HasData(
                // Cosifs para o Produto 0001
                new ProdutoCosif { CodProduto = "0001", CodCosif = "11111111111", CodClassificacao = "RENDA", StaStatus = "A" },
                new ProdutoCosif { CodProduto = "0001", CodCosif = "22222222222", CodClassificacao = "RENDA", StaStatus = "A" },

                // Cosifs para o Produto 0002
                new ProdutoCosif { CodProduto = "0002", CodCosif = "33333333333", CodClassificacao = "PRIV", StaStatus = "A" },

                // Cosifs para o Produto 0003 (inativo)
                new ProdutoCosif { CodProduto = "0003", CodCosif = "44444444444", CodClassificacao = "PRIV", StaStatus = "I" }
            );
        }
    }
}
