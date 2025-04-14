using Application.Services.Logins.Interfaces;
using Domain.Entities.Cadastros;
using Domain.Enums;
using Domain.Interfaces.Infra.Data.DAL;
using System.Security;

namespace Application.Services.LoginsAppService
{
    public class LoginAppService(IUnitOfWork unitOfWork) : ILoginAppService
    {
        public Medico LoginMedico(string crm, string uf, SecureString senha)
        {
            var ufEnum = ObterUf(uf);
            var medico = unitOfWork.MedicoRepository.ObterPorCrm(crm, ufEnum);
            
            if (medico == null || medico.Usuario is null || !medico.Usuario.ValidarSenha(senha))
                throw new UnauthorizedAccessException("Não foi possível fazer o login.");

            return medico;
        }

        public Paciente LoginPaciente(string cpfOuEmail, SecureString senha)
        {
            var paciente = ObterPaciente(cpfOuEmail);

            if (paciente == null || paciente.Usuario is null || !paciente.Usuario.ValidarSenha(senha))
                throw new UnauthorizedAccessException("Não foi possível fazer o login.");

            return paciente;
        }

        private Paciente? ObterPaciente(string cpfOuEmail)
        {
            return cpfOuEmail.Contains('@')
            ? unitOfWork.PacienteRepository.ObterPorEmail(cpfOuEmail)
            : unitOfWork.PacienteRepository.ObterPorCpf(cpfOuEmail);
        }

        private static UnidadeFederativa ObterUf(string uf)
        {
            if(!Enum.TryParse(uf, true, out UnidadeFederativa ufEnum) ||
                !Enum.IsDefined(typeof(UnidadeFederativa), ufEnum))
            {
                throw new InvalidOperationException($"O valor '{uf}' não é válido para o enum.");
            }

            return ufEnum;
        }
    }
}
