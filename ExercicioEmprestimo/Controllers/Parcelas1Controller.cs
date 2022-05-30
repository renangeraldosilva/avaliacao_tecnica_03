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
    public class Parcelas1Controller : Controller
    {
        private readonly ExercicioEmprestimoContext _context;

        public Parcelas1Controller(ExercicioEmprestimoContext context)
        {
            _context = context;
        }

        // GET: Parcelas1
        public async Task<IActionResult> Index()
        {
            var exercicioEmprestimoContext = _context.Parcelas.Include(p => p.Emprestimo);
            return View(await exercicioEmprestimoContext.ToListAsync());
        }

        // GET: Parcelas1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parcelas = await _context.Parcelas
                .Include(p => p.Emprestimo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parcelas == null)
            {
                return NotFound();
            }

            return View(parcelas);
        }

        // GET: Parcelas1/Create
        public IActionResult Create()
        {
            ViewData["EmprestimoId"] = new SelectList(_context.Emprestimo, "Id", "Dscricao");
            return View();
        }

        // POST: Parcelas1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmprestimoId,ValorParcelas,Paga,DataVencimento")] Parcelas parcelas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parcelas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmprestimoId"] = new SelectList(_context.Emprestimo, "Id", "Dscricao", parcelas.EmprestimoId);
            return View(parcelas);
        }

        // GET: Parcelas1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parcelas = await _context.Parcelas.FindAsync(id);
            if (parcelas == null)
            {
                return NotFound();
            }
            ViewData["EmprestimoId"] = new SelectList(_context.Emprestimo, "Id", "Dscricao", parcelas.EmprestimoId);
            return View(parcelas);
        }

        // POST: Parcelas1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Paga")] Parcelas parcelas)
        {
            if (id != parcelas.Id)
            {
                return NotFound();
            }

            try
            {
                var parcelaSalva = await _context.Parcelas.FindAsync(id);

                if(!parcelaSalva.Paga)
                {
                    parcelaSalva.Paga = parcelas.Paga;

                    _context.Update(parcelaSalva);
                    await _context.SaveChangesAsync();

                    if (parcelas.Paga)
                    {
                        var emprestimo = await _context.Emprestimo.FirstOrDefaultAsync(m => m.Id == parcelaSalva.EmprestimoId);
                        emprestimo.TotalRestante = emprestimo.TotalRestante - parcelaSalva.ValorParcelas;

                        _context.Update(emprestimo);
                        await _context.SaveChangesAsync();
                    }
                }        
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParcelasExists(parcelas.Id))
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

        // GET: Parcelas1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parcelas = await _context.Parcelas
                .Include(p => p.Emprestimo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parcelas == null)
            {
                return NotFound();
            }

            return View(parcelas);
        }

        // POST: Parcelas1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parcelas = await _context.Parcelas.FindAsync(id);
            _context.Parcelas.Remove(parcelas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParcelasExists(int id)
        {
            return _context.Parcelas.Any(e => e.Id == id);
        }
    }
}
