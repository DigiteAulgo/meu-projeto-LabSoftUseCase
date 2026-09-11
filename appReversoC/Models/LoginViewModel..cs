using System.ComponentModel.DataAnnotations;
using appReverso.Models;
using appReversoC.Models;
using Microsoft.AspNetCore.Mvc;

namespace appReverso.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [Display(Name = "CPF do Paciente")]
        public string Cpf { get; set; } = string.Empty;
    }
}