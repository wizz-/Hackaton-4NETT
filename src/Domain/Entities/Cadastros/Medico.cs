using Domain.Entities.Login;

namespace Domain.Entities.Cadastros
{
    public class Medico
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public virtual Crm Crm { get; private set; }
        public int TempoDeConsulta { get; private set; }
        public virtual IList<Especialidade> Especialidades { get; private set; }
        public virtual IList<HorarioDisponivel> HorariosDisponiveis { get; private set; }
        public virtual Usuario Usuario { get; private set; }

        protected Medico()
        {
        }

        public Medico(string nome, Crm crm, int tempodeConsulta, IList<Especialidade> especialidades, IList<HorarioDisponivel> horariosDisponiveis, Usuario usuario)
        {
            ValidarCampos(nome, crm, especialidades, horariosDisponiveis, usuario);

            Nome = nome;
            Crm = crm;
            TempoDeConsulta = tempodeConsulta;
            Especialidades = especialidades;
            HorariosDisponiveis = horariosDisponiveis;
            Usuario = usuario;
        }

        private void ValidarCampos(string nome, Crm crm, IList<Especialidade> especialidades, IList<HorarioDisponivel> horariosDisponiveis, Usuario usuario)
        {
            ValidarNome(nome);
            ValidarCrm(crm);
            ValidarEspecialidades(especialidades);
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

        private void ValidarEspecialidades(IList<Especialidade> especialidades)
        {
            if (especialidades == null) throw new InvalidOperationException("A lista de Especialidades não pode ser nula.");

            //https://www.jusbrasil.com.br/artigos/quantas-especialidades-medicas-pode-um-medico-ter/2066715627
            if (especialidades.Count > 2) throw new InvalidOperationException("Um médico não pode ter mais de duas especialidades.");

            foreach (var item in especialidades)
            {
                if (item == null) throw new InvalidOperationException("Um das especialidades da lista é nula.");
            }
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
