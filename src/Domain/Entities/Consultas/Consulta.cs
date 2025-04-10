using Domain.Entities.Cadastros;

namespace Domain.Entities.Consultas
{
    public class Consulta
    {
        public int Id { get; private set; }
        public DateOnly Dia { get; private set; }
        public virtual Paciente Paciente { get; private set; }
        public virtual Medico Medico { get; private set; }
        public virtual Especialidade Especialidade { get; private set; }
        public virtual Periodo Horario { get; private set; }

        protected Consulta()
        {
        }

        public Consulta(DateOnly dia, Paciente paciente, Medico medico, Especialidade especialidade, Periodo horario)
        {
            Dia = dia;
            Paciente = paciente;
            Medico = medico;
            Especialidade = especialidade;
            Horario = horario;
        }
    }
}
