using Application.Services.Medicos.Dtos;

namespace Application.Services.Medicos.Interfaces
{
    public interface IMedicoAppService
    {
        IList<MedicoParaConsultaDto> ObterMedicosPorEspecialidade(int especialidadeId);
        IList<MedicoParaConsultaDto> ObterTodosMedicos();
        CadastroMedicoDto ObterDadosDoMedico(int id);
    }
}
