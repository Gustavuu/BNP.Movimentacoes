using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BNP.Movimentacoes.Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PRODUTO",
                columns: table => new
                {
                    COD_PRODUTO = table.Column<string>(type: "char(4)", nullable: false),
                    DES_PRODUTO = table.Column<string>(type: "varchar(30)", nullable: true),
                    STA_STATUS = table.Column<string>(type: "char(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUTO", x => x.COD_PRODUTO);
                });

            migrationBuilder.CreateTable(
                name: "PRODUTO_COSIF",
                columns: table => new
                {
                    COD_COSIF = table.Column<string>(type: "char(11)", nullable: false),
                    COD_PRODUTO = table.Column<string>(type: "char(4)", nullable: false),
                    COD_CLASSIFICACAO = table.Column<string>(type: "char(6)", nullable: true),
                    STA_STATUS = table.Column<string>(type: "char(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUTO_COSIF", x => new { x.COD_PRODUTO, x.COD_COSIF });
                    table.ForeignKey(
                        name: "FK_PRODUTO_COSIF_PRODUTO_COD_PRODUTO",
                        column: x => x.COD_PRODUTO,
                        principalTable: "PRODUTO",
                        principalColumn: "COD_PRODUTO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MOVIMENTO_MANUAL",
                columns: table => new
                {
                    DAT_MES = table.Column<decimal>(type: "numeric(2,0)", nullable: false),
                    DAT_ANO = table.Column<decimal>(type: "numeric(4,0)", nullable: false),
                    NUM_LANCAMENTO = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    COD_PRODUTO = table.Column<string>(type: "char(4)", nullable: false),
                    COD_COSIF = table.Column<string>(type: "char(11)", nullable: false),
                    DES_DESCRICAO = table.Column<string>(type: "varchar(50)", nullable: false),
                    DAT_MOVIMENTO = table.Column<DateTime>(type: "datetime", nullable: false),
                    COD_USUARIO = table.Column<string>(type: "varchar(15)", nullable: false),
                    VAL_VALOR = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MOVIMENTO_MANUAL", x => new { x.DAT_MES, x.DAT_ANO, x.NUM_LANCAMENTO });
                    table.ForeignKey(
                        name: "FK_MOVIMENTO_MANUAL_PRODUTO_COD_PRODUTO",
                        column: x => x.COD_PRODUTO,
                        principalTable: "PRODUTO",
                        principalColumn: "COD_PRODUTO",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MOVIMENTO_MANUAL_PRODUTO_COSIF_COD_PRODUTO_COD_COSIF",
                        columns: x => new { x.COD_PRODUTO, x.COD_COSIF },
                        principalTable: "PRODUTO_COSIF",
                        principalColumns: new[] { "COD_PRODUTO", "COD_COSIF" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "PRODUTO",
                columns: new[] { "COD_PRODUTO", "DES_PRODUTO", "STA_STATUS" },
                values: new object[,]
                {
                    { "0001", "Tesouro Selic", "A" },
                    { "0002", "CDB BNP", "A" },
                    { "0003", "LCI BNP", "I" }
                });

            migrationBuilder.InsertData(
                table: "PRODUTO_COSIF",
                columns: new[] { "COD_COSIF", "COD_PRODUTO", "COD_CLASSIFICACAO", "STA_STATUS" },
                values: new object[,]
                {
                    { "11111111111", "0001", "RENDA", "A" },
                    { "22222222222", "0001", "RENDA", "A" },
                    { "33333333333", "0002", "PRIV", "A" },
                    { "44444444444", "0003", "PRIV", "I" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MOVIMENTO_MANUAL_COD_PRODUTO_COD_COSIF",
                table: "MOVIMENTO_MANUAL",
                columns: new[] { "COD_PRODUTO", "COD_COSIF" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MOVIMENTO_MANUAL");

            migrationBuilder.DropTable(
                name: "PRODUTO_COSIF");

            migrationBuilder.DropTable(
                name: "PRODUTO");
        }
    }
}
