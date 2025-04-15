using Hackaton.Web.Models.Especialidade;
using Hackaton.Web.Models.Usuario;

namespace Hackaton.Web.Models.Medico
{
    public class MedicoCadastroRequest
    {
        public int Id { get; set; } = 0;
        public string Nome { get; set; } = string.Empty;
        public string CrmNumero { get; set; } = string.Empty;
        public string CrmUf { get; set; } = string.Empty;
        public EspecialidadeModel Especialidade { get; set; } = new();
        public UsuarioRequest Usuario { get; set; } = new();
    }
}
