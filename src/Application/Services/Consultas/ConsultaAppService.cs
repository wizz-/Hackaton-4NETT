using Application.Services.Consultas.Dtos;
using Application.Services.Consultas.Interfaces;
using Domain.Entities.Cadastros;
using Domain.Entities.Consultas;
using Domain.Interfaces.Infra.Data.DAL;

namespace Application.Services.Consultas
{
    public class ConsultaAppService(IUnitOfWork unitOfWork) : IConsultaAppService
    {
        public void MarcarConsulta(ConsultaDto dto)
        {
            var paciente = unitOfWork.PacienteRepository.ObterPorId(dto.PacienteId);
            if (paciente == null) throw new InvalidOperationException($"Paciente com id '{dto.PacienteId}' não localizado.");

            var medico = unitOfWork.MedicoRepository.ObterPorId(dto.MedicoId);
            if (medico == null) throw new InvalidOperationException($"Médico com id '{dto.MedicoId}' não localizado.");

            var especialidade = unitOfWork.EspecialidadeRepository.ObterPorId(dto.EspecialidadeId);
            if (especialidade == null) throw new InvalidOperationException($"Especialidade com id '{dto.EspecialidadeId}' não existe.");

            var periodo = new Periodo(dto.Inicio, dto.Fim);

            var consulta = new Consulta(dto.Dia, paciente, medico, especialidade, periodo);

            unitOfWork.ConsultaRepository.Inserir(consulta);
            unitOfWork.SaveChanges();
        }
    }
}
