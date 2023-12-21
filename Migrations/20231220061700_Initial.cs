using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace liberacao_credito.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    CPF = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    UF = table.Column<string>(type: "char(2)", nullable: false),
                    Telefone = table.Column<string>(type: "varchar(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.CPF);
                });

            migrationBuilder.CreateTable(
                name: "TipoCredito",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "varchar(30)", nullable: false),
                    Taxa = table.Column<decimal>(type: "numeric(3,2)", nullable: false),
                    IsAtivo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoCredito", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Financiamento",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CPF = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "numeric(9,2)", nullable: false),
                    DataVencimentoUltimo = table.Column<DateTime>(type: "datetime", nullable: false),
                    TipoCreditoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Financiamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Financiamento_Cliente_CPF",
                        column: x => x.CPF,
                        principalTable: "Cliente",
                        principalColumn: "CPF",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Financiamento_TipoCredito_TipoCreditoId",
                        column: x => x.TipoCreditoId,
                        principalTable: "TipoCredito",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parcela",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FinanciamentoId = table.Column<long>(type: "bigint", nullable: false),
                    NumParcela = table.Column<byte>(type: "tinyint", nullable: false),
                    ValorParcela = table.Column<decimal>(type: "numeric(9,2)", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataPagamento = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcela", x => new { x.Id, x.FinanciamentoId });
                    table.ForeignKey(
                        name: "FK_Parcela_Financiamento_FinanciamentoId",
                        column: x => x.FinanciamentoId,
                        principalTable: "Financiamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cliente",
                columns: new[] { "CPF", "Nome", "Telefone", "UF" },
                values: new object[,]
                {
                    { "111.222.333-44", "Cliente3", "11987656000", "SP" },
                    { "123.456.789-09", "Cliente1", "11987654000", "SP" },
                    { "555.666.777-88", "Cliente4", "11987657000", "SP" },
                    { "987.654.321-09", "Cliente2", "21987655000", "RJ" },
                    { "999.888.777-66", "Cliente5", "21987657770", "RJ" }
                });

            migrationBuilder.InsertData(
                table: "TipoCredito",
                columns: new[] { "Id", "Descricao", "IsAtivo", "Taxa" },
                values: new object[,]
                {
                    { 1, "Direto", true, 2m },
                    { 2, "Consignado", true, 1m },
                    { 3, "Pessoa Jurídica", true, 5m },
                    { 4, "Pessoa Física", true, 3m },
                    { 5, "Imobiliário", true, 9m }
                });

            migrationBuilder.InsertData(
                table: "Financiamento",
                columns: new[] { "Id", "CPF", "DataVencimentoUltimo", "TipoCreditoId", "ValorTotal" },
                values: new object[,]
                {
                    { 1L, "123.456.789-09", new DateTime(2022, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 5500m },
                    { 2L, "123.456.789-09", new DateTime(2023, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 75000m },
                    { 3L, "111.222.333-44", new DateTime(2023, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 187500m },
                    { 4L, "999.888.777-66", new DateTime(2022, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 7500m },
                    { 5L, "999.888.777-66", new DateTime(2023, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 10000m },
                    { 6L, "987.654.321-09", new DateTime(2021, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 50000m },
                    { 7L, "987.654.321-09", new DateTime(2023, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 30000m }
                });

            migrationBuilder.InsertData(
                table: "Parcela",
                columns: new[] { "FinanciamentoId", "Id", "DataPagamento", "DataVencimento", "NumParcela", "ValorParcela" },
                values: new object[,]
                {
                    { 1L, 1L, new DateTime(2022, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)1, 1000m },
                    { 1L, 2L, new DateTime(2022, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)2, 1200m },
                    { 1L, 3L, new DateTime(2022, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)3, 800m },
                    { 1L, 4L, null, new DateTime(2022, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)4, 1000m },
                    { 1L, 5L, null, new DateTime(2022, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)5, 1500m },
                    { 2L, 6L, new DateTime(2023, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)1, 15000m },
                    { 2L, 7L, new DateTime(2023, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)2, 15000m },
                    { 2L, 8L, null, new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)3, 15000m },
                    { 2L, 9L, null, new DateTime(2023, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)4, 15000m },
                    { 2L, 10L, null, new DateTime(2023, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)5, 15000m },
                    { 3L, 11L, new DateTime(2022, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)1, 12500m },
                    { 3L, 12L, new DateTime(2022, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)2, 12500m },
                    { 3L, 13L, new DateTime(2022, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)3, 12500m },
                    { 3L, 14L, new DateTime(2022, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)4, 12500m },
                    { 3L, 15L, new DateTime(2022, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)5, 12500m },
                    { 3L, 16L, new DateTime(2022, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)6, 12500m },
                    { 3L, 17L, new DateTime(2023, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)7, 12500m },
                    { 3L, 18L, new DateTime(2023, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)8, 12500m },
                    { 3L, 19L, new DateTime(2023, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)9, 12500m },
                    { 3L, 20L, new DateTime(2023, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)10, 12500m },
                    { 3L, 21L, null, new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)11, 12500m },
                    { 3L, 22L, null, new DateTime(2023, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)12, 12500m },
                    { 3L, 23L, null, new DateTime(2023, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)13, 12500m },
                    { 3L, 24L, null, new DateTime(2023, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)14, 12500m },
                    { 3L, 25L, null, new DateTime(2023, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)15, 12500m },
                    { 4L, 26L, new DateTime(2022, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)1, 500m },
                    { 4L, 27L, new DateTime(2022, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)2, 700m },
                    { 4L, 28L, new DateTime(2022, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)3, 1000m },
                    { 4L, 29L, new DateTime(2022, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)4, 2000m },
                    { 4L, 30L, null, new DateTime(2022, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)5, 3300m },
                    { 5L, 31L, new DateTime(2023, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)1, 1000m },
                    { 5L, 32L, new DateTime(2023, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)2, 1000m },
                    { 5L, 33L, new DateTime(2023, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)3, 1000m },
                    { 5L, 34L, new DateTime(2023, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)4, 1000m },
                    { 5L, 35L, null, new DateTime(2023, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)5, 2000m },
                    { 5L, 36L, null, new DateTime(2023, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)6, 4000m },
                    { 6L, 37L, new DateTime(2021, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)1, 10000m },
                    { 6L, 38L, new DateTime(2021, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)2, 8000m },
                    { 6L, 39L, new DateTime(2021, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)3, 8000m },
                    { 6L, 40L, new DateTime(2021, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)4, 8000m },
                    { 6L, 41L, new DateTime(2021, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)5, 6000m },
                    { 6L, 42L, new DateTime(2021, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)6, 10000m },
                    { 7L, 43L, new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)1, 6000m },
                    { 7L, 44L, new DateTime(2023, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)2, 3000m },
                    { 7L, 45L, new DateTime(2023, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)3, 6000m },
                    { 7L, 46L, new DateTime(2023, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)4, 6000m },
                    { 7L, 47L, new DateTime(2023, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)5, 9000m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Financiamento_CPF",
                table: "Financiamento",
                column: "CPF");

            migrationBuilder.CreateIndex(
                name: "IX_Financiamento_TipoCreditoId",
                table: "Financiamento",
                column: "TipoCreditoId");

            migrationBuilder.CreateIndex(
                name: "IX_Parcela_FinanciamentoId",
                table: "Parcela",
                column: "FinanciamentoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parcela");

            migrationBuilder.DropTable(
                name: "Financiamento");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "TipoCredito");
        }
    }
}
