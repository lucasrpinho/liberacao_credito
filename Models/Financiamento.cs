using Humanizer;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace liberacao_credito.Models
{
    public class Financiamento
    {
        public Financiamento() { }

        public Financiamento(long id, string cPF, decimal valorTotal, DateTime dataVencimentoUltimo, byte tipoCreditoId)
        {
            Id = id;
            CPF = cPF;
            ValorTotal = valorTotal;
            DataVencimentoUltimo = dataVencimentoUltimo;
            TipoCreditoId = tipoCreditoId;
        }

        public long Id { get; set; } = 0;

        [Required(ErrorMessage = "O campo CPF é obrigatório.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Necessário informar o valor desejado para o crédito.")]
        [Range(0.1, double.MaxValue, ErrorMessage = "O valor de crédito informado precisa ser maior que zero.")]

        [Display(Name = "Valor Total")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal ValorTotal { get; set; }

        [NotMapped]
        [DataType(DataType.Date)]
        public DateTime DataVencimentoPrimeiro { get; set; }

        [Display(Name = "Data do Último Vencimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataVencimentoUltimo { get; set; }

        [Display(Name = "Quantidade de Parcelas")]
        [NotMapped]
        [Required]
        [Range(1, 72)]
        public byte QtdParcelas { get; set; }

        [Display(Name = "Tipo de Crédito")]
        public int TipoCreditoId { get; set; }

        [Display(Name = "Tipo de Crédito")]
        public TipoCredito? TipoCredito { get; set; }

        public ICollection<Parcela>? Parcela { get; set; }
        public Cliente? Cliente { get; set; }
    }

    public class DateRangeAttribute : ValidationAttribute
    {
        private readonly int _minDias;
        private readonly int _maxDias;

        public DateRangeAttribute(int minData, int maxData)
        {
            _minDias = minData;
            _maxDias = maxData;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime data)
            {
                DateTime dataAtual = DateTime.Now.Date;
                DateTime minData = dataAtual.AddDays(_minDias);
                DateTime maxData = dataAtual.AddDays(_maxDias);

                if (data < minData || data > maxData)
                {
                    return new ValidationResult($"A data deve estar entre {minData.ToShortDateString()} e {maxData.ToShortDateString()}");
                }
            }

            return ValidationResult.Success;
        }
    }

    public class ValorTotalAttribute : ValidationAttribute
    {
        private readonly decimal _valorMinimo;
        private readonly int _tipoCreditoIdPessoaJuridica;

        public ValorTotalAttribute(int valorMinimo, int tipoCreditoIdPessoaJuridica)
        {
            _valorMinimo = valorMinimo;
            _tipoCreditoIdPessoaJuridica = tipoCreditoIdPessoaJuridica;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is decimal valorTotal)
            {
                var tipoCreditoId = (int)validationContext.ObjectInstance.GetType().GetProperty("TipoCreditoId").GetValue(validationContext.ObjectInstance);

                if (tipoCreditoId > 0)
                {

                    if (tipoCreditoId == _tipoCreditoIdPessoaJuridica && valorTotal < _valorMinimo)
                    {
                        return new ValidationResult($"Para Pessoa Jurídica, o valor total mínimo aceito é de {_valorMinimo:C}.");
                    }
                }
                else
                {
                    return new ValidationResult("O tipo de crédito não foi selecionado.");
                }                
            }
            return ValidationResult.Success;
        }
    }
}
