using Domain.Entities.Cadastros;
using Domain.Entities.Consultas;
using Domain.Entities.Login;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Hackaton.UnitTest.Domain
{
    public class ConsultaDomainTests
    {
        private static SecureString GetUsuario(string senha)
        {
            var securitySenha = new SecureString();
            senha.ToCharArray().ToList().ForEach(f => securitySenha.AppendChar(f));

            return securitySenha;
        }

        [Fact]
        public void Deve_Criar_Consulta_Com_Status_Pendente()
        {
            // Arrange
            var dia = DateOnly.FromDateTime(DateTime.Today);
            var paciente = new Paciente("12345678909", "paciente@email.com", "João", new Usuario("usuario1", GetUsuario("1234"), TipoDeUsuario.Paciente));
            var crm = new Crm("123456", UnidadeFederativa.SP);
            var medico = new Medico("Dr. André", crm, new Especialidade("Clínico Geral"), new Usuario("usuario2", GetUsuario("1234"), TipoDeUsuario.Medico));
            var periodo = new Periodo(new TimeOnly(10, 0), new TimeOnly(10, 30));
            var especialidade = new Especialidade("Clínico Geral");

            // Act
            var consulta = new Consulta(dia, paciente, medico, especialidade, periodo);

            // Assert
            Assert.Equal(StatusConsulta.Pendente, consulta.Status);
        }

        [Fact]
        public void Deve_Confirmar_Consulta_Com_Medico_Correto()
        {
            var crm = new Crm("123456", UnidadeFederativa.SP);
            var medico = new Medico("Dr. Ana", crm, new Especialidade("Ortopedia"), new Usuario("ana", GetUsuario("1234"), TipoDeUsuario.Medico));
            var paciente = new Paciente("12345678909", "email@email.com", "Carlos", new Usuario("user", GetUsuario("1234"), TipoDeUsuario.Paciente));
            var consulta = new Consulta(DateOnly.FromDateTime(DateTime.Today), paciente, medico, new Especialidade("Ortopedia"), new Periodo(new TimeOnly(9, 0), new TimeOnly(9, 30)));

            // Act
            consulta.ConfirmarConsulta(medico);

            // Assert
            Assert.Equal(StatusConsulta.Confirmada, consulta.Status);
        }

        [Fact]
        public void Deve_Lancar_Excecao_Se_Confirmar_Consulta_Com_Medico_Diferente()
        {
            var medicoDono = new Medico("Dr. Marcos", new Crm("123456", UnidadeFederativa.SP), new Especialidade("Cardio"), new Usuario("a", GetUsuario("1234"), TipoDeUsuario.Medico));
            var medicoOutro = new Medico("Dr. Outro", new Crm("654321", UnidadeFederativa.RJ), new Especialidade("Cardio"), new Usuario("b", GetUsuario("1234"), TipoDeUsuario.Medico));
            var paciente = new Paciente("12345678909", "email@email.com", "Lucas", new Usuario("user", GetUsuario("1234"), TipoDeUsuario.Paciente));
            var consulta = new Consulta(DateOnly.FromDateTime(DateTime.Today), paciente, medicoDono, new Especialidade("Cardio"), new Periodo(new TimeOnly(9, 0), new TimeOnly(9, 30)));

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => consulta.ConfirmarConsulta(medicoOutro));
            Assert.Equal("Consulta não pode ser confirmada por outro médico.", ex.Message);
        }

        [Fact]
        public void Deve_Rejeitar_Consulta_Com_Medico_Correto()
        {
            var medico = new Medico("Dr. Rejeita", new Crm("112233", UnidadeFederativa.MG), new Especialidade("Orto"), new Usuario("x", GetUsuario("1234"), TipoDeUsuario.Medico));
            var paciente = new Paciente("12345678909", "email@email.com", "José", new Usuario("y", GetUsuario("1234"), TipoDeUsuario.Paciente));
            var consulta = new Consulta(DateOnly.FromDateTime(DateTime.Today), paciente, medico, new Especialidade("Orto"), new Periodo(new TimeOnly(10, 0), new TimeOnly(10, 30)));

            // Act
            consulta.RejeitarConsulta(medico);

            // Assert
            Assert.Equal(StatusConsulta.Recusada, consulta.Status);
        }

        [Fact]
        public void Deve_Lancar_Excecao_Se_Rejeitar_Consulta_Com_Medico_Diferente()
        {
            var medicoOriginal = new Medico("Dr. Original", new Crm("999999", UnidadeFederativa.SP), new Especialidade("Geral"), new Usuario("a", GetUsuario("1234"), TipoDeUsuario.Medico));
            var medicoOutro = new Medico("Dr. X", new Crm("111111", UnidadeFederativa.RJ), new Especialidade("Geral"), new Usuario("b", GetUsuario("1234"), TipoDeUsuario.Medico));
            var paciente = new Paciente("12345678909", "email@email.com", "Luana", new Usuario("c", GetUsuario("1234"), TipoDeUsuario.Paciente));
            var consulta = new Consulta(DateOnly.FromDateTime(DateTime.Today), paciente, medicoOriginal, new Especialidade("Geral"), new Periodo(new TimeOnly(8, 0), new TimeOnly(8, 30)));

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => consulta.RejeitarConsulta(medicoOutro));
            Assert.Equal("Consulta não pode ser cancelada por outro médico.", ex.Message);
        }

        [Fact]
        public void Deve_Cancelar_Consulta_Com_Paciente_Correto()
        {
            var paciente = new Paciente("12345678909", "paciente@x.com", "Pedro", new Usuario("u", GetUsuario("1234"), TipoDeUsuario.Paciente));
            var medico = new Medico("Dr. Cancel", new Crm("888888", UnidadeFederativa.SP), new Especialidade("Uro"), new Usuario("m", GetUsuario("1234"), TipoDeUsuario.Medico));
            var consulta = new Consulta(DateOnly.FromDateTime(DateTime.Today), paciente, medico, new Especialidade("Uro"), new Periodo(new TimeOnly(14, 0), new TimeOnly(14, 30)));

            // Act
            consulta.CancelarConsulta(paciente, "Problema pessoal");

            // Assert
            Assert.Equal(StatusConsulta.Cancelada, consulta.Status);
            Assert.Equal("Problema pessoal", consulta.MotivoDeCancelamento);
        }

        [Fact]
        public void Deve_Lancar_Excecao_Se_Cancelar_Consulta_Com_Outro_Paciente()
        {
            var pacienteOriginal = new Paciente("12345678909", "a@a.com", "Alice", new Usuario("x", GetUsuario("1234"), TipoDeUsuario.Paciente));
            var pacienteOutro = new Paciente("98765432100", "b@b.com", "Beatriz", new Usuario("y", GetUsuario("1234"), TipoDeUsuario.Paciente));
            var medico = new Medico("Dr. Confuso", new Crm("777777", UnidadeFederativa.MG), new Especialidade("Clínico"), new Usuario("z", GetUsuario("1234"), TipoDeUsuario.Medico));
            var consulta = new Consulta(DateOnly.FromDateTime(DateTime.Today), pacienteOriginal, medico, new Especialidade("Clínico"), new Periodo(new TimeOnly(15, 0), new TimeOnly(15, 30)));

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => consulta.CancelarConsulta(pacienteOutro, "Outro motivo"));
            Assert.Equal("Consulta não pode ser cancelada por outro paciente", ex.Message);
        }

        [Fact]
        public void Deve_Lancar_Excecao_Se_Motivo_Do_Cancelamento_For_Vazio()
        {
            var paciente = new Paciente("12345678909", "a@a.com", "Marcos", new Usuario("x", GetUsuario("1234"), TipoDeUsuario.Paciente));
            var medico = new Medico("Dr. Cancelador", new Crm("999999", UnidadeFederativa.SP), new Especialidade("Cardio"), new Usuario("y", GetUsuario("1234"), TipoDeUsuario.Medico));
            var consulta = new Consulta(DateOnly.FromDateTime(DateTime.Today), paciente, medico, new Especialidade("Cardio"), new Periodo(new TimeOnly(11, 0), new TimeOnly(11, 30)));

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => consulta.CancelarConsulta(paciente, ""));
            Assert.Equal("Motivo não pode ser nulo", ex.Message);
        }
    }
}
