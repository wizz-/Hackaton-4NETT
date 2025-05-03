using Hackaton.Web.Models.Medico;

namespace Hackaton.Web.Services.Medicos.Interfaces
{
    public interface IMedicoService
    {
        Task CadastrarMedicoAsync(PrimeiroAcessoMedicoModel medico);
        Task<MeuCadastroMedicoModel> ObterMedicoAsync(int id);
        Task<IList<MedicoParaConsultaModel>> ObterTodosMedicos();
        Task AtualizarCadastroAsync(MedicoMeuCadastroRequest medico);
    }
}
