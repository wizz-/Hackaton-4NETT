using Domain.Entities.Cadastros;
using Domain.Enums;

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
        public StatusConsulta Status { get; private set; }
        public string? MotivoDeCancelamento { get; private set; }

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
            Status = StatusConsulta.Pendente;
        }

        public Consulta(DateTime dataHorario, Paciente paciente, Medico medico, Especialidade especialidade)
        {
            var horaInicial = TimeOnly.FromDateTime(dataHorario);

            var diaDaConsulta = medico.HorariosDisponiveis.First(x => x.DiaDaSemana == dataHorario.DayOfWeek);

            Dia = DateOnly.FromDateTime(dataHorario);
            Paciente = paciente;
            Medico = medico;
            Especialidade = especialidade;
            Horario = new Periodo(horaInicial, horaInicial.AddMinutes(diaDaConsulta.TempoDeConsulta));
            Status = StatusConsulta.Pendente;
        }

        public void ConfirmarConsulta(Medico medico)
        {
            if (Medico.Crm.Numero != medico.Crm.Numero || Medico.Crm.Uf != medico.Crm.Uf) throw new InvalidOperationException($"Consulta não pode ser confirmada por outro médico.");

            Status = StatusConsulta.Confirmada;
        }

        public void RejeitarConsulta(Medico medico)
        {
            if (Medico.Crm.Numero != medico.Crm.Numero || Medico.Crm.Uf != medico.Crm.Uf) throw new InvalidOperationException($"Consulta não pode ser cancelada por outro médico.");

            Status = StatusConsulta.Recusada;
        }

        public void CancelarConsulta(Paciente paciente, string motivo)
        {
            if (Paciente.Cpf != paciente.Cpf) throw new InvalidOperationException($"Consulta não pode ser cancelada por outro paciente");
            if (string.IsNullOrWhiteSpace(motivo)) throw new InvalidOperationException("Motivo não pode ser nulo");

            Status = StatusConsulta.Cancelada;
            MotivoDeCancelamento = motivo;
        }
    }
}
