using System.Security;

namespace Application.Services.Logins.Interfaces
{
    public interface ILoginAppService
    {
        void LoginMedico(string crm, string uf, SecureString senha);
        void LoginPaciente(string cpf, SecureString senha);
    }
}
