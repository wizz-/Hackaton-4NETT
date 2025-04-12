using Application.Services.Logins.Interfaces;
using Domain.Enums;
using Domain.Interfaces.Infra.Data.DAL;
using System.Security;

namespace Application.Services.LoginsAppService
{
    public class LoginAppService(IUnitOfWork unitOfWork) : ILoginAppService
    {
        public void LoginMedico(string crm, string uf, SecureString senha)
        {
            var ufEnum = ObterUf(uf);
            var medico = unitOfWork.MedicoRepository.ObterPorCrm(crm, ufEnum);
            if (medico == null) throw new UnauthorizedAccessException($"Não foi possível fazer o login.");

            if (!medico.Usuario!.ValidarSenha(senha)) throw new UnauthorizedAccessException($"Não foi possível fazer o login.");
        }

        public void LoginPaciente(string cpfOuEmail, SecureString senha)
        {
            var paciente = unitOfWork.PacienteRepository.ObterPorCpf(cpfOuEmail);
            if (paciente == null) throw new UnauthorizedAccessException($"Não foi possível fazer o login.");

            if (!paciente.Usuario!.ValidarSenha(senha)) throw new UnauthorizedAccessException($"Não foi possível fazer o login.");
        }

        private UnidadeFederativa ObterUf(string uf)
        {
            if (!Enum.IsDefined(typeof(UnidadeFederativa), uf)) throw new InvalidOperationException($"O valor '{uf}' não é válido para o enum.");

            if (!Enum.TryParse(uf, true, out UnidadeFederativa ufEnum)) throw new InvalidOperationException($"O valor '{uf}' não é válido para o enum.");

            return ufEnum;
        }
    }
}
