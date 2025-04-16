using Domain.Entities.Cadastros;
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
    public class MedicoDomianTests
    {
        private static SecureString GetUsuario(string senha)
        {
            var securitySenha = new SecureString();
            senha.ToCharArray().ToList().ForEach(f => securitySenha.AppendChar(f));

            return securitySenha;
        }

        [Fact]
        public void Deve_Criar_Medico_Com_Dados_Validos()
        {
            // Arrange
            var crm = new Crm("123456", UnidadeFederativa.SP);
            var especialidade = new Especialidade("Cardiologia");
            var senha = "123456";

            var usuario = new Usuario("joaosilva", GetUsuario(senha), TipoDeUsuario.Medico);
            var nome = "Dr. João Silva";

            // Act
            var medico = new Medico(nome, crm, especialidade, usuario);

            // Assert
            Assert.Equal(nome, medico.Nome);
            Assert.Equal(crm, medico.Crm);
            Assert.Equal(especialidade, medico.Especialidade);
            Assert.Equal(usuario, medico.Usuario);

        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Deve_Lancar_Excecao_Se_Nome_For_Invalido(string nomeInvalido)
        {
            var crm = new Crm("123456", UnidadeFederativa.MG);
            var especialidade = new Especialidade("Pediatria");

            var usuario = new Usuario("usuario", GetUsuario("1234"), TipoDeUsuario.Medico);

            var ex = Assert.Throws<InvalidOperationException>(() => new Medico(nomeInvalido, crm, especialidade, usuario));
            Assert.Equal("Nome Inválido.", ex.Message);
        }

        [Fact]
        public void Deve_Lancar_Excecao_Se_Tipo_Usuario_Nao_For_Medico()
        {
            var crm = new Crm("123456", UnidadeFederativa.MG);
            var especialidade = new Especialidade("Pediatria");

            var usuario = new Usuario("usuario", GetUsuario("1234"), TipoDeUsuario.Paciente);

            var ex = Assert.Throws<InvalidOperationException>(() => new Medico("Teste", crm, especialidade, usuario));
            Assert.Equal("O tipo de usuário não é invalido.", ex.Message);
        }

        [Fact]
        public void Deve_Lancar_Excecao_Se_Crm_For_Nulo()
        {
            var especialidade = new Especialidade("Clínico Geral");
            var usuario = new Usuario("usuario", GetUsuario("1234"), TipoDeUsuario.Medico);

            var ex = Assert.Throws<InvalidOperationException>(() => new Medico("Nome", null, especialidade, usuario));
            Assert.Equal("CRM não pode ser nulo.", ex.Message);
        }

        [Fact]
        public void Deve_Lancar_Excecao_Se_Especialidade_For_Nula()
        {
            var crm = new Crm("123456", UnidadeFederativa.MG);
            var usuario = new Usuario("usuario", GetUsuario("1234"), TipoDeUsuario.Medico);

            var ex = Assert.Throws<InvalidOperationException>(() => new Medico("Nome", crm, null, usuario));
            Assert.Equal("Especialidadeñão pode ser nula.", ex.Message); // Obs: erro de digitação no texto original
        }

        [Fact]
        public void Deve_Lancar_Excecao_Se_Usuario_For_Nulo()
        {
            var crm = new Crm("123456", UnidadeFederativa.MG);
            var especialidade = new Especialidade("Ortopedia");

            var ex = Assert.Throws<InvalidOperationException>(() => new Medico("Nome", crm, especialidade, null));
            Assert.Equal("Usuário Inválido.", ex.Message);
        }

        [Fact]
        public void Deve_Cadastrar_Horarios_Com_Sucesso()
        {
            var crm = new Crm("123456", UnidadeFederativa.MG);
            var especialidade = new Especialidade("Dermatologia");
            var usuario = new Usuario("usuario", GetUsuario("1234"), TipoDeUsuario.Medico);
            var medico = new Medico("Dra. Ana", crm, especialidade, usuario);

            var horarios = new List<HorarioDisponivel>
            {
                new HorarioDisponivel(DayOfWeek.Monday,especialidade, new Periodo(new TimeOnly(8, 0), new TimeOnly(12, 0)))
            };

            // Act
            medico.CadastrarHorarios(especialidade, 150.00m, horarios);

            // Assert
            Assert.Equal(especialidade, medico.Especialidade);
            Assert.Equal(150.00m, medico.ValorDaConsulta);
            Assert.Equal(horarios, medico.HorariosDisponiveis);
        }

        [Fact]
        public void Deve_Lancar_Excecao_Se_Lista_De_Horarios_For_Nula()
        {
            var crm = new Crm("123456", UnidadeFederativa.MG);
            var especialidade = new Especialidade("Neurologia");
            var usuario = new Usuario("usuario", GetUsuario("1234"), TipoDeUsuario.Medico);
            var medico = new Medico("Dr. Zé", crm, especialidade, usuario);

            var ex = Assert.Throws<InvalidOperationException>(() =>
                medico.CadastrarHorarios(especialidade, 200, null));

            Assert.Equal("A lista de horários não pode ser nula.", ex.Message);
        }       
    }
}
