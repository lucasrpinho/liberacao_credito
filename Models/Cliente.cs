using System.ComponentModel.DataAnnotations;

namespace liberacao_credito.Models
{
    public class Cliente
    {

        private string _telefone;

        public Cliente() { }
        public Cliente(string cpf, string nome, string uf, string telefone)
        {
            CPF = cpf;
            Nome = nome;
            UF = uf;
            Telefone = telefone;
        }

        [Required(ErrorMessage = "Necessário informar o CPF.")]
        public string CPF { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Nome { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string UF { get; set; }

        [Required]
        [Display(Name = "Telefone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(\d{2}\) \d{5}-\d{4}$", ErrorMessage = "O formato do telefone deve ser (99) 99999-9999")]
        public string Telefone
        {
            get
            {
                if (!string.IsNullOrEmpty(_telefone) && _telefone.Length == 11)
                {
                    return $"({_telefone.Substring(0, 2)}) {_telefone.Substring(2, 5)}-{_telefone.Substring(7)}";
                }
                return _telefone;
            }
            set { _telefone = value; }
        }
        public ICollection<Financiamento>? Financiamento { get; set; }
    }
}
