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

        private void ValidarPeriodo(TimeOnly inicio, TimeOnly fim)
        {
            if (inicio > fim) throw new InvalidOperationException($"Horário de início '{inicio}' não pode ser maior que o horário fim '{fim}'.");
        }
    }
}
