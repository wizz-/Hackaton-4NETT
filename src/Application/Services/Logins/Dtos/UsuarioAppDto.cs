using System.Security;

namespace Application.Services.Logins.Dtos
{
    public class UsuarioAppDto
    {
        public string Email { get; set; }
        public SecureString Senha { get; set; }
    }
}
