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
    public class PacienteDomainTests
    {
        [Fact]
        public void Deve_Criar_Paciente_Com_Dados_Validos()
        {
            // Arrange
            var cpf = "123.456.789-09"; // CPF válido
            var email = "paciente@teste.com";
            var nome = "João da Silva";
            var senha = "123456";

            var securitySenha = new SecureString();

            senha.ToCharArray().ToList().ForEach(f => securitySenha.AppendChar(f));

            var usuario = new Usuario("paciente123", securitySenha, TipoDeUsuario.Paciente);

            // Act
            var paciente = new Paciente(cpf, email, nome, usuario);

            // Assert
            Assert.Equal("12345678909", paciente.Cpf);
            Assert.Equal(email, paciente.Email);
            Assert.Equal(nome, paciente.Nome);
            Assert.Equal(usuario, paciente.Usuario);
            Assert.True(usuario.ValidarSenha(securitySenha));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("     ")]
        public void Deve_Lancar_Excecao_Se_Nome_For_Invalido(string nomeInvalido)
        {
            var cpf = "12345678909";
            var email = "teste@teste.com";
            var senha = "123456";

            var securitySenha = new SecureString();
            senha.ToCharArray().ToList().ForEach(f => securitySenha.AppendChar(f));

            var usuario = new Usuario("teste", securitySenha, TipoDeUsuario.Paciente);

            var ex = Assert.Throws<InvalidOperationException>(() => new Paciente(cpf, email, nomeInvalido, usuario));
            Assert.Equal("Nome Inválido.", ex.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData("semarroba.com")]
        [InlineData("invalido@email")] // sem ponto após @
        public void Deve_Lancar_Excecao_Se_Email_For_Invalido(string emailInvalido)
        {
            var cpf = "12345678909";
            var nome = "Maria";
            var senha = "123456";

            var securitySenha = new SecureString();
            senha.ToCharArray().ToList().ForEach(f => securitySenha.AppendChar(f));
            var usuario = new Usuario("teste", securitySenha, TipoDeUsuario.Paciente);

            var ex = Assert.Throws<InvalidOperationException>(() => new Paciente(cpf, emailInvalido, nome, usuario));
            Assert.Equal("E-mail Inválido.", ex.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("11111111111")] // todos iguais
        [InlineData("12345678900")] // CPF inválido
        public void Deve_Lancar_Excecao_Se_Cpf_For_Invalido(string cpfInvalido)
        {
            var email = "valido@teste.com";
            var nome = "Carlos";
            var senha = "123456";

            var securitySenha = new SecureString();
            senha.ToCharArray().ToList().ForEach(f => securitySenha.AppendChar(f));
            var usuario = new Usuario("teste", securitySenha, TipoDeUsuario.Paciente);

            var ex = Assert.Throws<InvalidOperationException>(() => new Paciente(cpfInvalido, email, nome, usuario));
            Assert.Equal("CPF Inválido.", ex.Message);
        }

        [Fact]
        public void Deve_Lancar_Excecao_Se_Usuario_For_Nulo()
        {
            var cpf = "12345678909";
            var email = "teste@teste.com";
            var nome = "Carlos";

            var ex = Assert.Throws<InvalidOperationException>(() => new Paciente(cpf, email, nome, null));
            Assert.Equal("Usuário Inválido.", ex.Message);
        }
    }
}
