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
        public bool? Confirmada { get; private set; }
        public bool Cancelada { get; private set; }

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

        public void ConfirmarConsulta(Medico medico)
        {
            if (Medico.Crm.Numero != medico.Crm.Numero || Medico.Crm.Uf != medico.Crm.Uf) throw new InvalidOperationException($"Consulta não pode ser confirmada por outro médico.");

            Confirmada = true;
        }

        public void RejeitarConsulta(Medico medico)
        {
            if (Medico.Crm.Numero != medico.Crm.Numero || Medico.Crm.Uf != medico.Crm.Uf) throw new InvalidOperationException($"Consulta não pode ser cancelada por outro médico.");

            Confirmada = false;
        }

        public void CancelarConsulta(Paciente paciente)
        {
            if (Paciente.Cpf != paciente.Cpf) throw new InvalidOperationException($"Consulta não pode ser cancelada por outro paciente");
            Cancelada = true;
        }
    }
}
