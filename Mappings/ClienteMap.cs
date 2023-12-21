using liberacao_credito.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace liberacao_credito.Mappings
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Cliente");

            builder.HasKey(p => p.CPF);
            builder.Property(p => p.CPF)
                .ValueGeneratedNever();

            builder.Property(p => p.Telefone)
                .HasColumnType("varchar(11)")
                .IsRequired();

            builder.Property(p=>p.UF)
                .HasColumnType("char(2)")
                .IsRequired();

            builder.Property(p=>p.Nome)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.HasMany(p => p.Financiamento)
                .WithOne();

            builder.HasData(
                new Cliente { CPF = "123.456.789-09", Nome = "Cliente1", UF = "SP", Telefone = "11987654000", },
                new Cliente { CPF = "987.654.321-09", Nome = "Cliente2", UF = "RJ", Telefone = "21987655000" },
                new Cliente { CPF = "111.222.333-44", Nome = "Cliente3", UF = "SP", Telefone = "11987656000" },
                new Cliente { CPF = "555.666.777-88", Nome = "Cliente4", UF = "SP", Telefone = "11987657000" },
                new Cliente { CPF = "999.888.777-66", Nome = "Cliente5", UF = "RJ", Telefone = "21987657770" }
                );
        }
    }
}
