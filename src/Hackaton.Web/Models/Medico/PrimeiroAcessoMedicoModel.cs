using System.ComponentModel.DataAnnotations;

namespace Hackaton.Web.Models.Medico
{
    public class PrimeiroAcessoMedicoModel
    {
        [Required(ErrorMessage = "O nome completo é obrigatório.")]
        [StringLength(80, ErrorMessage = "O nome deve ter no máximo 80 caracteres.")]
        public string NomeCompleto { get; set; } = "";

        [Required(ErrorMessage = "O CRM é obrigatório.")]
        [RegularExpression(@"^\d{1,10}$", ErrorMessage = "O CRM deve conter apenas números.")]
        public string CRM { get; set; } = "";

        [Required(ErrorMessage = "A UF é obrigatória.")]
        public string UF { get; set; } = "";

        [Required(ErrorMessage = "A especialidade é obrigatória.")]
        public int? EspecialidadeId { get; set; }
        public string EspecialidadeNome { get; set; } = "";

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "A senha deve ter entre 3 e 10 caracteres.")]
        public string Senha { get; set; } = "";

        [Required(ErrorMessage = "Repita a senha.")]
        [Compare("Senha", ErrorMessage = "As senhas não coincidem.")]
        public string RepetirSenha { get; set; } = "";
    }
}
