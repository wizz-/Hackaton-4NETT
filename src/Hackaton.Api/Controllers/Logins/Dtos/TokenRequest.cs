namespace Hackaton.Api.Controllers.Logins.Dtos
{
    public class TokenRequest
    {
        public string Id { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Perfil { get; set; } = string.Empty;
        public string Identificador { get; set; } = string.Empty;
    }
}
