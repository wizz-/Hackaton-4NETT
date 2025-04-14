using Domain.Entities.Cadastros;
using System.Security;

namespace Application.Services.Logins.Interfaces
{
    public interface ILoginAppService
    {
        Medico LoginMedico(string crm, string uf, SecureString senha);
        Paciente LoginPaciente(string cpf, SecureString senha);
    }
}
