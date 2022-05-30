using System;
using System.ComponentModel.DataAnnotations;

namespace ExercicioEmprestimo.Models
{
    public class Emprestimo
    {
        public int Id { get; set; }

        [Display(Name="Descrição")]
        [Required(ErrorMessage = "Descrição do emprestimo")]
        public string Dscricao { get; set; }

        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

        [Required(ErrorMessage = "Informe o valor total do emprestimo")]
        public decimal Total { get; set; }

        [Display(Name = "Total de parcelas")]
        [Required(ErrorMessage = "Informe o numero de parcelas")]
        [Range(1, 10, ErrorMessage = "Parcelas de 1 à 10 !")]
        public int TotalParcelas { get; set; }

        [Display(Name = "Data de Contratação")]
        [Required(ErrorMessage = "Informe uma data")]
        public DateTime DataContratacao { get; set; }

        [Display(Name = "Valor a pagar")]
        [Required(ErrorMessage = "Campo Requerido")]
        public decimal TotalRestante { get; set; }
    }
}
