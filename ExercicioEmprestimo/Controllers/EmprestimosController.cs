using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExercicioEmprestimo.Data;
using ExercicioEmprestimo.Models;

namespace ExercicioEmprestimo.Controllers
{
    public class EmprestimosController : Controller
    {
        private readonly ExercicioEmprestimoContext _context;

        public EmprestimosController(ExercicioEmprestimoContext context)
        {
            _context = context;
        }

        // GET: Emprestimos
        public async Task<IActionResult> Index()
        {
            var exercicioEmprestimoContext = _context.Emprestimo.Include(e => e.Cliente);
            return View(await exercicioEmprestimoContext.ToListAsync());
        }

        // GET: Emprestimos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emprestimo = await _context.Emprestimo
                .Include(e => e.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emprestimo == null)
            {
                return NotFound();
            }

            return View(emprestimo);
        }

        // GET: Emprestimos/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Nome");
            return View();
        }

        // POST: Emprestimos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Dscricao,ClienteId,Total,TotalParcelas,DataContratacao,TotalRestante")] Emprestimo emprestimo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(emprestimo);
                await _context.SaveChangesAsync();

                if(emprestimo.TotalParcelas != 0)
                {
                    var valorParcelas = emprestimo.Total / emprestimo.TotalParcelas;


                    for (int i = 0; i < emprestimo.TotalParcelas; i++)
                    {
                        var dataVencimento = emprestimo.DataContratacao.AddMonths(i+1);

                        var parcela = new Parcelas() { EmprestimoId = emprestimo.Id, Paga = false, DataVencimento = dataVencimento , ValorParcelas = valorParcelas };
                        _context.Add(parcela);
                        await _context.SaveChangesAsync();
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Id", emprestimo.ClienteId);
            return View(emprestimo);
        }

        // GET: Emprestimos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emprestimo = await _context.Emprestimo.FindAsync(id);
            if (emprestimo == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Nome", emprestimo.ClienteId);
            return View(emprestimo);
        }

        // POST: Emprestimos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Dscricao")] Emprestimo emprestimo)
        {
            if (id != emprestimo.Id)
            {
                return NotFound();
            }

            try
            {
                var emprestimoSalvo = await _context.Emprestimo.FindAsync(id);
                emprestimoSalvo.Dscricao = emprestimo.Dscricao;

                _context.Update(emprestimoSalvo);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmprestimoExists(emprestimo.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Emprestimos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emprestimo = await _context.Emprestimo
                .Include(e => e.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emprestimo == null)
            {
                return NotFound();
            }

            return View(emprestimo);
        }

        // POST: Emprestimos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emprestimo = await _context.Emprestimo.FindAsync(id);
            _context.Emprestimo.Remove(emprestimo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmprestimoExists(int id)
        {
            return _context.Emprestimo.Any(e => e.Id == id);
        }
    }
}
