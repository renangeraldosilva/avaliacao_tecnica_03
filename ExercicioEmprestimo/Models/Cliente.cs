using System.ComponentModel.DataAnnotations;

namespace ExercicioEmprestimo.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite um nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Informe um telefone")]
        public string Telefone { get; set; }

        [Display(Name="Endereço")]
        [Required(ErrorMessage = "Informe um endereço")]
        public string Endereco { get; set; }
    }
}
