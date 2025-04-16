using Domain.Entities.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackaton.UnitTest.Domain
{
    public class PeriodoDomainTests
    {
        [Fact]
        public void Deve_Criar_Periodo_Valido()
        {
            // Arrange
            var inicio = new TimeOnly(8, 0);
            var fim = new TimeOnly(12, 0);

            // Act
            var periodo = new Periodo(inicio, fim);

            // Assert
            Assert.Equal(inicio, periodo.Inicio);
            Assert.Equal(fim, periodo.Fim);
        }

        [Fact]
        public void Deve_Lancar_Excecao_Se_Inicio_Maior_Que_Fim()
        {
            // Arrange
            var inicio = new TimeOnly(14, 0);
            var fim = new TimeOnly(12, 0);

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => new Periodo(inicio, fim));
            Assert.Equal($"Horário de início '{inicio}' não pode ser maior que o horário fim '{fim}'.", ex.Message);
        }

        [Fact]
        public void Deve_Gerar_Horarios_Disponiveis_Sem_Conflito()
        {
            // Arrange
            var inicio = new TimeOnly(8, 0);
            var fim = new TimeOnly(9, 0);
            var periodo = new Periodo(inicio, fim);
            int tempoConsulta = 30;

            var periodosOcupado = new List<Periodo>(); // nenhum bloqueio

            // Act
            var horarios = periodo.GerarHorarios(tempoConsulta, periodosOcupado);

            // Assert
            Assert.Equal(2, horarios.Count);
            Assert.Equal(new TimeOnly(8, 0), horarios[0]);
            Assert.Equal(new TimeOnly(8, 30), horarios[1]);
        }

        [Fact]
        public void Deve_Gerar_Horarios_Desconsiderando_Conflitos()
        {
            // Arrange
            var inicio = new TimeOnly(8, 0);
            var fim = new TimeOnly(10, 0);
            var periodo = new Periodo(inicio, fim);
            int tempoConsulta = 30;

            var ocupado = new Periodo(new TimeOnly(8, 30), new TimeOnly(9, 0));
            var periodosOcupado = new List<Periodo> { ocupado };

            // Act
            var horarios = periodo.GerarHorarios(tempoConsulta, periodosOcupado);

            // Assert
            Assert.Equal(3, horarios.Count);
            Assert.Contains(new TimeOnly(8, 0), horarios);
            Assert.Contains(new TimeOnly(9, 0), horarios);
            Assert.Contains(new TimeOnly(9, 30), horarios);
            Assert.DoesNotContain(new TimeOnly(8, 30), horarios); // esse conflita
        }

        [Fact]
        public void Nao_Deve_Gerar_Horarios_Se_Todos_Estiverem_Ocupados()
        {
            // Arrange
            var periodo = new Periodo(new TimeOnly(10, 0), new TimeOnly(11, 0));
            int tempoConsulta = 30;

            var ocupado1 = new Periodo(new TimeOnly(10, 0), new TimeOnly(10, 30));
            var ocupado2 = new Periodo(new TimeOnly(10, 30), new TimeOnly(11, 0));
            var periodosOcupado = new List<Periodo> { ocupado1, ocupado2 };

            // Act
            var horarios = periodo.GerarHorarios(tempoConsulta, periodosOcupado);

            // Assert
            Assert.Empty(horarios);
        }
    }
}
