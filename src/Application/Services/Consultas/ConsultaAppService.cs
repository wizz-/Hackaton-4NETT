﻿using Application.Services.Consultas.Dtos;
using Application.Services.Consultas.Interfaces;
using Application.Services.Consultas.Mappers.Interfaces;
using Domain.Entities.Cadastros;
using Domain.Entities.Consultas;
using Domain.Enums;
using Domain.Interfaces.Infra.Data.DAL;

namespace Application.Services.Consultas
{
    public class ConsultaAppService(IUnitOfWork unitOfWork, IMapperConsultaAppService mapper) : IConsultaAppService
    {
        public IList<ConsultaDto> ObterConsultasDoMedico(int medicoId)
        {
            var consultas = unitOfWork.ConsultaRepository.ObterConsultasFuturasPorMedico(medicoId);

            return mapper.Map(consultas);
        }

        public void CancelarConsulta(int consultaId, string cpf, string motivo)
        {
            var consulta = ObterConsulta(consultaId);

            var paciente = unitOfWork.PacienteRepository.ObterPorCpf(cpf);
            if (paciente == null) throw new InvalidOperationException($"Paciente com CPF '{cpf}' não localizado.");

            consulta.CancelarConsulta(paciente, motivo);
            unitOfWork.ConsultaRepository.Atualizar(consulta);
            unitOfWork.SaveChanges();
        }

        public void ConfirmarConsulta(int consultaId, string crm, string uf)
        {
            var consulta = ObterConsulta(consultaId);

            var medico = ObterMedico(crm, uf);

            consulta.ConfirmarConsulta(medico);
            unitOfWork.ConsultaRepository.Atualizar(consulta);
            unitOfWork.SaveChanges();
        }

        public void RejeitarConsulta(int consultaId, string crm, string uf)
        {
            var consulta = ObterConsulta(consultaId);

            var medico = ObterMedico(crm, uf);

            consulta.RejeitarConsulta(medico);
            unitOfWork.ConsultaRepository.Atualizar(consulta);
            unitOfWork.SaveChanges();
        }

        public void MarcarConsulta(ConsultaCadastroDto dto)
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

        private Consulta ObterConsulta(int consultaId)
        {
            var consulta = unitOfWork.ConsultaRepository.ObterPorId(consultaId);
            if (consulta == null) throw new InvalidOperationException($"Consulta com {consultaId} não localizada");
            return consulta;
        }

        private Medico ObterMedico(string crm, string uf)
        {
            Enum.TryParse<UnidadeFederativa>(uf, out var ufEnum);

            if (!Enum.IsDefined(typeof(UnidadeFederativa), ufEnum))
            {
                throw new InvalidOperationException($"Unidade Federativa '{uf}' não localizada.");
            }

            var medico = unitOfWork.MedicoRepository.ObterPorCrm(crm, ufEnum);
            if (medico == null) throw new InvalidOperationException($"Médico com CRM '{crm}' da UF '{uf}' não localizado.");
            return medico;
        }
    }
}
