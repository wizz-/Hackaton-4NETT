using Hackaton.Web.Models.Medico;

namespace Hackaton.Web.Services.Medicos
{
    public interface IMedicoService
    {
        Task CadastrarMedicoAsync(MedicoCadastroRequest medico);
    }
}
