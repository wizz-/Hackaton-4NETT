using Application.Services.Consultas.Dtos;

namespace Application.Services.Consultas.Interfaces
{
    public interface IConsultaAppService
    {
        void MarcarConsulta(ConsultaDto dto);
    }
}
