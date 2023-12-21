using liberacao_credito.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace liberacao_credito.Mappings
{
    public class TipoCreditoMap : IEntityTypeConfiguration<TipoCredito>
    {
        public void Configure(EntityTypeBuilder<TipoCredito> builder)
        {
            builder.ToTable("TipoCredito");

            builder.HasKey(t => t.Id);

            builder.Property(p => p.Descricao)
                .HasColumnType("varchar(30)")
                .IsRequired();

            builder.Property(p => p.IsAtivo)
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(p => p.Taxa)
                .HasColumnType("numeric(3,2)")
                .IsRequired();

            builder.HasData(
                new TipoCredito { Id = 1, Descricao = "Direto",  Taxa = 2, IsAtivo = true},
                new TipoCredito { Id = 2, Descricao = "Consignado", Taxa = 1, IsAtivo = true },
                new TipoCredito { Id = 3, Descricao = "Pessoa Jurídica", Taxa = 5, IsAtivo = true },
                new TipoCredito { Id = 4, Descricao = "Pessoa Física", Taxa = 3, IsAtivo = true },
                new TipoCredito { Id = 5, Descricao = "Imobiliário", Taxa = 9, IsAtivo = true }
                );

        }
    }
}
