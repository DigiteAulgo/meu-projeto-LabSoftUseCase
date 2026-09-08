using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using appReversotask.Models;

namespace appReversotask.Controllers
{
    public class FuncionariosController : Controller
    {
        private readonly dbTasksContext _context;

        public FuncionariosController(dbTasksContext context)
        {
            _context = context;
        }

        // GET: Funcionarios
        public async Task<IActionResult> Index()
        {
            var dbTasksContext = _context.Funcionarios.Include(f => f.CentralCusto).Include(f => f.Departamento).Include(f => f.Gerente);
            return View(await dbTasksContext.ToListAsync());
        }

        // GET: Funcionarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios
                .Include(f => f.CentralCusto)
                .Include(f => f.Departamento)
                .Include(f => f.Gerente)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // GET: Funcionarios/Create
        public IActionResult Create()
        {
            ViewData["CentralCustoId"] = new SelectList(_context.CentralCustos, "Codigo", "Nome");
            ViewData["DepartamentoId"] = new SelectList(_context.Departamentos, "Codigo", "Nome");
            ViewData["GerenteId"] = new SelectList(_context.Funcionarios, "Codigo", "Cargo");
            return View();
        }

        // POST: Funcionarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Nome,Cargo,DepartamentoId,CentralCustoId,GerenteId")] Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(funcionario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CentralCustoId"] = new SelectList(_context.CentralCustos, "Codigo", "Nome", funcionario.CentralCustoId);
            ViewData["DepartamentoId"] = new SelectList(_context.Departamentos, "Codigo", "Nome", funcionario.DepartamentoId);
            ViewData["GerenteId"] = new SelectList(_context.Funcionarios, "Codigo", "Cargo", funcionario.GerenteId);
            return View(funcionario);
        }

        // GET: Funcionarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }
            ViewData["CentralCustoId"] = new SelectList(_context.CentralCustos, "Codigo", "Nome", funcionario.CentralCustoId);
            ViewData["DepartamentoId"] = new SelectList(_context.Departamentos, "Codigo", "Nome", funcionario.DepartamentoId);
            ViewData["GerenteId"] = new SelectList(_context.Funcionarios, "Codigo", "Cargo", funcionario.GerenteId);
            return View(funcionario);
        }

        // POST: Funcionarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,Nome,Cargo,DepartamentoId,CentralCustoId,GerenteId")] Funcionario funcionario)
        {
            if (id != funcionario.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcionario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionarioExists(funcionario.Codigo))
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
            ViewData["CentralCustoId"] = new SelectList(_context.CentralCustos, "Codigo", "Nome", funcionario.CentralCustoId);
            ViewData["DepartamentoId"] = new SelectList(_context.Departamentos, "Codigo", "Nome", funcionario.DepartamentoId);
            ViewData["GerenteId"] = new SelectList(_context.Funcionarios, "Codigo", "Cargo", funcionario.GerenteId);
            return View(funcionario);
        }

        // GET: Funcionarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios
                .Include(f => f.CentralCusto)
                .Include(f => f.Departamento)
                .Include(f => f.Gerente)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario != null)
            {
                _context.Funcionarios.Remove(funcionario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionarioExists(int id)
        {
            return _context.Funcionarios.Any(e => e.Codigo == id);
        }
    }
}
