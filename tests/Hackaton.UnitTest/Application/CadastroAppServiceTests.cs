using Application.Services.Cadastros.Dtos;
using Application.Services.Cadastros;
using Application.Services.Logins.Dtos;
using Domain.Entities.Login;
using Domain.Enums;
using Domain.Interfaces.Infra.Data.DAL.Repositories;
using Domain.Interfaces.Infra.Data.DAL;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using Domain.Entities.Cadastros;

namespace Hackaton.UnitTest.Application
{
    public class CadastroAppServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly Mock<IUsuarioRepository> _mockUsuarioRepo;
        private readonly Mock<IPacienteRepository> _mockPacienteRepo;
        private readonly CadastroAppService _service;        
        private readonly Mock<IMedicoRepository> _mockMedRepo;
        private readonly Mock<IEspecialidadeRepository> _mockEspecialidadeRepo;
       
        public CadastroAppServiceTests()
        {
            _mockUow = new Mock<IUnitOfWork>();
            _mockUsuarioRepo = new Mock<IUsuarioRepository>();
            _mockPacienteRepo = new Mock<IPacienteRepository>();

            _mockUow.Setup(u => u.UsuarioRepository).Returns(_mockUsuarioRepo.Object);
            _mockUow.Setup(u => u.PacienteRepository).Returns(_mockPacienteRepo.Object);
            
            _mockMedRepo = new Mock<IMedicoRepository>();
            _mockEspecialidadeRepo = new Mock<IEspecialidadeRepository>();

            _mockUow.Setup(u => u.MedicoRepository).Returns(_mockMedRepo.Object);
            _mockUow.Setup(u => u.EspecialidadeRepository).Returns(_mockEspecialidadeRepo.Object);

            _service = new CadastroAppService(_mockUow.Object);
        }

        private static SecureString GetSecureUsuario(string senha)
        {
            var securitySenha = new SecureString();
            senha.ToCharArray().ToList().ForEach(f => securitySenha.AppendChar(f));

            return securitySenha;
        }

