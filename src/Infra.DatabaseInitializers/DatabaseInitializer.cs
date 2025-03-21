using Infra.Data.Context;
using Infra.DatabaseInitializers.Interfaces;

namespace Infra.DatabaseInitializers
{
    public class DatabaseInitializer(Contexto context) : IDatabaseInitializer
    {
        public void InicializarDatabase()
        {
            context.Database.EnsureCreated();
        }
    }
}
