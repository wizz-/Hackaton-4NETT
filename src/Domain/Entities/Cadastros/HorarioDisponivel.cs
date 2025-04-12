namespace Domain.Entities.Cadastros
{
    public class HorarioDisponivel
    {
        public int Id { get; private set; }
        public decimal ValorDaConsulta { get; private set; }
        public int TempoDeConsulta { get; private set; } = 30;
        public virtual Especialidade? Especialidade { get; set; }
        public DayOfWeek DiaDaSemana { get; private set; }
        public virtual Periodo? Periodo { get; private set; }

        protected HorarioDisponivel()
        {
        }

        public HorarioDisponivel(DayOfWeek diaDaSemana, Especialidade especialidade, Periodo periodo, decimal valorDaConsulta)
        {
            Especialidade = especialidade;
            DiaDaSemana = diaDaSemana;
            Periodo = periodo;
            ValorDaConsulta = valorDaConsulta;
        }

        public IList<TimeOnly> ObterHoras(IList<Periodo> periodosOcupado)
        {
            return Periodo!.GerarHorarios(TempoDeConsulta, periodosOcupado);
        }
    }
}
