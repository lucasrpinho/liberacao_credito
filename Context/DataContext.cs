using liberacao_credito.Mappings;
using liberacao_credito.Models;
using Microsoft.EntityFrameworkCore;

namespace liberacao_credito.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TipoCreditoMap());
            modelBuilder.ApplyConfiguration(new ClienteMap());
            modelBuilder.ApplyConfiguration(new FinanciamentoMap());
            modelBuilder.ApplyConfiguration(new ParcelaMap());
        }

        public DbSet<TipoCredito> TipoCreditos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Financiamento> Financiamentos { get; set; }
        public DbSet<Parcela>  Parcelas { get; set; }
    }
}
