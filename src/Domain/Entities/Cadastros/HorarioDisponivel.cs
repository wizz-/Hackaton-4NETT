namespace Domain.Entities.Cadastros
{
    public class HorarioDisponivel
    {
        public int Id { get; private set; }
        public virtual Especialidade? Especialidade { get; set; }
        public DayOfWeek DiaDaSemana { get; private set; }
        public virtual Periodo? Periodo { get; private set; }

        protected HorarioDisponivel()
        {
        }

        public HorarioDisponivel(DayOfWeek diaDaSemana, Especialidade especialidade, Periodo periodo)
        {
            Especialidade = especialidade;
            DiaDaSemana = diaDaSemana;
            Periodo = periodo;
        }

        public IList<TimeOnly> ObterHoras(int tempoDeConsulta, IList<Periodo> periodosOcupado)
        {
            return Periodo!.GerarHorarios(tempoDeConsulta, periodosOcupado);
        }
    }
}
