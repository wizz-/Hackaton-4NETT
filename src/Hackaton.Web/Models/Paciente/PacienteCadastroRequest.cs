using Hackaton.Web.Models.Usuario;

namespace Hackaton.Web.Models.Paciente
{
    public class PacienteCadastroRequest
    {
        public string Nome { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string Email { get; set; }
        public UsuarioRequest Usuario { get; set; } = new();
    }
}
