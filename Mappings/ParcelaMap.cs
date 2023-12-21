using liberacao_credito.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace liberacao_credito.Mappings
{
    public class ParcelaMap : IEntityTypeConfiguration<Parcela>
    {
        public void Configure(EntityTypeBuilder<Parcela> builder)
        {
            builder.ToTable("Parcela");

            builder.HasKey(x => new {x.Id, x.FinanciamentoId});

            builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

            builder.Property(p => p.NumParcela)
                .HasColumnType("tinyint")
                .IsRequired();

            builder.Property(p => p.ValorParcela)
                .HasColumnType("numeric(9,2)")
                .IsRequired();

            builder.Property(p => p.DataVencimento)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(p => p.DataPagamento)
                .HasColumnType("datetime");

            builder.HasOne(p => p.Financiamento)
                .WithMany(p => p.Parcela);

            builder.HasData(
                new Parcela { Id = 1, NumParcela = 1, ValorParcela = 1000, DataVencimento = new DateTime(2022, 1, 15), DataPagamento = new DateTime(2022, 1, 20), FinanciamentoId = 1 },
                new Parcela { Id = 2, NumParcela = 2, ValorParcela = 1200, DataVencimento = new DateTime(2022, 2, 15), DataPagamento = new DateTime(2022, 2, 22), FinanciamentoId = 1 },
                new Parcela { Id = 3, NumParcela = 3, ValorParcela = 800, DataVencimento = new DateTime(2022, 3, 15), DataPagamento = new DateTime(2022, 3, 12), FinanciamentoId = 1 },
                new Parcela { Id = 4, NumParcela = 4, ValorParcela = 1000, DataVencimento = new DateTime(2022, 4, 15), DataPagamento = null, FinanciamentoId = 1 },
                new Parcela { Id = 5, NumParcela = 5, ValorParcela = 1500, DataVencimento = new DateTime(2022, 5, 15), DataPagamento = null, FinanciamentoId = 1 },
                //
                //
                new Parcela { Id = 6, NumParcela = 1, ValorParcela = 15000, DataVencimento = new DateTime(2023, 3, 7), DataPagamento = new DateTime(2023, 3, 13), FinanciamentoId = 2 },
                new Parcela { Id = 7, NumParcela = 2, ValorParcela = 15000, DataVencimento = new DateTime(2023, 4, 7), DataPagamento = new DateTime(2023, 4, 22), FinanciamentoId = 2 },
                new Parcela { Id = 8, NumParcela = 3, ValorParcela = 15000, DataVencimento = new DateTime(2023, 5, 7), DataPagamento = null, FinanciamentoId = 2 },
                new Parcela { Id = 9, NumParcela = 4, ValorParcela = 15000, DataVencimento = new DateTime(2023, 6, 7), DataPagamento = null, FinanciamentoId = 2 },
                new Parcela { Id = 10, NumParcela = 5, ValorParcela = 15000, DataVencimento = new DateTime(2023, 7, 7), DataPagamento = null, FinanciamentoId = 2 },
                //
                //
                new Parcela { Id = 11, NumParcela = 1, ValorParcela = 12500, DataVencimento = new DateTime(2022, 7, 7), DataPagamento = new DateTime(2022, 7, 25), FinanciamentoId = 3 },
                new Parcela { Id = 12, NumParcela = 2, ValorParcela = 12500, DataVencimento = new DateTime(2022, 8, 7), DataPagamento = new DateTime(2022, 8, 13), FinanciamentoId = 3 },
                new Parcela { Id = 13, NumParcela = 3, ValorParcela = 12500, DataVencimento = new DateTime(2022, 9, 7), DataPagamento = new DateTime(2022, 9, 8), FinanciamentoId = 3 },
                new Parcela { Id = 14, NumParcela = 4, ValorParcela = 12500, DataVencimento = new DateTime(2022, 10, 7), DataPagamento = new DateTime(2022, 10, 4), FinanciamentoId = 3 },
                new Parcela { Id = 15, NumParcela = 5, ValorParcela = 12500, DataVencimento = new DateTime(2022, 11, 7), DataPagamento = new DateTime(2022, 11, 3), FinanciamentoId = 3 },
                new Parcela { Id = 16, NumParcela = 6, ValorParcela = 12500, DataVencimento = new DateTime(2022, 12, 7), DataPagamento = new DateTime(2022, 12, 17), FinanciamentoId = 3 },
                new Parcela { Id = 17, NumParcela = 7, ValorParcela = 12500, DataVencimento = new DateTime(2023, 1, 7), DataPagamento = new DateTime(2023, 1, 4), FinanciamentoId = 3 },
                new Parcela { Id = 18, NumParcela = 8, ValorParcela = 12500, DataVencimento = new DateTime(2023, 2, 7), DataPagamento = new DateTime(2023, 2, 10), FinanciamentoId = 3 },
                new Parcela { Id = 19, NumParcela = 9, ValorParcela = 12500, DataVencimento = new DateTime(2023, 3, 7), DataPagamento = new DateTime(2023, 3, 11), FinanciamentoId = 3 },
                new Parcela { Id = 20, NumParcela = 10, ValorParcela = 12500, DataVencimento = new DateTime(2023, 4, 7), DataPagamento = new DateTime(2023, 4, 11), FinanciamentoId = 3 },
                new Parcela { Id = 21, NumParcela = 11, ValorParcela = 12500, DataVencimento = new DateTime(2023, 5, 7), DataPagamento = null, FinanciamentoId = 3 },
                new Parcela { Id = 22, NumParcela = 12, ValorParcela = 12500, DataVencimento = new DateTime(2023, 6, 7), DataPagamento = null, FinanciamentoId = 3 },
                new Parcela { Id = 23, NumParcela = 13, ValorParcela = 12500, DataVencimento = new DateTime(2023, 7, 7), DataPagamento = null, FinanciamentoId = 3 },
                new Parcela { Id = 24, NumParcela = 14, ValorParcela = 12500, DataVencimento = new DateTime(2023, 8, 7), DataPagamento = null, FinanciamentoId = 3 },
                new Parcela { Id = 25, NumParcela = 15, ValorParcela = 12500, DataVencimento = new DateTime(2023, 9, 7), DataPagamento = null, FinanciamentoId = 3 },
                //
                //
                new Parcela { Id = 26, NumParcela = 1, ValorParcela = 500, DataVencimento = new DateTime(2022, 5, 7), DataPagamento = new DateTime(2022, 5, 25), FinanciamentoId = 4 },
                new Parcela { Id = 27, NumParcela = 2, ValorParcela = 700, DataVencimento = new DateTime(2022, 6, 7), DataPagamento = new DateTime(2022, 6, 13), FinanciamentoId = 4 },
                new Parcela { Id = 28, NumParcela = 3, ValorParcela = 1000, DataVencimento = new DateTime(2022, 7, 7), DataPagamento = new DateTime(2022, 7, 8), FinanciamentoId = 4 },
                new Parcela { Id = 29, NumParcela = 4, ValorParcela = 2000, DataVencimento = new DateTime(2022, 8, 7), DataPagamento = new DateTime(2022, 8, 4), FinanciamentoId = 4 },
                new Parcela { Id = 30, NumParcela = 5, ValorParcela = 3300, DataVencimento = new DateTime(2022, 9, 7), DataPagamento = null, FinanciamentoId = 4 },
                //
                //
                new Parcela { Id = 31, NumParcela = 1, ValorParcela = 1000, DataVencimento = new DateTime(2023, 4, 7), DataPagamento = new DateTime(2023, 4, 1), FinanciamentoId = 5 },
                new Parcela { Id = 32, NumParcela = 2, ValorParcela = 1000, DataVencimento = new DateTime(2023, 5, 7), DataPagamento = new DateTime(2023, 5, 2), FinanciamentoId = 5 },
                new Parcela { Id = 33, NumParcela = 3, ValorParcela = 1000, DataVencimento = new DateTime(2023, 6, 7), DataPagamento = new DateTime(2023, 6, 3), FinanciamentoId = 5 },
                new Parcela { Id = 34, NumParcela = 4, ValorParcela = 1000, DataVencimento = new DateTime(2023, 7, 7), DataPagamento = new DateTime(2023, 7, 5), FinanciamentoId = 5 },
                new Parcela { Id = 35, NumParcela = 5, ValorParcela = 2000, DataVencimento = new DateTime(2023, 8, 7), DataPagamento = null, FinanciamentoId = 5 },
                new Parcela { Id = 36, NumParcela = 6, ValorParcela = 4000, DataVencimento = new DateTime(2023, 9, 7), DataPagamento = null, FinanciamentoId = 5 },
                //
                //
                new Parcela { Id = 37, NumParcela = 1, ValorParcela = 10000, DataVencimento = new DateTime(2021, 4, 7), DataPagamento = new DateTime(2021, 4, 1), FinanciamentoId = 6 },
                new Parcela { Id = 38, NumParcela = 2, ValorParcela = 8000, DataVencimento = new DateTime(2021, 5, 7), DataPagamento = new DateTime(2021, 5, 2), FinanciamentoId = 6 },
                new Parcela { Id = 39, NumParcela = 3, ValorParcela = 8000, DataVencimento = new DateTime(2021, 6, 7), DataPagamento = new DateTime(2021, 6, 3), FinanciamentoId = 6 },
                new Parcela { Id = 40, NumParcela = 4, ValorParcela = 8000, DataVencimento = new DateTime(2021, 7, 7), DataPagamento = new DateTime(2021, 7, 5), FinanciamentoId = 6 },
                new Parcela { Id = 41, NumParcela = 5, ValorParcela = 6000, DataVencimento = new DateTime(2021, 8, 7), DataPagamento = new DateTime(2021, 8, 5), FinanciamentoId = 6 },
                new Parcela { Id = 42, NumParcela = 6, ValorParcela = 10000, DataVencimento = new DateTime(2021, 9, 7), DataPagamento = new DateTime(2021, 9, 5), FinanciamentoId = 6 },
                //
                //
                new Parcela { Id = 43, NumParcela = 1, ValorParcela = 6000, DataVencimento = new DateTime(2023, 5, 7), DataPagamento = new DateTime(2023, 5, 1), FinanciamentoId = 7 },
                new Parcela { Id = 44, NumParcela = 2, ValorParcela = 3000, DataVencimento = new DateTime(2023, 6, 7), DataPagamento = new DateTime(2023, 6, 2), FinanciamentoId = 7 },
                new Parcela { Id = 45, NumParcela = 3, ValorParcela = 6000, DataVencimento = new DateTime(2023, 7, 7), DataPagamento = new DateTime(2023, 7, 3), FinanciamentoId = 7 },
                new Parcela { Id = 46, NumParcela = 4, ValorParcela = 6000, DataVencimento = new DateTime(2023, 8, 7), DataPagamento = new DateTime(2023, 8, 5), FinanciamentoId = 7 },
                new Parcela { Id = 47, NumParcela = 5, ValorParcela = 9000, DataVencimento = new DateTime(2023, 9, 7), DataPagamento = new DateTime(2023, 9, 7), FinanciamentoId = 7 }
            );
        }
    }
}
