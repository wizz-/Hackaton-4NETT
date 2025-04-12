using Domain.Entities.Login;

namespace Domain.Entities.Cadastros
{
    public class Medico
    {
        public int Id { get; private set; }
        public string? Nome { get; private set; }
        public virtual Crm? Crm { get; private set; }
        public Especialidade? Especialidade { get; private set; }
        public virtual IList<HorarioDisponivel>? HorariosDisponiveis { get; private set; }
        public virtual Usuario? Usuario { get; private set; }

        protected Medico()
        {
        }

        public Medico(string nome, Crm crm, int tempodeConsulta, Especialidade especialidade, IList<HorarioDisponivel> horariosDisponiveis, Usuario usuario)
        {
            ValidarCampos(nome, crm, especialidade, horariosDisponiveis, usuario);

            Nome = nome;
            Crm = crm;
            Especialidade = especialidade;
            HorariosDisponiveis = horariosDisponiveis;
            Usuario = usuario;
        }

        public IList<TimeOnly> ObterHorasDiponiveis(DayOfWeek diaDaSemana, IList<Periodo> periodosOcupado)
        {
            return HorariosDisponiveis!.Where(x => x.DiaDaSemana == diaDaSemana).SelectMany(x => x.ObterHoras(periodosOcupado)).ToList();
        }

        private void ValidarCampos(string nome, Crm crm, Especialidade especialidade, IList<HorarioDisponivel> horariosDisponiveis, Usuario usuario)
        {
            ValidarNome(nome);
            ValidarCrm(crm);
            ValidarEspecialidade(especialidade);
            ValidarHorariosDisponiveis(horariosDisponiveis);
            ValidarUsuario(usuario);
        }

        private void ValidarHorariosDisponiveis(IList<HorarioDisponivel> horariosDisponiveis)
        {
            if (horariosDisponiveis == null) throw new InvalidOperationException("A lista de horários não pode ser nula.");

            foreach (var item in horariosDisponiveis)
            {
                if (item == null) throw new InvalidOperationException("Um dos horários da lista é nulo.");
            }
        }

        private void ValidarCrm(Crm crm)
        {
            if (crm == null) throw new InvalidOperationException("CRM não pode ser nulo.");
        }

        private void ValidarEspecialidade(Especialidade especialidade)
        {
            if (especialidade == null) throw new InvalidOperationException("Especialidadeñão pode ser nula.");
        }

        private void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new InvalidOperationException("Nome Inválido.");
        }

        private void ValidarUsuario(Usuario usuario)
        {
            if (usuario == null) throw new InvalidOperationException("Usuário Inválido.");
        }
    }
}
