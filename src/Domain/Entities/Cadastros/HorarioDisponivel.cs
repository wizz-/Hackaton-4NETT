using Domain.Enums;

namespace Domain.Entities.Cadastros
{
    public class HorarioDisponivel
    {
        public int Id { get; private set; }
        public DiaDaSemana DiaDaSemana { get; private set; }
        public virtual Periodo Periodo { get; private set; }

        protected HorarioDisponivel()
        {

        }

        public HorarioDisponivel(DiaDaSemana diaDaSemana, Periodo periodo)
        {
            DiaDaSemana = diaDaSemana;
            Periodo = periodo;
        }
    }
}
