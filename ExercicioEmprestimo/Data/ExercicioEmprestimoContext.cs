using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ExercicioEmprestimo.Models;

namespace ExercicioEmprestimo.Data
{
    public class ExercicioEmprestimoContext : DbContext
    {
        public ExercicioEmprestimoContext (DbContextOptions<ExercicioEmprestimoContext> options)
            : base(options)
        {
        }

        public DbSet<ExercicioEmprestimo.Models.Cliente> Cliente { get; set; }

        public DbSet<ExercicioEmprestimo.Models.Emprestimo> Emprestimo { get; set; }

        public DbSet<ExercicioEmprestimo.Models.Parcelas> Parcelas { get; set; }
    }
}
