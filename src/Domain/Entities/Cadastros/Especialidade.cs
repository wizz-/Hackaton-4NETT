namespace Domain.Entities.Cadastros
{
    public class Especialidade
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }

        protected Especialidade()
        {
        }

        public Especialidade(string nome)
        {
            Nome = nome;
        }
    }
}
