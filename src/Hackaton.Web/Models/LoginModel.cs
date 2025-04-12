using Hackaton.Web.Models;
using System.ComponentModel.DataAnnotations;

public class LoginModel
{
    public PerfisDeUsuario Perfil { get; set; }

    [Required(ErrorMessage = "O CRM é obrigatório.")]
    [RegularExpression(@"^\d+$", ErrorMessage = "Informe apenas números.")]
    public string CRM { get; set; } = "";

    [Required(ErrorMessage = "A UF é obrigatória.")]
    public string UF { get; set; } = "";

    [Required(ErrorMessage = "O CPF ou Email é obrigatório.")]
    public string EmailOuCPF { get; set; } = "";

    [Required(ErrorMessage = "A senha é obrigatória.")]
    [StringLength(10, MinimumLength = 3, ErrorMessage = "A senha deve ter entre 3 e 10 caracteres.")]
    public string Senha { get; set; } = "";
}
