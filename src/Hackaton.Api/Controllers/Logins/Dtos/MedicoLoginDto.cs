using System.Security;

namespace Hackaton.Api.Controllers.Logins.Dtos
{
    public class MedicoLoginDto
    {
        public string Crm { get; set; }
        public string Uf { get; set; }
        public SecureString Senha { get; set; }
    }
}
