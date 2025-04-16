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
    public class UsuarioDomainTests
    {
        private SecureString CriarSecureString(string senha)
        {
            var secure = new SecureString();
            foreach (char c in senha)
                secure.AppendChar(c);
            secure.MakeReadOnly();
            return secure;
        }

        [Fact]
        public void Deve_Criar_Usuario_Com_Email_Salt_Hash_Validos()
        {
            // Arrange
            var email = "usuario@teste.com";
            var senha = CriarSecureString("MinhaSenhaSegura123");
            var tipo = TipoDeUsuario.Paciente;

            // Act
            var usuario = new Usuario(email, senha, tipo);

            // Assert
            Assert.Equal(email, usuario.Email);
            Assert.Equal(tipo, usuario.Tipo);
            Assert.False(string.IsNullOrEmpty(usuario.Salt));
            Assert.False(string.IsNullOrEmpty(usuario.SenhaHash));
        }

        [Fact]
        public void Deve_Validar_Senha_Correta()
        {
            // Arrange
            var senhaOriginal = CriarSecureString("Senha@123");
            var usuario = new Usuario("email@teste.com", senhaOriginal, TipoDeUsuario.Medico);

            // Act
            var resultado = usuario.ValidarSenha(CriarSecureString("Senha@123"));

            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public void Nao_Deve_Validar_Senha_Incorreta()
        {
            // Arrange
            var senhaOriginal = CriarSecureString("SenhaCorreta!");
            var usuario = new Usuario("user@teste.com", senhaOriginal, TipoDeUsuario.Medico);

            // Act
            var resultado = usuario.ValidarSenha(CriarSecureString("SenhaErrada"));

            // Assert
            Assert.False(resultado);
        }

        [Fact]
        public void Deve_Lancar_Excecao_Se_SecureString_For_Nulo()
        {
            // Arrange
            var usuario = new Usuario("teste@teste.com", CriarSecureString("123456"), TipoDeUsuario.Paciente);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => usuario.ValidarSenha(null));
        }
    }
}
