using Hackaton.Web.Models.Medico;

namespace Hackaton.Web.Services.Medicos.Interfaces
{
    public interface IMedicoService
    {
        Task CadastrarMedicoAsync(MedicoCadastroRequest medico);
    }
}
