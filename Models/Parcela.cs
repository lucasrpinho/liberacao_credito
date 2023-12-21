namespace liberacao_credito.Models
{
    public class Parcela

    {
        public Parcela() { }
        public Parcela(long id, byte numParcela, decimal valorParcela, DateTime dataVencimento, DateTime? dataPagamento, long financiamentoId)
        {
            Id = id;
            NumParcela = numParcela;
            ValorParcela = valorParcela;
            DataVencimento = dataVencimento;
            DataPagamento = dataPagamento;
            FinanciamentoId = financiamentoId;
        }

        public long Id { get; set; }
        public long FinanciamentoId { get; set; }
        public byte NumParcela { get; set; }
        public decimal ValorParcela { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public Financiamento Financiamento { get;}

    }
}
