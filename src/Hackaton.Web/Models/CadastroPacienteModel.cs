using System.ComponentModel.DataAnnotations;

namespace Hackaton.Web.Models
{
    public class CadastroPacienteModel
    {
        [Required(ErrorMessage = "O nome completo é obrigatório.")]
        [StringLength(80, ErrorMessage = "O nome deve ter no máximo 80 caracteres.")]
        public string NomeCompleto { get; set; } = "";

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve conter 11 dígitos numéricos.")]
        public string CPF { get; set; } = "";

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
