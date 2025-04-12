namespace Hackaton.Web.Models
{
    public class LoginModel
    {
        public PerfisDeUsuario Perfil { get; set; } = PerfisDeUsuario.Medico;
        public string UF { get; set; } = "";
        public string CRM { get; set; } = "";
        public string EmailOuCPF { get; set; } = "";
        public string Senha { get; set; } = "";
    }
}
