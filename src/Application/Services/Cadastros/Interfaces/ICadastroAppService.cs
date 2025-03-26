using Application.Services.Cadastros.Dtos;

namespace Application.Services.Cadastros.Interfaces
{
    public interface ICadastroAppService
    {
        void CadastrarPaciente(PacienteAppDto dto);
        void CadastrarMedico(MedicoAppDto dto);
    }
}
