using Infra.Data.Context;
using Infra.DatabaseInitializers.DataImporters.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TechChallenge4.Infra.DatabaseInitializers.DataImporters
{
    public class DataImporter : IDataImporter
    {
        private readonly Contexto Contexto;

        public DataImporter(Contexto contexto)
        {
            Contexto = contexto;
        }

        public void Seed()
        {
            var currentDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

            var allSqlFiles = GetFiles(currentDirectory);

            foreach (var sqlFile in allSqlFiles)
            {
                Contexto.Database.ExecuteSqlRaw(File.ReadAllText(sqlFile));
            }
        }

        private static IList<string> GetFiles(DirectoryInfo basePath)
        {
            var path = ProcurarPastaSqlFiles(basePath);

            if (string.IsNullOrWhiteSpace(path))
            {
                return new List<string>();
            }

            var sqlFiles = Directory.GetFiles(path, "*.sql").OrderBy(x => x).ToList();

            var allFiles = new List<string>();
            allFiles.AddRange(sqlFiles);

            return allFiles;
        }

        private static string ProcurarPastaSqlFiles(DirectoryInfo basePath)
        {
            var nomePastaSql = "SqlScripts";
            var diretorioPrincipal = Path.Combine(basePath.FullName, nomePastaSql);

            if (Directory.Exists(diretorioPrincipal)) return diretorioPrincipal;

            do
            {
                basePath = basePath.Parent;
                var diretorioBuscado = Path.Combine(basePath.FullName, nomePastaSql);

                if (Directory.Exists(diretorioBuscado)) return diretorioBuscado;

            } while (basePath.Parent != null);

            return null;
        }
    }
}
