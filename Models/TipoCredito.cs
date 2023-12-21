namespace liberacao_credito.Models
{
    public class TipoCredito
    {

        public TipoCredito() { }
        public TipoCredito(int id, string descricao, decimal taxa, bool isAtivo)
        {
            Id = id;
            Descricao = descricao;
            Taxa = taxa;
            IsAtivo = isAtivo;
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Taxa { get; set; }
        public bool IsAtivo { get; set; } = true;

    }
}
