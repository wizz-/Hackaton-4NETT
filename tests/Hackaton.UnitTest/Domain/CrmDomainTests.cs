using Domain.Entities.Cadastros;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackaton.UnitTest.Domain
{
    public class CrmDomainTests
    {
        [Fact]
        public void Deve_Criar_Crm_Valido()
        {
            // Arrange
            var numero = "123456";
            var uf = UnidadeFederativa.SP;

            // Act
            var crm = new Crm(numero, uf);

            // Assert
            Assert.Equal(numero, crm.Numero);
            Assert.Equal(uf, crm.Uf);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("     ")]
        public void Deve_Lancar_Excecao_Se_Crm_For_Nulo_Ou_Vazio(string numeroInvalido)
        {
            // Arrange
            var uf = UnidadeFederativa.SP;

            // Act & Assert
            var ex = Assert.Throws<InvalidCastException>(() => new Crm(numeroInvalido, uf));
            Assert.Equal("CRM não pode ser nulo.", ex.Message);
        }

        [Theory]
        [InlineData("ABC123")]  // contém letras
        [InlineData("123")]     // menos de 6 dígitos
        [InlineData("1234567")] // mais de 6 dígitos
        public void Deve_Lancar_Excecao_Se_Crm_Tiver_Formato_Invalido(string numeroInvalido)
        {
            // Arrange
            var uf = UnidadeFederativa.MG;

            // Act & Assert
            var ex = Assert.Throws<InvalidCastException>(() => new Crm(numeroInvalido, uf));
            Assert.Equal($"CRM '{numeroInvalido}' não é válido.", ex.Message);
        }

        [Fact]
        public void Deve_Lancar_Excecao_Se_Uf_For_Invalida()
        {
            // Arrange
            var numero = "123456";
            var ufInvalido = (UnidadeFederativa)999;

            // Act & Assert
            var ex = Assert.Throws<InvalidCastException>(() => new Crm(numero, ufInvalido));
            Assert.Equal($"A Unidade federativa '{ufInvalido}' não é válida.", ex.Message);
        }
    }
}
