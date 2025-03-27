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
        public void CadastrarPaciente(PacienteAppDto dto)
        {
            var usuario = CriarUsuario(dto.Usuario, TipoDeUsuario.Paciente);
            var paciente = new Paciente(dto.Cpf, dto.Email, dto.Nome, usuario);

            var pacienteDuplicado = unitOfWork.PacienteRepository.ObterPorCpf(paciente.Cpf);//Faço a verificação depois de criar a classe para aplicar as regras de formatação de CPF sempre no domínio
            if (pacienteDuplicado != null) throw new InvalidOperationException("O CPF informado já está já foi cadastrado.");

            unitOfWork.PacienteRepository.Inserir(paciente);
            unitOfWork.SaveChanges();
        }

        public void CadastrarMedico(MedicoAppDto dto)
        {
            var crm = new Crm(dto.CrmNumero, dto.CrmUf);

            var medicoDuplicado = unitOfWork.MedicoRepository.ObterPorCrm(crm.Numero, crm.Uf);//Faço a verificação depois de criar a classe para aplicar as regras de formatação no domínio
            if (medicoDuplicado != null) throw new InvalidOperationException("O CRM informado já está já foi cadastrado.");

            var usuario = CriarUsuario(dto.Usuario, TipoDeUsuario.Medico);
            var especialidades = ObterEspecialidade(dto.Especialidades);
            var horarios = CriarHorarios(dto.HorariosDisponiveis);

            var medico = new Medico(dto.Nome, crm, dto.TempoDeConsulta, especialidades, horarios, usuario);
            unitOfWork.MedicoRepository.Inserir(medico);
            unitOfWork.SaveChanges();
        }

        private IList<HorarioDisponivel> CriarHorarios(IList<HorarioDisponivelAppDto> horariosDisponiveis)
        {
            var horarios = new List<HorarioDisponivel>();

            foreach (var item in horariosDisponiveis)
            {
                var periodo = new Periodo(item.Periodo.Inicio, item.Periodo.Fim);

                var horario = new HorarioDisponivel(item.DiaDaSemana, periodo);

                horarios.Add(horario);
            }

            return horarios;
        }

        private IList<Especialidade> ObterEspecialidade(IList<EspecialidadeAppDto> especialidadesDto)
        {
            var especialidades = new List<Especialidade>();

            foreach (var item in especialidadesDto)
            {
                var especialidade = unitOfWork.EspecialidadeRepository.ObterPorId(item.Id);
                if (especialidade == null) throw new InvalidOperationException($"Especialidade '{item.Nome}' com id '{item.Id}' não foi localizada.");

                especialidades.Add(especialidade);
            }

            return especialidades;
        }

        private Usuario CriarUsuario(UsuarioAppDto usuarioAppDto, TipoDeUsuario tipoDeUsuario)
        {
            var usuarioBd = unitOfWork.UsuarioRepository.ObterPorEmail(usuarioAppDto.Email);
            if (usuarioBd != null) throw new InvalidOperationException("O e-mail informado já está associado com outro usuário.");

            var usuario = new Usuario(usuarioAppDto.Email, usuarioAppDto.Senha, tipoDeUsuario);

            unitOfWork.UsuarioRepository.Inserir(usuario);

            return usuario;
        }
    }
}
