using System.Security;

namespace Hackaton.Api.Controllers.Logins.Dtos
{
    public class PacienteLoginDto
    {
        public string cpfEmail { get; set; }
        public SecureString Senha { get; set; }
    }
}
