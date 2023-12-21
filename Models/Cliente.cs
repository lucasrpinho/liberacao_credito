using System.ComponentModel.DataAnnotations;

namespace liberacao_credito.Models
{
    public class Cliente
    {

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
        public string Telefone { get; set; }
        public ICollection<Financiamento>? Financiamento { get; set; }
    }
}
