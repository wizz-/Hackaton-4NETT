using Application.Services.Cadastros.Dtos;
using Application.Services.Cadastros.Interfaces;
using Application.Services.Logins.Dtos;
using Domain.Entities.Cadastros;
using Domain.Entities.Login;
using Domain.Enums;
using Domain.Interfaces.Infra.Data.DAL;

namespace Application.Services.Cadastros
{
    public class CadastroAppService(IUnitOfWork unitOfWork) : ICadastroAppService
    {
        public void CadastrarPaciente(PacienteDto dto)
        {
            var usuario = CriarUsuario(dto.Usuario, TipoDeUsuario.Paciente);
            var paciente = new Paciente(dto.Cpf, dto.Email, dto.Nome, usuario);

            var pacienteDuplicado = unitOfWork.PacienteRepository.ObterPorCpf(paciente.Cpf);//Faço a verificação depois de criar a classe para aplicar as regras de formatação de CPF sempre no domínio
            if (pacienteDuplicado != null) throw new InvalidOperationException("O CPF informado já está já foi cadastrado.");

            unitOfWork.PacienteRepository.Inserir(paciente);
            unitOfWork.SaveChanges();
        }

        public void CadastrarMedico(MedicoDto dto)
        {
            var ufEnum = ObterUf(dto.CrmUf);
            var crm = new Crm(dto.CrmNumero, ufEnum);

            var medicoDuplicado = unitOfWork.MedicoRepository.ObterPorCrm(crm.Numero, crm.Uf);//Faço a verificação depois de criar a classe para aplicar as regras de formatação no domínio
            if (medicoDuplicado != null) throw new InvalidOperationException("O CRM informado já está cadastrado.");

            var usuario = CriarUsuario(dto.Usuario, TipoDeUsuario.Medico);
            var especialidade = ObterEspecialidade(dto.Especialidade);

            var medico = new Medico(dto.Nome, crm, especialidade, usuario);
            unitOfWork.MedicoRepository.Inserir(medico);
            unitOfWork.SaveChanges();
        }

        public void CadastrarHorariosDisponiveis(int medicoId, EspecialidadeDto especialidadeDto, decimal valorDaConsulta, IList<HorarioDisponivelDto> horarioDisponivelDto)
        {
            var medico = unitOfWork.MedicoRepository.ObterPorId(medicoId);
            if (medico == null) throw new InvalidOperationException($"Médico com ID '{medicoId}' não existe.");
            var especialidade = ObterEspecialidade(especialidadeDto);

            var horarios = CriarHorarios(horarioDisponivelDto, especialidade);

            medico.CadastrarHorarios(especialidade, valorDaConsulta, horarios);
            unitOfWork.MedicoRepository.Atualizar(medico);
            unitOfWork.SaveChanges();
        }


        private IList<HorarioDisponivel> CriarHorarios(IList<HorarioDisponivelDto> horariosDisponiveis, Especialidade especialidade)
        {
            var horarios = new List<HorarioDisponivel>();

            foreach (var item in horariosDisponiveis)
            {
                var periodo = new Periodo(item.Periodo.Inicio, item.Periodo.Fim);

                var horario = new HorarioDisponivel(item.DiaDaSemana, especialidade, periodo);

                horarios.Add(horario);
            }

            return horarios;
        }

        private Especialidade ObterEspecialidade(EspecialidadeDto especialidadeDto)
        {
            var especialidade = unitOfWork.EspecialidadeRepository.ObterPorId(especialidadeDto.Id);
            if (especialidade == null) throw new InvalidOperationException($"Especialidade '{especialidadeDto.Nome}' com id '{especialidadeDto.Id}' não foi localizada.");

            return especialidade;
        }

        private Usuario CriarUsuario(UsuarioAppDto usuarioAppDto, TipoDeUsuario tipoDeUsuario)
        {
            var usuarioBd = unitOfWork.UsuarioRepository.ObterPorEmail(usuarioAppDto.Email);
            if (usuarioBd != null) throw new InvalidOperationException("O e-mail informado já está associado com outro usuário.");

            var usuario = new Usuario(usuarioAppDto.Email, usuarioAppDto.Senha, tipoDeUsuario);

            return usuario;
        }

        private UnidadeFederativa ObterUf(string uf)
        {
            if (!Enum.TryParse(uf, true, out UnidadeFederativa ufEnum)) throw new InvalidOperationException($"O valor '{uf}' não é válido para o enum.");

            if (!Enum.IsDefined(typeof(UnidadeFederativa), ufEnum)) throw new InvalidOperationException($"O valor '{uf}' não é válido para o enum.");

            return ufEnum;
        }
    }
}
