using appReverso.Models;
using appReversoC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace appReverso.Controllers
{
    public class AccountController : Controller
    {
        private readonly dbClinicaContext _context;

        public AccountController(dbClinicaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login() => View();


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var paciente = await _context.Pacientes
                .FirstOrDefaultAsync(p => p.Cpf == model.Cpf);

            if (paciente == null)
            {
                ModelState.AddModelError("", "CPF não encontrado.");
                return View(model);
            }

            // Salva na Session
            HttpContext.Session.SetInt32("PacienteId", paciente.Codigo);
            HttpContext.Session.SetString("PacienteNome", paciente.Nome);

            return RedirectToAction("Index", "Consulta");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Limpa a sessão
            return RedirectToAction("Login");
        }

    }
}
