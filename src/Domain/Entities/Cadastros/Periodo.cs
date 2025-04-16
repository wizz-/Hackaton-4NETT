namespace Domain.Entities.Cadastros
{
    public class Periodo
    {
        public TimeOnly Inicio { get; private set; }
        public TimeOnly Fim { get; private set; }

        protected Periodo()
        {
        }

        public Periodo(TimeOnly inicio, TimeOnly fim)
        {
            ValidarPeriodo(inicio, fim);

            Inicio = inicio;
            Fim = fim;
        }
       

        public List<TimeOnly> GerarHorarios(int tempoDeConsulta, IList<Periodo> periodosOcupado)
        {
            var timeSpan = TimeSpan.FromMinutes(tempoDeConsulta);

            var horarios = new List<TimeOnly>();
            var atual = Inicio;

            while (atual.Add(timeSpan) <= Fim)
            {
                if (!periodosOcupado.Any(x => x.HaConflito(atual, timeSpan, x)))
                {
                    horarios.Add(atual);
                }
                atual = atual.Add(timeSpan);
            }

            return horarios;
        }

        private bool HaConflito(TimeOnly horario, TimeSpan tempoDeConsulta, Periodo periodoOcupado)
        {
            return horario < periodoOcupado.Fim && horario.Add(tempoDeConsulta) > periodoOcupado.Inicio;
        }

        private void ValidarPeriodo(TimeOnly inicio, TimeOnly fim)
        {
            if (inicio > fim) throw new InvalidOperationException($"Horário de início '{inicio}' não pode ser maior que o horário fim '{fim}'.");
        }
    }
}
