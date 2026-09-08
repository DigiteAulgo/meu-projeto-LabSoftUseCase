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
    public class CentralCustoesController : Controller
    {
        private readonly dbTasksContext _context;

        public CentralCustoesController(dbTasksContext context)
        {
            _context = context;
        }

        // GET: CentralCustoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.CentralCustos.ToListAsync());
        }

        // GET: CentralCustoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var centralCusto = await _context.CentralCustos
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (centralCusto == null)
            {
                return NotFound();
            }

            return View(centralCusto);
        }

        // GET: CentralCustoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CentralCustoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Nome,Descricao")] CentralCusto centralCusto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(centralCusto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(centralCusto);
        }

        // GET: CentralCustoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var centralCusto = await _context.CentralCustos.FindAsync(id);
            if (centralCusto == null)
            {
                return NotFound();
            }
            return View(centralCusto);
        }

        // POST: CentralCustoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,Nome,Descricao")] CentralCusto centralCusto)
        {
            if (id != centralCusto.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(centralCusto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CentralCustoExists(centralCusto.Codigo))
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
            return View(centralCusto);
        }

        // GET: CentralCustoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var centralCusto = await _context.CentralCustos
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (centralCusto == null)
            {
                return NotFound();
            }

            return View(centralCusto);
        }

        // POST: CentralCustoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var centralCusto = await _context.CentralCustos.FindAsync(id);
            if (centralCusto != null)
            {
                _context.CentralCustos.Remove(centralCusto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CentralCustoExists(int id)
        {
            return _context.CentralCustos.Any(e => e.Codigo == id);
        }
    }
}
