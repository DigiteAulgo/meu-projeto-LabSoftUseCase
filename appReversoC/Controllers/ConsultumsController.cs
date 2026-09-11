using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using appReversoC.Models;
using Microsoft.AspNetCore.Authorization;

namespace appReversoC.Controllers
{
    [Authorize]
    public class ConsultumsController : Controller
    {
        private readonly dbClinicaContext _context;

        public ConsultumsController(dbClinicaContext context)
        {
            _context = context;
        }

        // GET: Consultums
        public async Task<IActionResult> Index()
        {
            var dbClinicaContext = _context.Consulta.Include(c => c.Medico).Include(c => c.Paciente);
            return View(await dbClinicaContext.ToListAsync());
        }

        // GET: Consultums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultum = await _context.Consulta
                .Include(c => c.Medico)
                .Include(c => c.Paciente)
                .FirstOrDefaultAsync(m => m.Codigo == id);

            if (consultum == null)
            {
                return NotFound();
            }

            return View(consultum);
        }

        // GET: Consultums/Create
        public IActionResult Create()
        {
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "Codigo", "Crm");
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Codigo", "Cpf");
            return View();
        }

        // POST: Consultums/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,DataHora,StatusConsulta,PacienteId,MedicoId")] Consultum consultum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consultum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "Codigo", "Crm", consultum.MedicoId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Codigo", "Cpf", consultum.PacienteId);
            return View(consultum);
        }

        // GET: Consultums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultum = await _context.Consulta.FindAsync(id);
            if (consultum == null)
            {
                return NotFound();
            }
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "Codigo", "Crm", consultum.MedicoId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Codigo", "Cpf", consultum.PacienteId);
            return View(consultum);
        }

        // POST: Consultums/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,DataHora,StatusConsulta,PacienteId,MedicoId")] Consultum consultum)
        {
            if (id != consultum.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consultum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultumExists(consultum.Codigo))
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
            ViewData["MedicoId"] = new SelectList(_context.Medicos, "Codigo", "Crm", consultum.MedicoId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Codigo", "Cpf", consultum.PacienteId);
            return View(consultum);
        }

        // GET: Consultums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultum = await _context.Consulta
                .Include(c => c.Medico)
                .Include(c => c.Paciente)
                .FirstOrDefaultAsync(m => m.Codigo == id);

            if (consultum == null)
            {
                return NotFound();
            }

            return View(consultum);
        }

        // POST: Consultums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consultum = await _context.Consulta.FindAsync(id);
            if (consultum != null)
            {
                _context.Consulta.Remove(consultum);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultumExists(int id)
        {
            return _context.Consulta.Any(e => e.Codigo == id);
        }
    }
}