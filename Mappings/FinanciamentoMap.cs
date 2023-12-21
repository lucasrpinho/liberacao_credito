using liberacao_credito.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace liberacao_credito.Mappings
{
    public class FinanciamentoMap : IEntityTypeConfiguration<Financiamento>
    {
        public void Configure(EntityTypeBuilder<Financiamento> builder)
        {
            builder.ToTable("Financiamento");

            builder.HasKey(t => new {t.Id});

            builder.Property(t => t.Id)
                .ValueGeneratedOnAdd();

            builder.HasOne(p => p.Cliente)
                .WithMany(p => p.Financiamento)
                .HasForeignKey(p => p.CPF);

            builder.Property(p => p.DataVencimentoUltimo)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(p => p.ValorTotal)
                .HasColumnType("numeric(9,2)")
                .IsRequired();

            builder.HasOne(p => p.TipoCredito)
                .WithMany()
                .HasForeignKey(p => p.TipoCreditoId);

            builder.HasData(
                new Financiamento { Id = 1, CPF = "123.456.789-09", ValorTotal = 5500, DataVencimentoUltimo = new DateTime(2022, 5, 15), TipoCreditoId = 1 },
                new Financiamento { Id = 2, CPF = "123.456.789-09", ValorTotal = 75000, DataVencimentoUltimo = new DateTime(2023, 7, 7), TipoCreditoId = 2 },
                new Financiamento { Id = 3, CPF = "111.222.333-44", ValorTotal = 187500, DataVencimentoUltimo = new DateTime(2023, 9, 7), TipoCreditoId = 3 },
                new Financiamento { Id = 4, CPF = "999.888.777-66", ValorTotal = 7500, DataVencimentoUltimo = new DateTime(2022, 9, 7), TipoCreditoId = 4 },
                new Financiamento { Id = 5, CPF = "999.888.777-66", ValorTotal = 10000, DataVencimentoUltimo = new DateTime(2023, 9, 7), TipoCreditoId = 4 },
                new Financiamento { Id = 6, CPF = "987.654.321-09", ValorTotal = 50000, DataVencimentoUltimo = new DateTime(2021, 9, 7), TipoCreditoId = 5 },
                new Financiamento { Id = 7, CPF = "987.654.321-09", ValorTotal = 30000, DataVencimentoUltimo = new DateTime(2023, 9, 7), TipoCreditoId = 5 }
                );
        }
    }
}
