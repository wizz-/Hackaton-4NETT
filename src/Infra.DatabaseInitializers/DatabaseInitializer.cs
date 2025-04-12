using Infra.Data.Context;
using Infra.DatabaseInitializers.DataImporters.Interfaces;
using Infra.DatabaseInitializers.Interfaces;

namespace Infra.DatabaseInitializers
{
    public class DatabaseInitializer(Contexto context, IDataImporter dataImporter) : IDatabaseInitializer
    {
        public void InicializarDatabase()
        {
            var databaseWasCreated = context.Database.EnsureCreated();

            if (databaseWasCreated)
            {
                dataImporter.Seed();
            }
        }
    }
}
