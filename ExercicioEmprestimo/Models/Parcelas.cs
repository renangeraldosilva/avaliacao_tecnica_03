using System;
using System.ComponentModel.DataAnnotations;

namespace ExercicioEmprestimo.Models
{
    public class Parcelas
    {
        public int Id { get; set; }

        [Display(Name = "Empréstimo")]
        public int EmprestimoId { get; set; }

        public Emprestimo? Emprestimo { get; set; }

        [Display(Name = "Valor da parcela")]
        public decimal ValorParcelas { get; set; }  

        public bool Paga { get; set; }

        [Display(Name = "Data de vencimento")]
        public DateTime DataVencimento { get; set; }
    }
}
