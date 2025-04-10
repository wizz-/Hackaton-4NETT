using Application.Services.Calendarios.Dtos;

namespace Application.Services.Calendarios.Interfaces
{
    public interface ICalendarioAppService
    {
        IList<MedicoAppDto> ObterCalendario(DateOnly dia, int especialidadeId);
    }
}