        [Fact]
        public void CadastrarPaciente_EmailJaCadastrado_DeveLancarInvalidOperationException()
        {
            // Arrange
            var dto = new PacienteDto
            {
                Cpf = "42310345679",
                Email = "joao@example.com",
                Nome = "João",
                Usuario = new UsuarioAppDto
                {
                    Email = "joao@example.com",
                    Senha = GetSecureUsuario("senha123")
                }
            };
            _mockUsuarioRepo
                .Setup(r => r.ObterPorEmail(dto.Usuario.Email))
                .Returns(new Usuario("joao@example.com", GetSecureUsuario("hash"), TipoDeUsuario.Paciente));
            
            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() =>
                _service.CadastrarPaciente(dto));
            Assert.Equal("O e-mail informado já está associado com outro usuário.", ex.Message);
        }

        [Fact]
        public void CadastrarPaciente_CpfJaCadastrado_DeveLancarInvalidOperationException()
        {
            // Arrange
            var dto = new PacienteDto
            {
                Cpf = "42310345679",
                Email = "maria@example.com",
                Nome = "Maria",
                Usuario = new UsuarioAppDto
                {
                    Email = "maria@example.com",
                    Senha = GetSecureUsuario("senha123")
                }
            };
            // e‑mail livre
            _mockUsuarioRepo
                .Setup(r => r.ObterPorEmail(dto.Usuario.Email))
                .Returns((Usuario?)null);
            // CPF já existe
            _mockPacienteRepo
                .Setup(r => r.ObterPorCpf(dto.Cpf))
                .Returns(new Paciente(dto.Cpf, dto.Email, dto.Nome, new Usuario(dto.Email, GetSecureUsuario("hash"), TipoDeUsuario.Paciente)));

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() =>
                _service.CadastrarPaciente(dto));
            Assert.Equal("O CPF informado já está já foi cadastrado.", ex.Message);
        }

        [Fact]
        public void CadastrarPaciente_Sucesso_DeveChamarInserirEGuardarMudancas()
        {
            // Arrange
            var dto = new PacienteDto
            {
                Cpf = "98765432100",
                Email = "ana@example.com",
                Nome = "Ana",
                Usuario = new UsuarioAppDto
                {
                    Email = "ana@example.com",
                    Senha = GetSecureUsuario("senha123")
                }
            };
            _mockUsuarioRepo
                .Setup(r => r.ObterPorEmail(dto.Usuario.Email))
                .Returns((Usuario?)null);
            _mockPacienteRepo
                .Setup(r => r.ObterPorCpf(dto.Cpf))
                .Returns((Paciente?)null);

            // Act
            _service.CadastrarPaciente(dto);

            // Assert
            _mockPacienteRepo.Verify(r =>
                r.Inserir(It.Is<Paciente>(p =>
                    p.Cpf == dto.Cpf &&
                    p.Email == dto.Email &&
                    p.Nome == dto.Nome
                )),
                Times.Once);

            _mockUow.Verify(u => u.SaveChanges(), Times.Once);
        }

        [Fact]
        public void CadastrarMedico_CrmJaCadastrado_ThrowsInvalidOperationException()
        {
            // Arrange
            var dto = new MedicoDto
            {
                Nome = "Dr. Ex",
                CrmNumero = "123456",
                CrmUf = "SP",
                Usuario = new UsuarioAppDto { Email = "doc@ex.com", Senha = GetSecureUsuario("pwd") },
                Especialidade = new EspecialidadeDto { Id = 0, Nome = "Cardio" }
            };

            // CRM duplicado
            _mockMedRepo
                .Setup(r => r.ObterPorCrm(dto.CrmNumero, UnidadeFederativa.SP))
                .Returns(new Medico(dto.Nome,
                                    new Crm(dto.CrmNumero, UnidadeFederativa.SP),
                                    new Especialidade(dto.Especialidade.Nome),
                                    new Usuario(dto.Usuario.Email, dto.Usuario.Senha, TipoDeUsuario.Medico)));

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => _service.CadastrarMedico(dto));
            Assert.Equal("O CRM informado já está cadastrado.", ex.Message);
        }

        [Fact]
        public void CadastrarMedico_UfInvalido_ThrowsInvalidOperationException()
        {
            // Arrange
            var dto = new MedicoDto
            {
                Nome = "Dr. UF",
                CrmNumero = "123456",
                CrmUf = "XX", // inválido
                Usuario = new UsuarioAppDto { Email = "uf@ex.com", Senha = GetSecureUsuario("pwd") },
                Especialidade = new EspecialidadeDto { Id = 1, Nome = "Ortopedia" }
            };

            // Act & Assert (executes ObterUf antes de qualquer repo)
            var ex = Assert.Throws<InvalidOperationException>(() => _service.CadastrarMedico(dto));
            Assert.Equal("O valor 'XX' não é válido para o enum.", ex.Message);
        }

        [Fact]
        public void CadastrarMedico_EmailJaAssociado_ThrowsInvalidOperationException()
        {
            // Arrange
            var dto = new MedicoDto
            {
                Nome = "Dr. Ema",
                CrmNumero = "123456",
                CrmUf = "RJ",
                Usuario = new UsuarioAppDto { Email = "ema@ex.com", Senha = GetSecureUsuario("pwd") },
                Especialidade = new EspecialidadeDto { Id = 2, Nome = "Dermato" }
            };

            // UF e CRM válidos
            _mockMedRepo
                .Setup(r => r.ObterPorCrm(dto.CrmNumero, UnidadeFederativa.RJ))
                .Returns((Medico?)null);
            // e-mail duplicado
            _mockUsuarioRepo
                .Setup(r => r.ObterPorEmail(dto.Usuario.Email))
                .Returns(new Usuario(dto.Usuario.Email, dto.Usuario.Senha, TipoDeUsuario.Medico));

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => _service.CadastrarMedico(dto));
            Assert.Equal("O e-mail informado já está associado com outro usuário.", ex.Message);
        }

        [Fact]
        public void CadastrarMedico_EspecialidadeNaoLocalizada_ThrowsInvalidOperationException()
        {
            // Arrange
            var dto = new MedicoDto
            {
                Nome = "Dr. Esp",
                CrmNumero = "123456",
                CrmUf = "MG",
                Usuario = new UsuarioAppDto { Email = "esp@ex.com", Senha = GetSecureUsuario("pwd") },
                Especialidade = new EspecialidadeDto { Id = 99, Nome = "Inexistente" }
            };

            // UF e CRM e e-mail válidos
            _mockMedRepo
                .Setup(r => r.ObterPorCrm(dto.CrmNumero, UnidadeFederativa.MG))
                .Returns((Medico?)null);
            _mockUsuarioRepo
                .Setup(r => r.ObterPorEmail(dto.Usuario.Email))
                .Returns((Usuario?)null);
            // especialidade não encontrada
            _mockEspecialidadeRepo
                .Setup(r => r.ObterPorId(dto.Especialidade.Id))
                .Returns((Especialidade?)null);

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => _service.CadastrarMedico(dto));
            Assert.Equal(
                $"Especialidade '{dto.Especialidade.Nome}' com id '{dto.Especialidade.Id}' não foi localizada.",
                ex.Message);
        }

        [Fact]
        public void CadastrarMedico_Sucesso_DeveInserirMedicoEChamarSaveChanges()
        {
            // Arrange
            var dto = new MedicoDto
            {
                Nome = "Dr. OK",
                CrmNumero = "123456",
                CrmUf = "BA",
                Usuario = new UsuarioAppDto { Email = "ok@ex.com", Senha = GetSecureUsuario("pwd") },
                Especialidade = new EspecialidadeDto { Id = 0, Nome = "Oftalmo" }
            };

            // UF, CRM e e-mail livres
            _mockMedRepo
                .Setup(r => r.ObterPorCrm(dto.CrmNumero, UnidadeFederativa.BA))
                .Returns((Medico?)null);
            _mockUsuarioRepo
                .Setup(r => r.ObterPorEmail(dto.Usuario.Email))
                .Returns((Usuario?)null);
            // especialidade existe
            _mockEspecialidadeRepo
                .Setup(r => r.ObterPorId(dto.Especialidade.Id))
                .Returns(new Especialidade(dto.Especialidade.Nome));

            // Act
            _service.CadastrarMedico(dto);

            // Assert
            _mockMedRepo.Verify(r => r.Inserir(It.Is<Medico>(m =>
                m.Nome == dto.Nome &&
                m.Crm.Numero == dto.CrmNumero &&
                m.Crm.Uf == UnidadeFederativa.BA &&
                m.Especialidade.Id == dto.Especialidade.Id &&
                m.Usuario.Email == dto.Usuario.Email
            )), Times.Once);
            _mockUow.Verify(u => u.SaveChanges(), Times.Once);
        }
    }
}

