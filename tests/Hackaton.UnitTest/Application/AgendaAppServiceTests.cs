using Application.Services.Agendas;
using Domain.Entities.Cadastros;
using Domain.Entities.Consultas;
using Domain.Interfaces.Infra.Data.DAL.Repositories;
using Domain.Interfaces.Infra.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Domain.Exceptions;
using Moq;
using Domain.Entities.Login;
using Domain.Enums;
using System.Security;

namespace Hackaton.UnitTest.Application
{
    public class AgendaAppServiceTests
    {
        private static SecureString GetSecureUsuario(string senha)
        {
            var securitySenha = new SecureString();
            senha.ToCharArray().ToList().ForEach(f => securitySenha.AppendChar(f));

            return securitySenha;
        }

        private static Medico GetMedico()
        {
            var crm = new Crm("123456", UnidadeFederativa.SP);
            var especialidade = new Especialidade("Cardiologia");
            var senha = "123456";

            var usuario = new Usuario("joaosilva", GetSecureUsuario(senha), TipoDeUsuario.Medico);
            var nome = "Dr. João Silva";

            return new Medico(nome, crm, especialidade, usuario);
        }

        private static Paciente GetPaciente()
        {
            string email = "maya_porto@capgemini.com";
            return new Paciente("42310345679", email, "Maya Rafaela Natália Porto", GetUsuario(email));
        }

        private static Usuario GetUsuario(string email)
        {
            var senha = "123456";

            return new Usuario(email, GetSecureUsuario(senha), TipoDeUsuario.Paciente);
        }

        private static Especialidade GetEspecialidade()
        {
            return new Especialidade("Clinico Geral");
        }

        private static Periodo GetPeriodo()
        {
            return new Periodo(new TimeOnly(9, 0), new TimeOnly(9, 30));
        }

        private List<HorarioDisponivel> GetHorariosDisponivel()
        {
            return new List<HorarioDisponivel>
            {
                new HorarioDisponivel(DayOfWeek.Sunday,GetEspecialidade(), GetPeriodo()),
                 new HorarioDisponivel(DayOfWeek.Sunday,GetEspecialidade(), new Periodo(new TimeOnly(9, 30), new TimeOnly(10,0))),
                 new HorarioDisponivel(DayOfWeek.Sunday,GetEspecialidade(), new Periodo(new TimeOnly(10,0), new TimeOnly(10,30))),
                new HorarioDisponivel(DayOfWeek.Friday,GetEspecialidade(), GetPeriodo()),
                new HorarioDisponivel(DayOfWeek.Tuesday,GetEspecialidade(), GetPeriodo()),
                new HorarioDisponivel(DayOfWeek.Monday,GetEspecialidade(), GetPeriodo()),
            };
        }


        [Fact]
        public void ObterAgenda_SemMedicosDisponiveis_DeveLancarNotFoundException()
        {
            // Arrange
            var dia = new DateOnly(2025, 4, 20);
            const int especialidadeId = 1;
            var mockUow = new Mock<IUnitOfWork>();
            mockUow
                .Setup(u => u.MedicoRepository.ObterPorDisponibilidade(dia.DayOfWeek, especialidadeId))
                .Returns(new List<Medico>()); // sem médicos

            var service = new AgendaAppService(mockUow.Object);

            // Act & Assert
            var ex = Assert.Throws<NotFoundException>(() => service.ObterAgenda(dia, especialidadeId));
            string message = "Não foi encontrado: {0}";
            Assert.Equal(string.Format(message, "Médico disponível"), ex.Message);
        }

        [Fact]
        public void ObterAgenda_SemConsultas_DoDia_DeveLancarNotFoundException()
        {
            // Arrange            
            var dia = new DateOnly(2025, 4, 20);
            const int especialidadeId = 1;
            var medico = GetMedico();

            var medicos = new List<Medico> { medico };

            var mockMedRepo = new Mock<IMedicoRepository>();
            mockMedRepo
                .Setup(r => r.ObterPorDisponibilidade(dia.DayOfWeek, especialidadeId))
                .Returns(medicos);

            var mockConsRepo = new Mock<IConsultaRepository>();
            mockConsRepo
                .Setup(r => r.ObterConsultasNaoCanceladasDoDia(dia, medicos))
                .Returns(new List<Consulta>()); // sem consultas

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(u => u.MedicoRepository).Returns(mockMedRepo.Object);
            mockUow.Setup(u => u.ConsultaRepository).Returns(mockConsRepo.Object);

            var service = new AgendaAppService(mockUow.Object);

            // Act & Assert
            var ex = Assert.Throws<NotFoundException>(() =>
                service.ObterAgenda(dia, especialidadeId));
            string message = "Não foi encontrado: {0}";
            Assert.Equal(string.Format(message, "Consultas para o dia"), ex.Message);
        }

        [Fact]
        public void ObterAgenda_ComHorarioDisponivel_DeveRetornarDtoComHorarios()
        {
            // Arrange
            var dia = new DateOnly(2025, 4, 20);
            const int especialidadeId = 1;
            var medico = GetMedico();
            medico.CadastrarHorarios(GetEspecialidade(), 10, GetHorariosDisponivel());
            var medicos = new List<Medico> { medico };

            // Simula uma consulta ocupando às 09:00
            var consultaOcupada = new Consulta(dia, GetPaciente(), medico, GetEspecialidade(), GetPeriodo());
            ;
            var consultas = new List<Consulta> { consultaOcupada };

            var mockMedRepo = new Mock<IMedicoRepository>();
            mockMedRepo
                .Setup(r => r.ObterPorDisponibilidade(dia.DayOfWeek, especialidadeId))
                .Returns(medicos);

            var mockConsRepo = new Mock<IConsultaRepository>();
            mockConsRepo
                .Setup(r => r.ObterConsultasNaoCanceladasDoDia(dia, medicos))
                .Returns(consultas);

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(u => u.MedicoRepository).Returns(mockMedRepo.Object);
            mockUow.Setup(u => u.ConsultaRepository).Returns(mockConsRepo.Object);

            var service = new AgendaAppService(mockUow.Object);

            // Act
            var result = service.ObterAgenda(dia, especialidadeId);

            // Assert
            Assert.Single(result);
            var dto = result.First();
            Assert.Equal(medico.Id, dto.Id);
            Assert.Equal(medico.Crm.Numero, dto.CrmNumero);
            Assert.Equal(medico.Crm.Uf, dto.CrmUf);
            Assert.Equal(medico.Nome, dto.Nome);

            // deve conter horários disponíveis, mas não o ocupado (09:00)
            Assert.NotEmpty(dto.HorariosDisponiveis);
            //Assert.DoesNotContain(consultaOcupada.Horario, dto.HorariosDisponiveis);
        }


    }
}
