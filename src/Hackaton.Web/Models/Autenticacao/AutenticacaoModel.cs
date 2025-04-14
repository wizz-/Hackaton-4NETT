using Hackaton.Web.Models.Configuracao;
using System.ComponentModel.DataAnnotations;

namespace Hackaton.Web.Models.Autenticacao
{
    public class AutenticacaoModel
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
}