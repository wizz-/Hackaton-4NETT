using Application.Services.Consultas.Dtos;

namespace Application.Services.Consultas.Interfaces
{
    public interface IConsultaAppService
    {
        void MarcarConsulta(ConsultaCadastroDto dto);
        void ConfirmarConsulta(int consultaId, string crm, string uf);
        void RejeitarConsulta(int consultaId, string crm, string uf);
        void CancelarConsulta(int consultaId, string cpf, string motivo);
        IList<ConsultaDto> ObterConsultasDoMedico(int medicoId);
    }
}
