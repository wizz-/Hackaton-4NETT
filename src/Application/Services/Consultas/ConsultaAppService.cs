using Application.Services.Consultas.Dtos;
using Application.Services.Consultas.Interfaces;
using Domain.Entities.Cadastros;
using Domain.Entities.Consultas;
using Domain.Interfaces.Infra.Data.DAL;

namespace Application.Services.Consultas
{
    public class ConsultaAppService(IUnitOfWork unitOfWork) : IConsultaAppService
    {
        public void MarcarConsulta(ConsultaAppDto dto)
        {
            var paciente = unitOfWork.PacienteRepository.ObterPorId(dto.PacienteId);
            if (paciente == null) throw new InvalidOperationException($"Paciente com id '{dto.PacienteId}' não localizado.");

            var medico = unitOfWork.MedicoRepository.ObterPorId(dto.MedicoId);
            if (medico == null) throw new InvalidOperationException($"Médico com id '{dto.MedicoId}' não localizado.");

            var especialidade = medico.Especialidades!.FirstOrDefault(x => x.Id == dto.EspecialidadeId);
            if (especialidade == null) throw new InvalidOperationException($"Especialidade com id '{dto.EspecialidadeId}' não pertence ao médico {medico.Id}.");

            var horarioFim = dto.Inicio.AddMinutes(medico.TempoDeConsulta);

            var periodo = new Periodo(dto.Inicio, horarioFim);

            var consulta = new Consulta(dto.Dia, paciente, medico, especialidade, periodo);

            unitOfWork.ConsultaRepository.Inserir(consulta);
            unitOfWork.SaveChanges();
        }
    }
}
